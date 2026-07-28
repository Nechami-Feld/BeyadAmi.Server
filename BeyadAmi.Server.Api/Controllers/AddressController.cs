using BeyadAmi.Server.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;


namespace BeyadAmi.Server.Api.Controllers;


[ApiController]
[Route("api/[controller]")]
public class AddressController : ControllerBase
{

    private readonly IAddressService _addressService;


    public AddressController(
        IAddressService addressService)
    {
        _addressService = addressService;
    }



    [HttpGet("search")]
    public async Task<IActionResult> Search(
        string query)
{
        if (string.IsNullOrWhiteSpace(query))
            return BadRequest();


        var result =
            await _addressService.SearchAsync(query);


        return Ok(result);
    }
}