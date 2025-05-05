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
    public class OrderDetailsController : ControllerBase
    {
        private readonly OrderDetailBusiness _business;

        public OrderDetailsController(OrderDetailBusiness business)
        {
            _business = business;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDetails>>> GetOrderDetails()
        {
            var orderDetails = await _business.GetAllAsync();
            return Ok(orderDetails);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDetails>> GetOrderDetails(int id)
        {
            var orderDetail = await _business.GetByIdAsync(id);
            if (orderDetail == null)
            {
                return BadRequest("Detalle de Orden inexistente");
            }
            return Ok(orderDetail);
        }
    }
}
