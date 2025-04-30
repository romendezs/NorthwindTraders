using Microsoft.AspNetCore.Mvc;
using NorthwindTraders.Application.Interfaces;

namespace NorthwindTraders.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NorthwindController(INorthwindService service) : Controller
    {
        [HttpGet]
        [Route("[action]ByDate/{date:DateTime}")]
        public async Task<IActionResult> GetOrdersByDate(DateTime date)
        {
            var orders = await service.GetOrderByDate(date);
            return Ok(orders);
        }

        [HttpGet]
        [Route("[action]ByCustomer/{customerID}")]
        public async Task<IActionResult> GetOrdersByCustomer(string customerID)
        {
            var orders = await service.GetOrderByCustomer(customerID);
            return Ok(orders);
        }

        [HttpGet]
        [Route("[action]ByEmployee/{employeeID}")]
        public async Task<IActionResult> GetOrdersByEmployee(int employeeID)
        {
            var orders = await service.GetOrderByEmployee(employeeID);
            return Ok(orders);
        }

        [HttpGet]
        [Route("[action]/{orderID}")]
        public async Task<IActionResult> GetOrdersByOrder(int orderID)
        {
            var orders = await service.GetOrderByOrder(orderID);
            return Ok(orders);
        }
    }
}
