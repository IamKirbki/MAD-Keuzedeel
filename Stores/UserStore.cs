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
        public static async Task<User?> Load()
        {
            return new User
            {
                id = int.Parse(await SecureStorage.Default.GetAsync("user_data_id") ?? "0"),
                name = await SecureStorage.Default.GetAsync("user_data_name"),
                email = await SecureStorage.Default.GetAsync("user_data_email"),
                username = await SecureStorage.Default.GetAsync("user_data_username"),
                avatar_url = await SecureStorage.Default.GetAsync("user_data_avatar_url")
            };
        }
        public static async Task Store(User user)
        {
            await SecureStorage.SetAsync("user_data_id", user.id.ToString());
            await SecureStorage.SetAsync("user_data_name", user.name ?? "");
            await SecureStorage.SetAsync("user_data_email", user.email ?? "");
            await SecureStorage.SetAsync("user_data_username", user.username ?? "");
            await SecureStorage.SetAsync("user_data_avatar_url", user.avatar_url ?? "");
        }

        public static async Task Delete()
        {
            var keys = new List<string>
            {
                "user_data_id",
                "user_data_name",
                "user_data_email",
                "user_data_username",
                "user_data_avatar_url"
            };
            foreach (var key in keys)
            {
                SecureStorage.Remove(key);
            }
        }
        public static Task Update(User user)
        {
            return Delete().ContinueWith(t => Store(user));
        }
    }
}
