using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

public class FirebaseAuthService
{
    private static readonly string firebaseApiKey = "AIzaSyB5V_QFnzAEpP6-QHQQzT08cRvfwn5SI6w"; //  API key

    public static async Task<string> RegisterUserAsync(string email, string password)
    {
        var client = new HttpClient();
        var jsonContent = JsonConvert.SerializeObject(new
        {
            email = email,
            password = password,
            returnSecureToken = true
        });

        var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
        var response = await client.PostAsync($"https://identitytoolkit.googleapis.com/v1/accounts:signUp?key={firebaseApiKey}", content);

        if (response.IsSuccessStatusCode)
        {
            var responseData = await response.Content.ReadAsStringAsync();
            return responseData; // You can return token or other info from Firebase
        }
        else
        {
            var error = await response.Content.ReadAsStringAsync();
            throw new Exception($"Registration failed: {error}");
        }
    }

    public static async Task<string> LoginUserAsync(string email, string password)
    {
        var client = new HttpClient();
        var jsonContent = JsonConvert.SerializeObject(new
        {
            email = email,
            password = password,
            returnSecureToken = true
        });

        var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
        var response = await client.PostAsync($"https://identitytoolkit.googleapis.com/v1/accounts:signInWithPassword?key={firebaseApiKey}", content);

        if (response.IsSuccessStatusCode)
        {
            var responseData = await response.Content.ReadAsStringAsync();
            return responseData; // Token will be part of the response
        }
        else
        {
            var error = await response.Content.ReadAsStringAsync();
            throw new Exception($"Login failed: {error}");
        }
    }
}
