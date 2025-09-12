using MAD_Keuzedeel.Models;
using MAD_Keuzedeel.Utils;
using Newtonsoft.Json;

namespace MAD_Keuzedeel.Stores
{
    class UserStore : IStore<User>
    {
        public static async Task<User?> Load() => JsonUtils.DeserializeStringToObject<User>(await SecureStorage.Default.GetAsync("user_data"));
        public static async Task Store(User user) => await SecureStorage.Default.SetAsync("user_data", JsonConvert.SerializeObject(user));
        public static async Task<bool> Delete() => SecureStorage.Remove("user_data");
    }
}
