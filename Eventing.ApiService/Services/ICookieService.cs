namespace Eventing.ApiService.Services;

public interface ICookieService
{
    void Set(string key, string value, int? expireMinutes = null);
    string Get(string key);
    void Delete(string key);
}
