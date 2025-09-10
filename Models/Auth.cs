using System.Collections;

namespace MAD_Keuzedeel.Models
{
    public class Auth
    {
        public string id_token { get; set; }
        public string access_token { get; set; }
        public string refresh_token { get; set; }
        public string scope { get; set; }
        public string token_type { get; set; }
        public int expires_in { get; set; }
        public int created_at { get; set; }
        public DateTime? expires_at => DateTimeOffset.FromUnixTimeSeconds(created_at + expires_in).UtcDateTime;
    }
}