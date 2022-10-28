using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyMicroservice.DataAccess.Requests;
using MyMicroservice.DataContext;
using MyMicroservice.DTOModels;
using MyMicroservice.Models;
using MyMicroservice.Services;

namespace MyMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _service;

        public OrdersController(IOrderService service)
        {
            _service = service;
        }

        // GET: api/Orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> GetOrders([FromQuery(Name = "page")] int page = 1, [FromQuery(Name = "Items")] int items = 20)
        {
            var result = await _service.GetOrders(page, items);
            if (result is null)
            {
                return BadRequest();
            }
            return Ok(result);
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDTO>> GetOrder(int id)
        {
            var order = await _service.GetDetailedOrder(id);

            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        [HttpPost]
        public async Task<IActionResult> PostOrder(OrderRequest request)
        {
            OrderDTO order = request.Order;
            order.OrderItems = request.OrderItems;
            order.OrderStatus = 4;
            await _service.CreateOrder(order);

            return Created("New Order", order);
        }

        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public IActionResult DeleteOrder(int id)
        {
            var order = _service.GetOrder(id);
            if (order == null)
            {
                return NotFound();
            }

            //_service.DeleteOrder(order);

            return NoContent();
        }

    }
}
