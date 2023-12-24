using AD.Memory.Cache;
using AD.SQLServer.Client.Constants;
using AD.SQLServer.Client.Extensions;
using AD.SQLServer.Client.GenericMethod;
using Microsoft.Data.SqlClient;

namespace AD.SQLServer.Client.SqlAccess
{
    public class SqlAccess
    {
        protected SqlConnectionStringBuilder builder;

        private static string server = null;
        private static string userId = null;
        private static string password = null;
        private static string catalog = null;

        public SqlAccess()
        {
            try
            {
                var dbcred = CommonCache.GetItemStatic<Dictionary<string, string>>(CacheConstants.DbCred);
                dbcred.TryGetValue(CacheConstants.UserName, out userId);
                dbcred.TryGetValue(CacheConstants.Pwd, out password);
                dbcred.TryGetValue(CacheConstants.Catalog, out catalog);
                dbcred.TryGetValue(CacheConstants.Server, out server);
                builder = GetConnectionStringBuilder();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: {0}", ex.Message);
            }
        }

        private SqlConnectionStringBuilder GetConnectionStringBuilder()
        {
            var builder = new SqlConnectionStringBuilder(GetConnectionString());
            builder.ConnectionString = "server=" + server + ";user id=" + userId + ";password=" + password + ";initial catalog=" + catalog;
            builder.Password = password;
            builder["Server"] = server;
            builder["Connect Timeout"] = 30;
            builder["Trusted_Connection"] = false;

            return builder;
        }

        private static string GetConnectionString()
        {
            return "Server=" + server + "; Integrated Security=SSPI; Initial Catalog=" + catalog;
        }

        public List<T> ExecuteSelect<T>(string query, IMapper<T> mapper, IDictionary<string, object> param = null)
        {
            var test = SelectFrom<T>(query, mapper, param);
            return test;
        }

        public bool ExcecuteUpdate(string selectQuery, IDictionary<string, object> param = null)
        {
            SqlConnection connection = new SqlConnection(builder.ConnectionString);
            try
            {
                using (SqlCommand command = new SqlCommand(selectQuery, connection))
                {
                    command.BuildCommand(param);
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                    return true;
                }
            }
            catch (Exception ex)
            {
                connection.Close();
                Console.WriteLine("Error in ExecuteUpdate: " + ex + "query: " + selectQuery);
                return false;
            }
        }

        /// <summary>
        /// Multiple result set, either query or SP
        /// </summary>
        /// <param name="selectQuery"></param>
        /// <returns></returns>
        public T MultipleSelect<T>(string selectQuery, IDictionary<string, object> param = null)
            where T: new()
        {
            var tables = new List<List<Dictionary<string, object>>>();
            SqlConnection connection = new SqlConnection(builder.ConnectionString);
            using (SqlCommand command = new SqlCommand(selectQuery, connection))
            {
                command.BuildCommand(param);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    do
                    {
                        var table = new List<Dictionary<string, object>>();                        
                        table.AddRange(reader.Serialize());
                        tables.Add(table);
                    } while (reader.NextResult());
                }
                connection.Close();
            }

            var result = GenericUtility.ConvertToPOCOList<T>(tables, EntityConstants.GenericPOCOMethod);
            return result;
        }       

        private List<T> SelectFrom<T>(string selectQuery, IMapper<T> mapper, IDictionary<string, object> param)
        {
            SqlConnection connection = new SqlConnection(builder.ConnectionString);
            List<T> values = new List<T>();
            try
            {
                using (SqlCommand command = new SqlCommand(selectQuery, connection))
                {
                    command.BuildCommand(param);
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        var mappedResults = mapper.MapFromDbResult(reader);
                        values.AddRange(mappedResults);
                        reader.NextResult();
                        connection.Close();
                        return values;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in SelectFrom: " + ex + "query: " + selectQuery);
                connection.Close();
                throw;
            }
        }
    }
}
