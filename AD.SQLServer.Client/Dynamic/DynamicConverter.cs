using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AD.SQLServer.Client.Dynamic
{
    public class DynamicConverter
    {
        public static ExpandoObject? JsonToExpandoObject(string jsonString)
        {
            var result =  JsonConvert.DeserializeObject<ExpandoObject>(jsonString);
            return result;
        }

        public static string ExpandoObjectToJson(ExpandoObject expandoObject)
        {
            return JsonConvert.SerializeObject(expandoObject);
        }
    }
}
