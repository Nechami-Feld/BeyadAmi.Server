namespace BeyadAmi.Server.Application.Interfaces.Services;

public interface IAddressService
{
    Task<IEnumerable<object>> SearchAsync(string query);
}