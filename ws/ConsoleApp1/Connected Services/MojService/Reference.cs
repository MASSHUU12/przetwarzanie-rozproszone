﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ConsoleApp1.MojService {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="CreateMatrixRequest", Namespace="http://schemas.datacontract.org/2004/07/WcfService1")]
    [System.SerializableAttribute()]
    public partial class CreateMatrixRequest : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int ColumnsField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int RowsField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Columns {
            get {
                return this.ColumnsField;
            }
            set {
                if ((this.ColumnsField.Equals(value) != true)) {
                    this.ColumnsField = value;
                    this.RaisePropertyChanged("Columns");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Rows {
            get {
                return this.RowsField;
            }
            set {
                if ((this.RowsField.Equals(value) != true)) {
                    this.RowsField = value;
                    this.RaisePropertyChanged("Rows");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="CreateMatrixResponse", Namespace="http://schemas.datacontract.org/2004/07/WcfService1")]
    [System.SerializableAttribute()]
    public partial class CreateMatrixResponse : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Guid MatrixIdField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Guid MatrixId {
            get {
                return this.MatrixIdField;
            }
            set {
                if ((this.MatrixIdField.Equals(value) != true)) {
                    this.MatrixIdField = value;
                    this.RaisePropertyChanged("MatrixId");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="MatrixChunk", Namespace="http://schemas.datacontract.org/2004/07/WcfService1")]
    [System.SerializableAttribute()]
    public partial class MatrixChunk : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int ChunkIndexField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private double[] DataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int TotalChunksField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int ChunkIndex {
            get {
                return this.ChunkIndexField;
            }
            set {
                if ((this.ChunkIndexField.Equals(value) != true)) {
                    this.ChunkIndexField = value;
                    this.RaisePropertyChanged("ChunkIndex");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public double[] Data {
            get {
                return this.DataField;
            }
            set {
                if ((object.ReferenceEquals(this.DataField, value) != true)) {
                    this.DataField = value;
                    this.RaisePropertyChanged("Data");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int TotalChunks {
            get {
                return this.TotalChunksField;
            }
            set {
                if ((this.TotalChunksField.Equals(value) != true)) {
                    this.TotalChunksField = value;
                    this.RaisePropertyChanged("TotalChunks");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="MatrixDto", Namespace="http://schemas.datacontract.org/2004/07/WcfService1")]
    [System.SerializableAttribute()]
    public partial class MatrixDto : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int ColumnsField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private double[] DataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int RowsField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Columns {
            get {
                return this.ColumnsField;
            }
            set {
                if ((this.ColumnsField.Equals(value) != true)) {
                    this.ColumnsField = value;
                    this.RaisePropertyChanged("Columns");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public double[] Data {
            get {
                return this.DataField;
            }
            set {
                if ((object.ReferenceEquals(this.DataField, value) != true)) {
                    this.DataField = value;
                    this.RaisePropertyChanged("Data");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Rows {
            get {
                return this.RowsField;
            }
            set {
                if ((this.RowsField.Equals(value) != true)) {
                    this.RowsField = value;
                    this.RaisePropertyChanged("Rows");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="MultiplyRequest", Namespace="http://schemas.datacontract.org/2004/07/WcfService1")]
    [System.SerializableAttribute()]
    public partial class MultiplyRequest : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Guid LeftMatrixIdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Guid RightMatrixIdField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Guid LeftMatrixId {
            get {
                return this.LeftMatrixIdField;
            }
            set {
                if ((this.LeftMatrixIdField.Equals(value) != true)) {
                    this.LeftMatrixIdField = value;
                    this.RaisePropertyChanged("LeftMatrixId");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Guid RightMatrixId {
            get {
                return this.RightMatrixIdField;
            }
            set {
                if ((this.RightMatrixIdField.Equals(value) != true)) {
                    this.RightMatrixIdField = value;
                    this.RaisePropertyChanged("RightMatrixId");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="MultiplyResponse", Namespace="http://schemas.datacontract.org/2004/07/WcfService1")]
    [System.SerializableAttribute()]
    public partial class MultiplyResponse : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Guid ResultMatrixIdField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Guid ResultMatrixId {
            get {
                return this.ResultMatrixIdField;
            }
            set {
                if ((this.ResultMatrixIdField.Equals(value) != true)) {
                    this.ResultMatrixIdField = value;
                    this.RaisePropertyChanged("ResultMatrixId");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="MandelbrotRequest", Namespace="http://schemas.datacontract.org/2004/07/WcfService1")]
    [System.SerializableAttribute()]
    public partial class MandelbrotRequest : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int HeightField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int MaxIterationsField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int ThreadsField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int WidthField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private double XMaxField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private double XMinField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private double YMaxField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private double YMinField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Height {
            get {
                return this.HeightField;
            }
            set {
                if ((this.HeightField.Equals(value) != true)) {
                    this.HeightField = value;
                    this.RaisePropertyChanged("Height");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int MaxIterations {
            get {
                return this.MaxIterationsField;
            }
            set {
                if ((this.MaxIterationsField.Equals(value) != true)) {
                    this.MaxIterationsField = value;
                    this.RaisePropertyChanged("MaxIterations");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Threads {
            get {
                return this.ThreadsField;
            }
            set {
                if ((this.ThreadsField.Equals(value) != true)) {
                    this.ThreadsField = value;
                    this.RaisePropertyChanged("Threads");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Width {
            get {
                return this.WidthField;
            }
            set {
                if ((this.WidthField.Equals(value) != true)) {
                    this.WidthField = value;
                    this.RaisePropertyChanged("Width");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public double XMax {
            get {
                return this.XMaxField;
            }
            set {
                if ((this.XMaxField.Equals(value) != true)) {
                    this.XMaxField = value;
                    this.RaisePropertyChanged("XMax");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public double XMin {
            get {
                return this.XMinField;
            }
            set {
                if ((this.XMinField.Equals(value) != true)) {
                    this.XMinField = value;
                    this.RaisePropertyChanged("XMin");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public double YMax {
            get {
                return this.YMaxField;
            }
            set {
                if ((this.YMaxField.Equals(value) != true)) {
                    this.YMaxField = value;
                    this.RaisePropertyChanged("YMax");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public double YMin {
            get {
                return this.YMinField;
            }
            set {
                if ((this.YMinField.Equals(value) != true)) {
                    this.YMinField = value;
                    this.RaisePropertyChanged("YMin");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="MandelbrotResponse", Namespace="http://schemas.datacontract.org/2004/07/WcfService1")]
    [System.SerializableAttribute()]
    public partial class MandelbrotResponse : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Guid ImageIdField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Guid ImageId {
            get {
                return this.ImageIdField;
            }
            set {
                if ((this.ImageIdField.Equals(value) != true)) {
                    this.ImageIdField = value;
                    this.RaisePropertyChanged("ImageId");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="MojService.IService1")]
    public interface IService1 {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/CreateMatrix", ReplyAction="http://tempuri.org/IService1/CreateMatrixResponse")]
        ConsoleApp1.MojService.CreateMatrixResponse CreateMatrix(ConsoleApp1.MojService.CreateMatrixRequest req);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/CreateMatrix", ReplyAction="http://tempuri.org/IService1/CreateMatrixResponse")]
        System.Threading.Tasks.Task<ConsoleApp1.MojService.CreateMatrixResponse> CreateMatrixAsync(ConsoleApp1.MojService.CreateMatrixRequest req);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/UploadMatrixChunk", ReplyAction="http://tempuri.org/IService1/UploadMatrixChunkResponse")]
        void UploadMatrixChunk(string matrixId, ConsoleApp1.MojService.MatrixChunk chunk);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/UploadMatrixChunk", ReplyAction="http://tempuri.org/IService1/UploadMatrixChunkResponse")]
        System.Threading.Tasks.Task UploadMatrixChunkAsync(string matrixId, ConsoleApp1.MojService.MatrixChunk chunk);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetMatrix", ReplyAction="http://tempuri.org/IService1/GetMatrixResponse")]
        ConsoleApp1.MojService.MatrixDto GetMatrix(string matrixId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetMatrix", ReplyAction="http://tempuri.org/IService1/GetMatrixResponse")]
        System.Threading.Tasks.Task<ConsoleApp1.MojService.MatrixDto> GetMatrixAsync(string matrixId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/MultiplyMatrices", ReplyAction="http://tempuri.org/IService1/MultiplyMatricesResponse")]
        ConsoleApp1.MojService.MultiplyResponse MultiplyMatrices(ConsoleApp1.MojService.MultiplyRequest req);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/MultiplyMatrices", ReplyAction="http://tempuri.org/IService1/MultiplyMatricesResponse")]
        System.Threading.Tasks.Task<ConsoleApp1.MojService.MultiplyResponse> MultiplyMatricesAsync(ConsoleApp1.MojService.MultiplyRequest req);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GenerateMandelbrot", ReplyAction="http://tempuri.org/IService1/GenerateMandelbrotResponse")]
        ConsoleApp1.MojService.MandelbrotResponse GenerateMandelbrot(ConsoleApp1.MojService.MandelbrotRequest req);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GenerateMandelbrot", ReplyAction="http://tempuri.org/IService1/GenerateMandelbrotResponse")]
        System.Threading.Tasks.Task<ConsoleApp1.MojService.MandelbrotResponse> GenerateMandelbrotAsync(ConsoleApp1.MojService.MandelbrotRequest req);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetMandelbrot", ReplyAction="http://tempuri.org/IService1/GetMandelbrotResponse")]
        System.IO.Stream GetMandelbrot(string imageId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetMandelbrot", ReplyAction="http://tempuri.org/IService1/GetMandelbrotResponse")]
        System.Threading.Tasks.Task<System.IO.Stream> GetMandelbrotAsync(string imageId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetMandelbrotRaw", ReplyAction="http://tempuri.org/IService1/GetMandelbrotRawResponse")]
        byte[] GetMandelbrotRaw(string imageId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetMandelbrotRaw", ReplyAction="http://tempuri.org/IService1/GetMandelbrotRawResponse")]
        System.Threading.Tasks.Task<byte[]> GetMandelbrotRawAsync(string imageId);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IService1Channel : ConsoleApp1.MojService.IService1, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class Service1Client : System.ServiceModel.ClientBase<ConsoleApp1.MojService.IService1>, ConsoleApp1.MojService.IService1 {
        
        public Service1Client() {
        }
        
        public Service1Client(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public Service1Client(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public Service1Client(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public Service1Client(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public ConsoleApp1.MojService.CreateMatrixResponse CreateMatrix(ConsoleApp1.MojService.CreateMatrixRequest req) {
            return base.Channel.CreateMatrix(req);
        }
        
        public System.Threading.Tasks.Task<ConsoleApp1.MojService.CreateMatrixResponse> CreateMatrixAsync(ConsoleApp1.MojService.CreateMatrixRequest req) {
            return base.Channel.CreateMatrixAsync(req);
        }
        
        public void UploadMatrixChunk(string matrixId, ConsoleApp1.MojService.MatrixChunk chunk) {
            base.Channel.UploadMatrixChunk(matrixId, chunk);
        }
        
        public System.Threading.Tasks.Task UploadMatrixChunkAsync(string matrixId, ConsoleApp1.MojService.MatrixChunk chunk) {
            return base.Channel.UploadMatrixChunkAsync(matrixId, chunk);
        }
        
        public ConsoleApp1.MojService.MatrixDto GetMatrix(string matrixId) {
            return base.Channel.GetMatrix(matrixId);
        }
        
        public System.Threading.Tasks.Task<ConsoleApp1.MojService.MatrixDto> GetMatrixAsync(string matrixId) {
            return base.Channel.GetMatrixAsync(matrixId);
        }
        
        public ConsoleApp1.MojService.MultiplyResponse MultiplyMatrices(ConsoleApp1.MojService.MultiplyRequest req) {
            return base.Channel.MultiplyMatrices(req);
        }
        
        public System.Threading.Tasks.Task<ConsoleApp1.MojService.MultiplyResponse> MultiplyMatricesAsync(ConsoleApp1.MojService.MultiplyRequest req) {
            return base.Channel.MultiplyMatricesAsync(req);
        }
        
        public ConsoleApp1.MojService.MandelbrotResponse GenerateMandelbrot(ConsoleApp1.MojService.MandelbrotRequest req) {
            return base.Channel.GenerateMandelbrot(req);
        }
        
        public System.Threading.Tasks.Task<ConsoleApp1.MojService.MandelbrotResponse> GenerateMandelbrotAsync(ConsoleApp1.MojService.MandelbrotRequest req) {
            return base.Channel.GenerateMandelbrotAsync(req);
        }
        
        public System.IO.Stream GetMandelbrot(string imageId) {
            return base.Channel.GetMandelbrot(imageId);
        }
        
        public System.Threading.Tasks.Task<System.IO.Stream> GetMandelbrotAsync(string imageId) {
            return base.Channel.GetMandelbrotAsync(imageId);
        }
        
        public byte[] GetMandelbrotRaw(string imageId) {
            return base.Channel.GetMandelbrotRaw(imageId);
        }
        
        public System.Threading.Tasks.Task<byte[]> GetMandelbrotRawAsync(string imageId) {
            return base.Channel.GetMandelbrotRawAsync(imageId);
        }
    }
}
