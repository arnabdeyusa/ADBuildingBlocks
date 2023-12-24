using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System.Data;

namespace AD.SQLServer.Client.Extensions
{
    public static class DataRowExtensions
    {
        public static bool TryGetColumnName(this DataRowCollection rows, string columnName)
        {
            bool result = false;
            for(int index = 0; index < rows.Count;index++)
            {
                if (columnName == Convert.ToString(rows[index].ItemArray[0]))
                {
                    result = true;
                    break;
                }
            }

            return result;
        }

        public static IEnumerable<Dictionary<string, object>> Serialize(this SqlDataReader reader)
        {
            var results = new List<Dictionary<string, object>>();
            var cols = new List<string>();
            for (var i = 0; i < reader.FieldCount; i++)
                cols.Add(reader.GetName(i));

            while (reader.Read())
                results.Add(SerializeRow(cols, reader));

            return results;
        }
        private static Dictionary<string, object> SerializeRow(IEnumerable<string> cols,
                                                        SqlDataReader reader)
        {
            var result = new Dictionary<string, object>();
            foreach (var col in cols)
                result.Add(col, reader[col]);
            return result;
        }

        public static T ConvertToPOCO<T>(this IEnumerable<Dictionary<string, object>> rows)
        {            
            var json = JsonConvert.SerializeObject(rows);
            var result = JsonConvert.DeserializeObject<T>(json);
            return result;
        }
    }
}
