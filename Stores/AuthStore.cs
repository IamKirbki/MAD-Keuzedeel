using MAD_Keuzedeel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAD_Keuzedeel.Stores
{
    class AuthStore : IStore<Auth>
    {
        public static async Task<Auth?> Load() => JsonConvert.DeserializeObject<Auth>(await SecureStorage.Default.GetAsync("auth_data")) ?? null;
        public static async Task Store(Auth auth) => await SecureStorage.Default.SetAsync("auth_data", JsonConvert.SerializeObject(auth));
        public static async Task Delete() => await SecureStorage.Remove("auth_data");
        public static Task Update(Auth auth) => Delete().ContinueWith(t => Store(auth));
    }
}
