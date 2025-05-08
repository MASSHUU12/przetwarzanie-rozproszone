using ConsoleApp1.MojService;
using System;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Starting test suite...\n");

            var tests = new Task[]
            {
                Task.Run(() => RandomMatricesMultiplyAndValidate(50, 50,   5,    1234)),
                //Task.Run(() => EdgeCaseMatrixCreation(0,   10)),
                //Task.Run(() => EdgeCaseMatrixCreation(-1,  -1)),
                Task.Run(() => StressTestLargeMatrix(200, 200,  50)),
                Task.Run(() => MandelbrotRaw("mandel_raw.bmp")),
                Task.Run(() => MandelbrotStream("mandel_stream.bmp")),
                Task.Run(() => MandelbrotTimeoutTest( 10 /*polls*/, 100 /*ms*/ ))
            };

            Task.WaitAll(tests);
            Console.WriteLine("\nAll tests completed.");
            Console.ReadLine();
        }

        #region Matrix Tests

        static void RandomMatricesMultiplyAndValidate(
            int rows,
            int cols,
            int chunksCount,
            int seed
        )
        {
            var rnd = new Random(seed);
            double[] A = Enumerable.Range(0, rows * cols)
                .Select(_ => rnd.NextDouble() * 10).ToArray();
            double[] B = Enumerable.Range(0, rows * cols)
                .Select(_ => rnd.NextDouble() * 10).ToArray();

            var client = new Service1Client();

            // create & upload A
            var aId = client.CreateMatrixAsync(new CreateMatrixRequest { Rows = rows, Columns = cols })
                            .Result.MatrixId;
            UploadInChunks(client, aId, rows, cols, chunksCount, A);

            // create & upload B (dims swapped so A×B is valid)
            var bId = client.CreateMatrixAsync(new CreateMatrixRequest { Rows = cols, Columns = rows })
                            .Result.MatrixId;
            UploadInChunks(client, bId, cols, rows, chunksCount, B);

            // multiply
            var resultId = client.MultiplyMatricesAsync(new MultiplyRequest
            {
                LeftMatrixId = aId,
                RightMatrixId = bId
            }).Result.ResultMatrixId;

            // poll until ready
            MatrixDto svcRes;
            while (true)
            {
                try
                {
                    svcRes = client.GetMatrixAsync(resultId.ToString()).Result;
                    break;
                }
                catch (AggregateException ae)
                when (ae.InnerException is FaultException<string> fe && fe.Code.Name == "Accepted")
                {
                    Thread.Sleep(100);
                }
            }

            // local multiply
            var local = new double[rows * rows];
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < rows; j++)
                    for (int k = 0; k < cols; k++)
                        local[i * rows + j] += A[i * cols + k] * B[k * rows + j];

            bool pass = local.SequenceEqual(svcRes.Data);
            Console.WriteLine(pass
                ? $"[PASS] RandomMultiply {rows}×{cols}"
                : $"[FAIL] RandomMultiply {rows}×{cols}: mismatch");

            client.Close();
        }

        static void EdgeCaseMatrixCreation(int rows, int cols)
        {
            var client = new Service1Client();
            try
            {
                // will throw on invalid dims
                client.CreateMatrixAsync(new CreateMatrixRequest { Rows = rows, Columns = cols }).Wait();
                Console.WriteLine($"[FAIL] EdgeCase {rows}×{cols}: expected fault");
            }
            catch (AggregateException ae)
            when (ae.InnerException is FaultException<string> fe && fe.Code.Name == "InvalidArgument")
            {
                Console.WriteLine($"[PASS] EdgeCase {rows}×{cols}: correctly faulted");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] EdgeCase {rows}×{cols}: unexpected {ex.GetType().Name}");
            }
            finally
            {
                client.Close();
            }
        }

        static void StressTestLargeMatrix(int rows, int cols, int chunksCount)
        {
            var client = new Service1Client();
            var sw = System.Diagnostics.Stopwatch.StartNew();

            var resp = client.CreateMatrixAsync(new CreateMatrixRequest { Rows = rows, Columns = cols })
                             .Result;
            UploadInChunks(client, resp.MatrixId, rows, cols, chunksCount);

            sw.Stop();
            Console.WriteLine($"[STRESS] {rows}×{cols} in {chunksCount} chunks -> {sw.ElapsedMilliseconds} ms");
            client.Close();
        }

        static void UploadInChunks(
            Service1Client client,
            Guid matrixId,
            int rows,
            int cols,
            int chunksCount,
            double[] data = null
        )
        {
            int len = rows * cols;
            double[] buf = data ?? Enumerable.Range(1, len).Select(i => (double)i).ToArray();
            int chunkSz = len / chunksCount;

            for (int i = 0; i < chunksCount; i++)
            {
                int off = i * chunkSz;
                int take = (i == chunksCount - 1) ? len - off : chunkSz;
                var chunk = new MatrixChunk
                {
                    ChunkIndex = i,
                    TotalChunks = chunksCount,
                    Data = buf.Skip(off).Take(take).ToArray()
                };
                client.UploadMatrixChunk(matrixId.ToString(), chunk);
                Console.WriteLine($"  [chunk] {matrixId:N} {i + 1}/{chunksCount}");
            }
        }
        #endregion

        #region Mandelbrot Tests

        static void MandelbrotRaw(string fileName)
        {
            var client = new Service1Client();
            var imgId = client.GenerateMandelbrotAsync(new MandelbrotRequest
            {
                Width = 300,
                Height = 200,
                XMin = -2,
                XMax = 1,
                YMin = -1,
                YMax = 1,
                MaxIterations = 300,
                Threads = 4
            }).Result.ImageId;

            // poll raw endpoint
            byte[] img = null;
            while (true)
            {
                try
                {
                    img = client.GetMandelbrotRawAsync(imgId.ToString()).Result;
                    break;
                }
                catch (AggregateException ae)
                when (ae.InnerException is FaultException<string> fe && fe.Code.Name == "Accepted")
                {
                    Thread.Sleep(200);
                }
            }

            File.WriteAllBytes(fileName, img);
            Console.WriteLine($"[MB] Raw -> {fileName}");
            client.Close();
        }

        static void MandelbrotStream(string fileName)
        {
            var client = new Service1Client();
            var imgId = client.GenerateMandelbrotAsync(new MandelbrotRequest
            {
                Width = 400,
                Height = 400,
                XMin = -0.75,
                XMax = -0.74,
                YMin = 0.1,
                YMax = 0.11,
                MaxIterations = 1000,
                Threads = 4
            }).Result.ImageId;

            // poll streaming endpoint
            Stream s = null;
            while (true)
            {
                try
                {
                    s = client.GetMandelbrotAsync(imgId.ToString()).Result;
                    break;
                }
                catch (AggregateException ae)
                when (ae.InnerException is FaultException<string> fe && fe.Code.Name == "Accepted")
                {
                    Thread.Sleep(200);
                }
            }

            using (var fs = File.OpenWrite(fileName))
                s.CopyTo(fs);

            Console.WriteLine($"[MB] Stream -> {fileName}");
            client.Close();
        }

        static void MandelbrotTimeoutTest(int maxPolls, int delayMs)
        {
            var client = new Service1Client();
            var imgId = client.GenerateMandelbrotAsync(new MandelbrotRequest
            {
                Width = 2000,
                Height = 2000,
                XMin = -2,
                XMax = 1,
                YMin = -1,
                YMax = 1,
                MaxIterations = 5000,
                Threads = 4
            }).Result.ImageId;

            int polls = 0;
            try
            {
                while (polls++ < maxPolls)
                {
                    try
                    {
                        var _ = client.GetMandelbrotRawAsync(imgId.ToString()).Result;
                        Console.WriteLine($"[MB] Completed on poll {polls}");
                        return;
                    }
                    catch (AggregateException ae)
                    when (ae.InnerException is FaultException<string> fe && fe.Code.Name == "Accepted")
                    {
                        Thread.Sleep(delayMs);
                    }
                }
                Console.WriteLine($"[MB] Timeout after {maxPolls} polls (gave up)");
            }
            finally
            {
                client.Close();
            }
        }
        #endregion
    }
}
