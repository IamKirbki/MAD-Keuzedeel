using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAD_Keuzedeel.Utils
{
    static class JsonUtils
    {
        public static T? DeserializeStringToObject<T>(string jsonString)
        {
            if (string.IsNullOrEmpty(jsonString))
                return default;

            return JsonConvert.DeserializeObject<T>(jsonString);
        }
    }
}
