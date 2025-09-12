using System.Net.Http.Headers;

namespace MAD_Keuzedeel.Utils
{
    public static class ApiUtils
    {
        public static async Task<ReturnType?> FetchAndDeserialize<ReturnType, ApiResponseType>(
            Uri uri,
            string apiResponseDataKey,
            string authorisationToken = null
            )
        {
            try
            {
                using HttpClient _http = new();
                if (!string.IsNullOrEmpty(authorisationToken))
                    _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(MauiProgram.AuthService._AUTH.token_type, authorisationToken);

                HttpResponseMessage res = await _http.GetAsync(uri);
                string resString = await res.Content.ReadAsStringAsync();

                ReturnType result = JsonUtils.DeserializeReponseStringToObject<ReturnType, ApiResponseType>(resString, apiResponseDataKey);
                return result ?? default;
            }
            catch (Exception ex)
            {
                return default;
            }
        }

        public static string CreateQueryString(Dictionary<string, string> parameters)
        {
            if (parameters == null || parameters.Count == 0)
                return string.Empty;

            string queryString = "?";
            foreach (var param in parameters)
            {
                queryString += $"{Uri.EscapeDataString(param.Key)}={Uri.EscapeDataString(param.Value)}&";
            }

            return queryString.TrimEnd('&');
        }

    }
}
