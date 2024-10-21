namespace Product.Model
{
    public class Common
    {
        public struct ApiResponse
        {
            public byte Status { get; set; }
            public string Message { get; set; }
            public string DetailedError { get; set; }
            public Object data { get; set; }
        }
        public enum StatusFlag : byte
        {
            Success = 1,
            Failed = 2,
            AlredyExist = 3,
            DependencyExist = 4,
            NotPermitted = 5,
        }
    }
}
