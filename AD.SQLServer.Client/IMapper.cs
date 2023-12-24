using Microsoft.Data.SqlClient;

namespace AD.SQLServer.Client
{
    public interface IMapper<T>
    {
        List<T> MapFromDbResult(SqlDataReader reader);
    }
}
