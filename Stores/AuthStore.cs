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
        public static async Task<Auth?> Load()
        {
            return new Auth
            {
                id_token = await SecureStorage.Default.GetAsync("auth_data_id_token"),
                access_token = await SecureStorage.Default.GetAsync("auth_data_access_token"),
                refresh_token = await SecureStorage.Default.GetAsync("auth_data_refresh_token"),
                scope = await SecureStorage.Default.GetAsync("auth_data_scope"),
                token_type = await SecureStorage.Default.GetAsync("auth_data_token_type"),
                expires_in = int.Parse(await SecureStorage.Default.GetAsync("auth_data_expires_in") ?? "0"),
                created_at = int.Parse(await SecureStorage.Default.GetAsync("auth_data_created_at") ?? "0"),
            };
        }

        public static async Task Store(Auth auth)
        {
            await SecureStorage.Default.SetAsync("auth_data_id_token", auth.id_token);
            await SecureStorage.Default.SetAsync("auth_data_access_token", auth.access_token);
            await SecureStorage.Default.SetAsync("auth_data_refresh_token", auth.refresh_token);
            await SecureStorage.Default.SetAsync("auth_data_scope", auth.scope);
            await SecureStorage.Default.SetAsync("auth_data_token_type", auth.token_type);
            await SecureStorage.Default.SetAsync("auth_data_expires_in", auth.expires_in.ToString());
            await SecureStorage.Default.SetAsync("auth_data_created_at", auth.created_at.ToString());
        }

        public static async Task Delete()
        {
            var keys = new List<string>
            {
                "auth_data_id_token",
                "auth_data_access_token",
                "auth_data_refresh_token",
                "auth_data_scope",
                "auth_data_token_type",
                "auth_data_expires_in",
                "auth_data_created_at"
            };

            foreach (var key in keys)
            {
                SecureStorage.Remove(key);
            }
        }

        public static Task Update(Auth auth)
        {
            return Delete().ContinueWith(t => Store(auth));
        }
    }
}
