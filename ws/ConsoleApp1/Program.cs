using System;
using System.IO;
using System.ServiceModel;
using System.Threading.Tasks;
using ConsoleApp1.MojService;

namespace ConsoleApp1
{
    class Program
    {
        static void Main()
        {
            Task.WaitAll(
                Task.Run(() => SingleMatrixAndMandelbrot(4, 4, 2, 300, 200, "mandelbrot1.bmp")),
                Task.Run(() => SingleMatrixAndMandelbrot(5, 5, 5, 400, 300, "mandelbrot2.bmp")),
                Task.Run(() => TwoMatricesMultiplyAndFetchChunks(3, 3, 2)),
                Task.Run(() => TwoMatricesMultiplyAndFetchChunks(4, 2, 2))
            );

            Console.WriteLine("\nAll tasks completed.\n");
        }

        static void SingleMatrixAndMandelbrot(int rows, int cols, int chunksCount, int imgW, int imgH, string imgFile)
        {
            var client = new Service1Client();

            var matrixResp = client.CreateMatrix(new CreateMatrixRequest { Rows = rows, Columns = cols });
            Guid matrixId = matrixResp.MatrixId;
            SendMatrixInChunks(client, matrixId, rows, cols, chunksCount);

            Console.WriteLine($"[{matrixId}] Matrix send.");

            var mbResp = client.GenerateMandelbrot(new MandelbrotRequest
            {
                Width = imgW,
                Height = imgH,
                XMin = -2.0,
                XMax = 1.0,
                YMin = -1.0,
                YMax = 1.0,
                MaxIterations = 500
            });

            byte[] imageBytes;
            while (true)
            {
                try
                {
                    imageBytes = client.GetMandelbrotRaw(mbResp.ImageId.ToString());
                    break;
                }
                catch (FaultException<string> fe) when (fe.Code.Name == "Accepted")
                {
                    Task.Delay(200).Wait();
                }
            }

            File.WriteAllBytes(imgFile, imageBytes);
            Console.WriteLine($"[{matrixId}] Mandelbrot saved: {imgFile}");
            client.Close();
        }

        static void TwoMatricesMultiplyAndFetchChunks(int size, int chunksCount, int resultChunks)
        {
            var client = new Service1Client();

            var respA = client.CreateMatrix(new CreateMatrixRequest { Rows = size, Columns = size });
            Guid matrixA = respA.MatrixId;
            SendMatrixInChunks(client, matrixA, size, size, chunksCount);

            var respB = client.CreateMatrix(new CreateMatrixRequest { Rows = size, Columns = size });
            Guid matrixB = respB.MatrixId;
            SendMatrixInChunks(client, matrixB, size, size, chunksCount);

            Console.WriteLine($"[{matrixA}] Matrix A i B send.");

            var mulResp = client.MultiplyMatrices(new MultiplyRequest
            {
                LeftMatrixId = matrixA,
                RightMatrixId = matrixB
            });
            Guid resultId = mulResp.ResultMatrixId;

            MatrixDto resultMatrix;
            while (true)
            {
                try
                {
                    resultMatrix = client.GetMatrix(resultId.ToString());
                    break;
                }
                catch (FaultException<string> fe) when (fe.Code.Name == "Accepted")
                {
                    Task.Delay(200).Wait();
                }
            }

            Console.WriteLine($"[{matrixA}] Got resulting matrix:");
            PrintMatrix(resultMatrix);
            client.Close();
        }

        static void PrintMatrix(MatrixDto matrix)
        {
            for (int i = 0; i < matrix.Rows; i++)
            {
                for (int j = 0; j < matrix.Columns; j++)
                    Console.Write(matrix.Data[i * matrix.Columns + j] + "\t");
                Console.WriteLine();
            }
        }

        static void SendMatrixInChunks(Service1Client client, Guid matrixId, int rows, int cols, int chunksCount, double[] predefinedData = null)
        {
            double[] data = predefinedData ?? new double[rows * cols];
            if (predefinedData == null)
            {
                for (int i = 0; i < data.Length; i++)
                    data[i] = i + 1;
            }

            int chunkSize = data.Length / chunksCount;
            for (int idx = 0; idx < chunksCount; idx++)
            {
                int offset = idx * chunkSize;
                int len = (idx == chunksCount - 1) ? data.Length - offset : chunkSize;
                var chunk = new MatrixChunk
                {
                    ChunkIndex = idx,
                    TotalChunks = chunksCount,
                    Data = new double[len]
                };
                Array.Copy(data, offset, chunk.Data, 0, len);
                client.UploadMatrixChunk(matrixId.ToString(), chunk);
                Console.WriteLine($"[{matrixId}] Chunk {idx + 1}/{chunksCount} send.");
            }
        }
    }
}
