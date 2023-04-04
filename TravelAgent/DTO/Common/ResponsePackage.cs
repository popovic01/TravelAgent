namespace TravelAgent.DTO.Common
{
    public class ResponsePackage<T> : ResponsePackageNoData
    {
        public T TransferObject { get; set; }

        public ResponsePackage() 
        {
            Status = 200;
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
    }
}
