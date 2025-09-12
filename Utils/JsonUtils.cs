using Newtonsoft.Json;
using System.Diagnostics;

namespace MAD_Keuzedeel.Utils
{
    static class JsonUtils
    {
        public static Object? DeserializeStringToObject<Object>(string jsonString)
        {
            if (string.IsNullOrEmpty(jsonString))
                return default;

            return JsonConvert.DeserializeObject<Object>(jsonString);
        }

        public static ReturnType? DeserializeReponseStringToObject<ReturnType, ResponseType>(string jsonString, string dataName)
        {
            if (string.IsNullOrWhiteSpace(jsonString))
                return default;

            try
            {
                var response = JsonConvert.DeserializeObject<ResponseType>(jsonString);
                return (ReturnType?)(response?.GetType().GetProperty(dataName)?.GetValue(response));
            }
            catch (JsonException e)
            {
                Debug.WriteLine(e);
                return default;
            }
        }

    }
}
