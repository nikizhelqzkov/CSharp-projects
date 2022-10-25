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
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            var order = await _service.GetDetailedOrder(id);

            if (order == null)
            {
                return NotFound();
            }

            return order;
        }

        //// PUT: api/Orders/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutOrder(int id, Order order)
        //{
        //    if (id != order.OrderId)
        //    {
        //        return BadRequest();
        //    }

        //    _service.Entry(order).State = EntityState.Modified;

        //    try
        //    {
        //        await _service.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!OrderExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // POST: api/Orders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostOrder(OrderRequest request)
        {
            OrderDTO order = request.Order;
            order.OrderItems = request.OrderItems;
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

            _service.DeleteOrder(order);
            //await _service.SaveChangesAsync();

            return NoContent();
        }

        //private bool OrderExists(int id)
        //{
        //    return _service.Orders.Any(e => e.OrderId == id);
        //}
    }
}
