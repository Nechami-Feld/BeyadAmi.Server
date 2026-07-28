using BeyadAmi.Server.Application.Interfaces.Services;
using System.Net.Http.Json;

namespace BeyadAmi.Server.Application.Services;

public class AddressService : IAddressService
{
    private readonly HttpClient _httpClient;

    public AddressService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }


    public async Task<IEnumerable<object>> SearchAsync(string query)
    {
        var url =
     $"https://nominatim.openstreetmap.org/search" +
     $"?q={query}, Israel" +
     $"&format=json" +
     $"&addressdetails=1" +
     $"&accept-language=he";


        _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd(
            "BeyadAmi-Address-Service"
        );


        var result =
            await _httpClient.GetFromJsonAsync<IEnumerable<object>>(url);


        return result ?? [];
    }
}