namespace TravelAgent.DTO.Common
{
    public class ResponsePackage<T> : ResponsePackageNoData
    {
        public T TransferObject { get; set; }

        public ResponsePackage() 
        {
            Status = 200;
        }

        public ResponsePackage(int status, string message)
        {
            Status = status;
            Message = message;
        }

        public ResponsePackage(T data, int status = 200, string message = "")
        {
            TransferObject = data;
            Status = status;
            Message = message;
        }
    }

    public class ResponsePackageNoData
    {
        public string Message { get; set; }
        public int Status { get; set; }

        public ResponsePackageNoData()
        {
            Status = 200;
            Message = "";
        }

        public ResponsePackageNoData(int status, string message)
        {
            Status = status;
            Message = message;
        }
    }
}
