namespace TravelAgent.Helpers
{
    public interface ICommonHelper
    {
        public string RandomString(int length);
        public void ExecuteProcedure(string procedureName, int offerId, int isInsert);
    }
}
