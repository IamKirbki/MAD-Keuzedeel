using MAD_Keuzedeel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAD_Keuzedeel.Stores
{
    class UserStore : IStore<User>
    {
        public static async Task<User?> Load() => JsonConvert.DeserializeObject<User>(await SecureStorage.Default.GetAsync("user_data")) ?? null;
        public static async Task<User?> Store(User user) => await SecureStorage.Default.SetAsync("user_data", JsonConvert.SerializeObject(user));
        public static async Task Delete() => await SecureStorage.Remove("user_data");
        public static Task Update(User user) => Delete().ContinueWith(t => Store(user));
    }
}
