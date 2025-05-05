using System;
using System.Collections.Concurrent;
using System.IO;
using System.Net;
using System.ServiceModel.Web;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace WcfService1
{
    [ServiceBehavior(
        InstanceContextMode = InstanceContextMode.PerCall,
        ConcurrencyMode = ConcurrencyMode.Multiple,
        IncludeExceptionDetailInFaults = false)]
    public class Service1 : IService1
    {
        static ConcurrentDictionary<Guid, TaskCompletionSource<MatrixDto>> _matrices =
            new ConcurrentDictionary<Guid, TaskCompletionSource<MatrixDto>>();

        static ConcurrentDictionary<Guid, MatrixChunkManager> _chunkManagers =
            new ConcurrentDictionary<Guid, MatrixChunkManager>();

        static ConcurrentDictionary<Guid, TaskCompletionSource<byte[]>> _fractals =
            new ConcurrentDictionary<Guid, TaskCompletionSource<byte[]>>();

        public Task<CreateMatrixResponse> CreateMatrixAsync(CreateMatrixRequest req)
        {
            if (req.Rows <= 0 || req.Columns <= 0)
                throw new FaultException<string>("Rows and Columns must be > 0", "InvalidArgument");

            var id = Guid.NewGuid();
            _chunkManagers[id] = new MatrixChunkManager(req.Rows, req.Columns);
            _matrices[id] = new TaskCompletionSource<MatrixDto>();
            return Task.FromResult(new CreateMatrixResponse(id));
        }

        public void UploadMatrixChunk(string matrixId, MatrixChunk chunk)
        {
            if (!Guid.TryParse(matrixId, out var id))
                throw new FaultException<string>("Invalid matrixId format", "InvalidArgument");

            if (!_chunkManagers.TryGetValue(id, out var manager))
                throw new WebFaultException(HttpStatusCode.NotFound);

            manager.AddChunk(chunk.ChunkIndex, chunk.Data, chunk.TotalChunks);

            if (manager.IsComplete && _matrices.TryGetValue(id, out var tcs))
            {
                tcs.SetResult(new MatrixDto
                {
                    Rows = manager.Rows,
                    Columns = manager.Columns,
                    Data = manager.GetData()
                });

                _chunkManagers.TryRemove(id, out _);
            }
        }

        public async Task<MatrixDto> GetMatrixAsync(string matrixId)
        {
            if (!Guid.TryParse(matrixId, out var id))
                throw new FaultException<string>("Invalid matrixId format", "InvalidArgument");
            if (!_matrices.TryGetValue(id, out var tcs))
                throw new WebFaultException(HttpStatusCode.NotFound);

            return await tcs.Task.ConfigureAwait(false);
        }

        public Task<MultiplyResponse> MultiplyMatricesAsync(MultiplyRequest req)
        {
            if (req.LeftMatrixId == Guid.Empty || req.RightMatrixId == Guid.Empty)
                throw new FaultException<string>("Matrix IDs cannot be empty", "InvalidArgument");

            var id = Guid.NewGuid();
            var tcs = new TaskCompletionSource<MatrixDto>();
            _matrices[id] = tcs;

            _ = Task.Run(async () =>
            {
                var left = await _matrices[req.LeftMatrixId].Task;
                var right = await _matrices[req.RightMatrixId].Task;
                var data = MatrixOps.Multiply(left.Data, left.Rows, left.Columns, right.Data, right.Rows, right.Columns);

                tcs.SetResult(new MatrixDto { Rows = left.Rows, Columns = right.Columns, Data = data });
            });

            return Task.FromResult(new MultiplyResponse(id));
        }

        public Task<MandelbrotResponse> GenerateMandelbrotAsync(MandelbrotRequest req)
        {
            if (req.Width <= 0 || req.Height <= 0)
                throw new FaultException<string>("Invalid image dimensions", "InvalidArgument");

            var id = Guid.NewGuid();
            var tcs = new TaskCompletionSource<byte[]>();
            _fractals[id] = tcs;

            _ = Task.Run(() =>
                tcs.SetResult(FractalOps.RenderPng(req.Width, req.Height, req.XMin, req.XMax, req.YMin, req.YMax, req.MaxIterations)));

            return Task.FromResult(new MandelbrotResponse(id));
        }

        public async Task<Stream> GetMandelbrotAsync(string imageId)
        {
            if (!Guid.TryParse(imageId, out var id))
                throw new FaultException<string>("Invalid ImageId", "InvalidArgument");

            if (!_fractals.TryGetValue(id, out var tcs))
                throw new WebFaultException(HttpStatusCode.NotFound);

            var bytes = await tcs.Task.ConfigureAwait(false);
            // Only set headers if we're actually in a Web context
            if (OperationContext.Current != null)
            {
                var http = OperationContext.Current.OutgoingMessageProperties
                                .ContainsKey(HttpResponseMessageProperty.Name)
                            ? (HttpResponseMessageProperty)OperationContext.Current.OutgoingMessageProperties[HttpResponseMessageProperty.Name]
                            : new HttpResponseMessageProperty();

                http.Headers[HttpResponseHeader.ContentType] = "image/png";
                OperationContext.Current.OutgoingMessageProperties[HttpResponseMessageProperty.Name] = http;
            }

            // Return the raw bytes in a MemoryStream; WCF will wrap it in SOAP for you
            return new MemoryStream(bytes);
        }

        public async Task<byte[]> GetMandelbrotRawAsync(string imageId)
        {
            if (!Guid.TryParse(imageId, out var id))
                throw new FaultException<string>("Invalid ImageId", "InvalidArgument");

            if (!_fractals.TryGetValue(id, out var tcs))
                throw new WebFaultException(HttpStatusCode.NotFound);
            return await tcs.Task;
        }
    }

    class MatrixChunkManager
    {
        readonly ConcurrentDictionary<int, double[]> _chunks = new ConcurrentDictionary<int, double[]>();
        public int Rows { get; }
        public int Columns { get; }
        public int TotalChunks { get; private set; }

        public MatrixChunkManager(int rows, int cols)
        {
            Rows = rows;
            Columns = cols;
        }

        public void AddChunk(int index, double[] data, int totalChunks)
        {
            TotalChunks = totalChunks;
            _chunks[index] = data;
        }

        public bool IsComplete => _chunks.Count == TotalChunks;

        public double[] GetData()
        {
            var result = new double[Rows * Columns];
            int offset = 0;
            for (int i = 0; i < TotalChunks; i++)
            {
                Array.Copy(_chunks[i], 0, result, offset, _chunks[i].Length);
                offset += _chunks[i].Length;
            }
            return result;
        }
    }

    static class FractalOps
    {
        public static byte[] RenderPng(
            int w, int h,
            double xmin, double xmax,
            double ymin, double ymax,
            int maxIter)
        {
            using (Bitmap bmp = new Bitmap(w, h))
            {
                for (int py = 0; py < h; py++)
                {
                    double y0 = ymin + (ymax - ymin) * py / (h - 1);
                    for (int px = 0; px < w; px++)
                    {
                        double x0 = xmin + (xmax - xmin) * px / (w - 1);
                        double x = 0, y = 0; int iter = 0;
                        while (x * x + y * y <= 4 && iter < maxIter)
                        {
                            double xt = x * x - y * y + x0;
                            y = 2 * x * y + y0;
                            x = xt;
                            iter++;
                        }
                        int c = iter == maxIter ? 0 : 255 - (int)(255.0 * iter / maxIter);
                        bmp.SetPixel(px, py, Color.FromArgb(c, c, c));
                    }
                }
                using (MemoryStream ms = new MemoryStream())
                {
                    bmp.Save(ms, ImageFormat.Png);
                    return ms.ToArray();
                }
            }
        }
    }

    static class MatrixOps
    {
        public static double[] Multiply(
            double[] a, int aR, int aC,
            double[] b, int bR, int bC)
        {
            var result = new double[aR * bC];
            for (int i = 0; i < aR; i++)
                for (int j = 0; j < bC; j++)
                {
                    double sum = 0;
                    for (int k = 0; k < aC; k++)
                        sum += a[i * aC + k] * b[k * bC + j];
                    result[i * bC + j] = sum;
                }
            return result;
        }
    }
}
