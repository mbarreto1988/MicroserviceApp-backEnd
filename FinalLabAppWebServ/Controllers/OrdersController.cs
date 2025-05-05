using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FinalLabAppWebServ.Context;
using FinalLabAppWebServ.Entities;
using FinalLabAppWebServ.Business;

namespace FinalLabAppWebServ.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly OrderBusiness _business;

        public OrdersController(OrderBusiness business)
        {
            _business = business;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            return await _business.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            var order = await _business.GetByIdAsync(id);
            if (order == null)
                return BadRequest("Orden inexistente");
            return order;
        }

        [HttpPost]
        public async Task<ActionResult<object>> PostOrder(Order order)
        {
            var result = await _business.CreateAsync(order);
            if (result is string errorMsg)
                return BadRequest(errorMsg);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var deleted = await _business.DeleteAsync(id);
            if (!deleted)
                return NotFound();
            return NoContent();
        }
    }
}
