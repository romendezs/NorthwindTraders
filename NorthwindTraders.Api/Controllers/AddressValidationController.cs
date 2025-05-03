using Microsoft.AspNetCore.Mvc;
using NorthwindTraders.Application.DTOs;
using NorthwindTraders.Domain.Interfaces.ExternalServices;
using NorthwindTraders.Infrastructure.ExternalServices;

namespace NorthwindTraders.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AddressValidationController(IAddressValidationApi service) : Controller
    {
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> ValidateAddress([FromBody] AddressValidationRequestDto request)
        {
            if (request == null)
            {
                return BadRequest("Request object cannot be null.");
            }

            // Call the external service
            var response = await service.validateAddress(
                request.address.regionCode,
                request.address.locality,
                request.address.administrativeArea,
                request.address.postalCode,
                request.address.addressLines
            ); ;
            return Ok(response);
        }
    }
}
