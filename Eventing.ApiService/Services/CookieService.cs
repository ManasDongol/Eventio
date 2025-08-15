using System.Net;

namespace Eventing.ApiService.Services;

public class CookieService : ICookieService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CookieService(IHttpContextAccessor accessor)
    {
        _httpContextAccessor = accessor;
    }

    public void Set(string key, string value, int? expireMinutes = null)
    {
        
        var options = new CookieOptions
        {
            Expires = DateTime.UtcNow.AddMinutes(expireMinutes ?? 60),
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict
        };

        _httpContextAccessor.HttpContext?.Response.Cookies.Append(key, value, options);
    }

    public string Get(string key)
    {
        return _httpContextAccessor.HttpContext?.Request.Cookies[key];
    }

    public void Delete(string key)
    {
        _httpContextAccessor.HttpContext?.Response.Cookies.Delete(key);
    }
}
