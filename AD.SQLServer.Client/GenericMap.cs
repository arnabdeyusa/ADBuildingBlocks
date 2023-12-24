using AD.SQLServer.Client.Extensions;
using Microsoft.Data.SqlClient;

namespace AD.SQLServer.Client
{
    public class GenericMap<T> : IMapper<T>
    {
        public List<T> MapFromDbResult(SqlDataReader reader)
        {
            var kvPairs = reader.Serialize();
            var result = kvPairs.ConvertToPOCO<List<T>>();
           return result;
        }
    }
}
