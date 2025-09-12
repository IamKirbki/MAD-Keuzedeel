using MAD_Keuzedeel.Models;
using MAD_Keuzedeel.Utils;
using Newtonsoft.Json;

namespace MAD_Keuzedeel.Stores
{
    class AuthStore : IStore<Auth>
    {
        public static async Task<Auth?> Load() => JsonUtils.DeserializeStringToObject<Auth>(await SecureStorage.Default.GetAsync("auth_data"));
        public static async Task Store(Auth auth) => await SecureStorage.Default.SetAsync("auth_data", JsonConvert.SerializeObject(auth));
        public static async Task<bool> Delete() => SecureStorage.Remove("auth_data");
    }
}
