using System;
using System.IO;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace WcfService1
{
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "matrices",
            RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        Task<CreateMatrixResponse> CreateMatrixAsync(CreateMatrixRequest req);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "matrices/{matrixId}/chunks",
            RequestFormat = WebMessageFormat.Json)]
        void UploadMatrixChunk(string matrixId, MatrixChunk chunk);

        [OperationContract]
        [WebGet(UriTemplate = "matrices/{matrixId}", ResponseFormat = WebMessageFormat.Json)]
        Task<MatrixDto> GetMatrixAsync(string matrixId);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "matrices/multiply",
            RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        Task<MultiplyResponse> MultiplyMatricesAsync(MultiplyRequest req);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "mandelbrot",
            RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        Task<MandelbrotResponse> GenerateMandelbrotAsync(MandelbrotRequest req);

        [OperationContract]
        //[WebGet(UriTemplate = "mandelbrot/{imageId}")]
        Task<Stream> GetMandelbrotAsync(string imageId);

        [OperationContract]
        [WebGet(UriTemplate = "mandelbrot/{imageId}/raw", ResponseFormat = WebMessageFormat.Json)]
        Task<byte[]> GetMandelbrotRawAsync(string imageId);
    }

    [DataContract]
    public class CompositeType
    {
        [DataMember] public bool BoolValue { get; set; } = true;
        [DataMember] public string StringValue { get; set; } = "Hello ";
    }

    [DataContract]
    public class CreateMatrixRequest
    {
        [DataMember] public int Rows { get; set; }
        [DataMember] public int Columns { get; set; }
    }

    [DataContract]
    public class CreateMatrixResponse
    {
        [DataMember] public Guid MatrixId { get; set; }
        public CreateMatrixResponse(Guid id) => MatrixId = id;
    }

    [DataContract]
    public class MatrixDto
    {
        [DataMember] public int Rows { get; set; }
        [DataMember] public int Columns { get; set; }
        [DataMember] public double[] Data { get; set; }
    }

    [DataContract]
    public class MatrixChunk
    {
        [DataMember] public int ChunkIndex { get; set; }
        [DataMember] public int TotalChunks { get; set; }
        [DataMember] public double[] Data { get; set; }
    }

    [DataContract]
    public class MultiplyRequest
    {
        [DataMember] public Guid LeftMatrixId { get; set; }
        [DataMember] public Guid RightMatrixId { get; set; }
    }

    [DataContract]
    public class MultiplyResponse
    {
        [DataMember] public Guid ResultMatrixId { get; set; }
        public MultiplyResponse(Guid id) => ResultMatrixId = id;
    }

    [DataContract]
    public class MandelbrotRequest
    {
        [DataMember] public int Width { get; set; }
        [DataMember] public int Height { get; set; }
        [DataMember] public double XMin { get; set; }
        [DataMember] public double XMax { get; set; }
        [DataMember] public double YMin { get; set; }
        [DataMember] public double YMax { get; set; }
        [DataMember] public int MaxIterations { get; set; }
        [DataMember] public int Threads { get; set; } = 1;
    }

    [DataContract]
    public class MandelbrotResponse
    {
        [DataMember] public Guid ImageId { get; set; }
        public MandelbrotResponse(Guid id) => ImageId = id;
    }
}
