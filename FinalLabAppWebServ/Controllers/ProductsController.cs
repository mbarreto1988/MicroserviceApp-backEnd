using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FinalLabAppWebServ.DAL.Context;
using FinalLabAppWebServ.Business;
using FinalLabAppWebServ.DAL.Entities;

namespace FinalLabAppWebServ.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ProductBusiness _business;

        public ProductsController(ProductBusiness business)
        {
            _business = business;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Products>>> GetProducts()
        {
            return await _business.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Products>> GetProduct(int id)
        {
            var product = await _business.GetByIdAsync(id);
            if (product == null)
                return BadRequest("Producto inexistente");
            return product;
        }

        [HttpPost]
        public async Task<ActionResult<Products>> PostProduct(Products product)
        {
            var created = await _business.CreateAsync(product);
            return CreatedAtAction(nameof(GetProduct), new { id = created.ProductId }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Products product)
        {
            var result = await _business.UpdateAsync(id, product);
            if (!result)
                return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var deleted = await _business.DeleteAsync(id);
            if (!deleted)
                return NotFound();
            return NoContent();
        }
    }
}
