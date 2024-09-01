using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;




namespace JokeUI.Services;

public class UserService
{
    // private readonly HttpClient _httpClient;
    // private readonly NavigationManager _navigationManager;
    // private readonly IJSRuntime _jsRuntime;

    // public UserService(HttpClient httpClient, NavigationManager navigationManager, IJSRuntime jsRuntime)
    // {
    //     _httpClient = httpClient;
    //     _navigationManager = navigationManager;
    //     _jsRuntime = jsRuntime;
    // }

    // public async Task<bool> RegisterUser(UserDTO user)
    // {
    //     var response = await _httpClient.PostAsJsonAsync("api/users", user);
    //     if (response.IsSuccessStatusCode)
    //     {
    //         var result = await response.Content.ReadFromJsonAsync<RegisterResponse>();
    //         if (result != null && !string.IsNullOrEmpty(result.Token))
    //         {
    //             await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "jwt", result.Token);
    //             return true;
    //         }
    //     }
    //     return false;
    // }


}



// public class RegisterResponse
// {
//     public string Token { get; set; }
// }