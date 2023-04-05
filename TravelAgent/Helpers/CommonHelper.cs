using Microsoft.Data.SqlClient;
using System.Data;

namespace TravelAgent.Helpers
{
    public class CommonHelper : ICommonHelper
    {
        private readonly IConfiguration _config;

        public CommonHelper(IConfiguration config)
        {
            _config = config;
        }

        public string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[new Random().Next(s.Length)]).ToArray());
        }

        public void ExecuteProcedure(string procedureName, int offerId, int isInsert)
        {
            string connectionString = _config.GetSection("ConnectionStrings:DefaultConnection").Value;
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(procedureName, connection);

            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@OfferId", offerId);
            command.Parameters.AddWithValue("@IsInsert", isInsert);

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }
    }
}
