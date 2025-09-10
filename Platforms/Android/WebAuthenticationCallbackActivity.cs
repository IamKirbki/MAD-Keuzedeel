using Android.App;
using Android.Content;
using Android.Content.PM;

namespace MAD_Keuzedeel;

[Activity(NoHistory = true, LaunchMode = LaunchMode.SingleTop, Exported = true)]
[IntentFilter(new[] { Intent.ActionView },
              Categories = new[] { Intent.CategoryDefault, Intent.CategoryBrowsable },
              DataScheme = "mad-keuzedeel",
              DataHost = "auth")]
public class WebAuthenticationCallbackActivity : Microsoft.Maui.Authentication.WebAuthenticatorCallbackActivity
{
    //const string CALLBACK_SCHEME = "mad-keuzedeel";
    //const string DATAHOST_SCHEME = "auth";
}