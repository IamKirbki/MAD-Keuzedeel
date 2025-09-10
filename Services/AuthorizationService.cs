using MAD_Keuzedeel.env;
using MAD_Keuzedeel.Models;
using MAD_Keuzedeel.Stores;
using Microsoft.Maui.Authentication;
using System.Diagnostics;
using System.Net.Http.Json;

class AuthorizationService
{
    private readonly Dictionary<string, string> _PKCE = PkceUtils.GeneratePkceData();
    private Auth? _AUTH;
    protected User? _USER;

    public async Task InitializeAuthorisation()
    {
        if (!await AuthorizationUtils.IsAuthorised())
        {
            _AUTH = await GetAuthentication();
            _USER = await FetchUserData();
        } else
        {
            _AUTH = await AuthStore.Load();
            _USER = await UserStore.Load();
        }
    }

    private async Task<Auth> GetAuthentication()
    {
        string token = await FetchAuthorizationToken();
        return await FetchAuthData(token);
    }

    private async Task<User> FetchUserData()
    {
        try
        {
            if (!string.IsNullOrEmpty(_AUTH?.access_token))
            {

                if (_AUTH.expires_at < DateTime.UtcNow)
                    _AUTH = await RefreshAuthData();
            }
            else
            {
                throw new Exception("No authentication data found!");
            }

            using HttpClient client = new();
            client.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue(_AUTH.token_type, _AUTH.access_token);

            User user = await client.GetFromJsonAsync<User>(OAuth.UserInfoUrl);
            UserStore.Store(user);
            return user;

        }
        catch (TaskCanceledException e)
        {
            Console.WriteLine($"Failed to get user data: {e.Message}");
            return null;
        }
    }

    private async Task<string> FetchAuthorizationToken()
    {
        try
        {
            WebAuthenticatorResult authResult = await WebAuthenticator.Default.AuthenticateAsync(
                AuthorizationUtils.GenerateAuthUri(_PKCE),
                new Uri(OAuth.RedirectUri)
            );

            string returnedState = authResult.Properties["state"];
            string expectedState = _PKCE["state"];

            if (returnedState != expectedState)
                throw new Exception("State does not match!");

            string token = authResult.Properties["code"];
            if (string.IsNullOrEmpty(token))
                throw new Exception("The token is empty!");

            return token;
        }
        catch (TaskCanceledException e)
        {
            Console.WriteLine($"Failed to get authentication token: {e.Message}");
            return null;
        }
    }

    private async Task<Auth> FetchAuthData(string token)
    {
        try
        {
            Dictionary<string, string> requestData = AuthorizationUtils.GenerateRequestData(token, _PKCE["code_verifier"]);
            using HttpClient client = new();
            HttpResponseMessage response = await client.PostAsync(
                OAuth.TokenUrl,
                new FormUrlEncodedContent(requestData)
            );

            if (!response.IsSuccessStatusCode)
                throw new Exception($"Failed to fetch auth data: {response.ReasonPhrase}");
            
            Auth auth = await response.Content.ReadFromJsonAsync<Auth>();
            AuthStore.Store(auth);
            return auth;
        }
        catch (TaskCanceledException e)
        {
            Console.WriteLine($"Failed to get authentication data: {e.Message}");
            return null;
        }
    }

    private async Task<Auth> RefreshAuthData() {
        try
        {
            Dictionary<string, string> refreshData = AuthorizationUtils.GenerateRefreshData(_AUTH.refresh_token);
            using HttpClient client = new();
            HttpResponseMessage response = await client.PostAsync(
                OAuth.TokenUrl,
                new FormUrlEncodedContent(refreshData)
            );

            if (!response.IsSuccessStatusCode)
                throw new Exception($"Failed to refresh auth data: {response.ReasonPhrase}");

            Auth newAuth = await response.Content.ReadFromJsonAsync<Auth>();
            AuthStore.Store(newAuth);
            _AUTH = newAuth;
            return newAuth;
        }
        catch (TaskCanceledException e)
        {
            Console.WriteLine($"Failed to Refresh authentication data: {e.Message}");

            Console.WriteLine("Deleting old authentication data...");
            AuthStore.Delete();
            _AUTH = null;
            Console.WriteLine("Old authentication data deleted.");

            Console.WriteLine("Deleting old user data...");
            UserStore.Delete();
            _USER = null;
            Console.WriteLine("Old user data deleted.");

            return null;
        }
    }
}