using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using Eventing.ApiService.Controllers.User.Dto;

namespace Eventing.Web.Components.Functions.LoginFeature;

public partial class Login : ComponentBase
{
    [Inject] public HttpClient Http { get; set; }
    [Inject] public NavigationManager Navigation { get; set; }

    protected string Username { get; set; } = string.Empty;
    protected string Password { get; set; } = string.Empty;

    protected async Task HandleLogin()
    {
        var dto = new loginUserDto(Username, Password);
        
       var response = await Http.PostAsJsonAsync("api/Authentication/login", dto);

        if (response.IsSuccessStatusCode)
        {
            Navigation.NavigateTo("/counter");
        }
        else
        {
            Navigation.NavigateTo("/");
        }
    }
}