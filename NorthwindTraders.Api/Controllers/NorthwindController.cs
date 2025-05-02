using Microsoft.AspNetCore.Mvc;
using NorthwindTraders.Application.DTOs;
using NorthwindTraders.Application.Interfaces;
using NorthwindTraders.Application.Services;
using NorthwindTraders.Domain.Entities;

namespace NorthwindTraders.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NorthwindController(INorthwindService service) : Controller
    {
        //GET ORDERS
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

        [HttpGet]
        [Route("[action]/{orderID}")]

        public async Task<IActionResult> GetOrderDetailById(int orderID)
        {
            var orderDetails = await service.GetOrderDetailByOrder(orderID);
            return Ok(orderDetails);
        }

        [HttpGet]
        [Route("[action]/{id}")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            var employee = await service.GetEmployeeById(id);
            return Ok(employee);
        }

        [HttpGet]
        [Route("[action]/{id}")]
        public async Task<IActionResult> GetCustomerById(string id)
        {
            var customer = await service.GetCustomerById(id);
            return Ok(customer);
        }

        //POST
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> AddOrder([FromBody] OrderDto orderDto)
        {
            if (orderDto == null)
            {
                return BadRequest("Order data is required.");
            }

            try
            {
                var result = await service.AddOrder(orderDto).ConfigureAwait(false);
                return Ok(new { Message = "Order added successfully", OrderID = result });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> AddOrderDetail([FromBody] LineDto orderDetailDto)
        {
            if (orderDetailDto == null)
            {
                return BadRequest("Order detail data is required.");
            }
            try
            {
                var result = await service.AddOrderDetail(orderDetailDto).ConfigureAwait(false);
                return Ok(new { Message = "Order detail added successfully", OrderID = result });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(new { Message = ex.Message });
            }
        }

        //UPDATE
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> UpdateOrder([FromBody] OrderDto orderDto)
        {
            if(orderDto == null)
            {
                return BadRequest("Order is required");
            }

            try
            {
                var result = await service.UpdateOrder(orderDto).ConfigureAwait(false);
                return Ok(new {Message = "Order detail added successfully", OrderID = result });

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(new { Message = ex.Message });
            }

        }

        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> UpdateOrderDetail([FromBody] LineDto orderDetailDto)
        {
            if (orderDetailDto == null)
            {
                return BadRequest("Order detail is required");
            }
            try
            {
                var result = await service.UpdateOrderDetail(orderDetailDto, service).ConfigureAwait(false);
                return Ok(new { Message = "Order detail updated successfully", OrderID = result });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(new { Message = ex.Message });
            }
        }

        //DELETE

        [HttpDelete]
        [Route("[action]/{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            if(id == 0)
            {
                return BadRequest("OrderID is required");
            }

            await service.DeleteOrder(id).ConfigureAwait(false); 
            return Ok();
        }

        [HttpDelete]
        [Route("[action]/{orderId}/{productId}")]
        public async Task<IActionResult> DeleteOrderDetail(int orderId, int productId)
        {
            if(orderId ==null || productId ==null)
            {
                return BadRequest("Missing fields required");
            }

            await service.DeleteOrderDetail(orderId, productId).ConfigureAwait(false);
            return Ok();
        }
    }
}
