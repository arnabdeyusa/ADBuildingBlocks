
using Newtonsoft.Json;
using System.Reflection;

namespace AD.SQLServer.Client.GenericMethod
{
    public class GenericUtility
    {
        public static T ConvertToPOCO<T>(IEnumerable<Dictionary<string, object>> rows)
        {
            var json = JsonConvert.SerializeObject(rows);
            var result = JsonConvert.DeserializeObject<T>(json);
            return result;
        }

        /// <summary>
        /// Use for multiple Select
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static T ConvertToPOCOList<T>(List<List<Dictionary<string, object>>> rows, string methodName)
            where T : new()
        {
            var result = new T();
            MethodInfo method = typeof(GenericUtility).GetMethod(methodName);
            var properties = result.GetType().GetProperties();
            var types = result.GetType().GetProperties().Select(t => t.PropertyType);
            int index = 0;
            foreach (Type type in types)
            {
                MethodInfo genericMethod = method.MakeGenericMethod(type);
                var param = new Object[1];
                param[0] = rows[index];
                var obj = genericMethod.Invoke(null, param); // No target
                properties[index].SetValue(result, obj);
                index++;
            }
           
            return result;
        }
    }
}
