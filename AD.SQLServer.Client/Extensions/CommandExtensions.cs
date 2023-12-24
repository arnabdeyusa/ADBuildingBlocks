using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AD.SQLServer.Client.Extensions
{
    public static class CommandExtensions
    {
        public static void BuildCommand(this SqlCommand command, IDictionary<string, object> param)
        {
            if (!(param is null))
                foreach (var item in param)
                {
                    command.Parameters.AddWithValue(item.Key, item.Value);
                }
        }
    }
}
