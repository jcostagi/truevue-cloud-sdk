namespace TrueVUE.Cloud.SDK.API.Responses
{
    public class TagConfigurationResponse
    {
        public bool AllowUnknownProducts { get; set; }
        public int ProductMaxLength { get; set; }
        public int ProductMinLength { get; set; }
        public int RfidMaxLength { get; set; }
        public int RfidMinLength { get; set; }
        public string TagEncodeMethodType { get; set; }
    }
}
