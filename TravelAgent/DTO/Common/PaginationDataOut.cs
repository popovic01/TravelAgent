namespace TravelAgent.DTO.Common
{
    public class PaginationDataOut<DataOut>
    {
        public int Count { get; set; }

        public List<DataOut> Data { get; set; }

        public PaginationDataOut()
        {
            Data = new List<DataOut>();
        }
    }
}
