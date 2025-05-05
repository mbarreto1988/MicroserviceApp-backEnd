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
    public class RegistersController : ControllerBase
    {
        private readonly RegisterBusiness _registerBusiness;

        public RegistersController(RegisterBusiness registerBusiness)
        {
            _registerBusiness = registerBusiness;
        }

        // GET: api/Registers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Register>>> GetRegisters()
        {
            var registers = await _registerBusiness.GetRegistersAsync();
            return Ok(registers);
        }

        // GET: api/Registers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Register>> GetRegister(int id)
        {
            try
            {
                var register = await _registerBusiness.GetRegisterByIdAsync(id);
                if (register == null)
                {
                    return NotFound();
                }
                return Ok(register);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/Registers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRegister(int id, Register register)
        {
            try
            {
                var updatedRegister = await _registerBusiness.UpdateRegisterAsync(id, register);
                return Ok(updatedRegister);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: api/Registers
        [HttpPost]
        public async Task<ActionResult<Register>> PostRegister(Register register)
        {
            try
            {
                var newRegister = await _registerBusiness.CreateRegisterAsync(register);
                return CreatedAtAction("GetRegister", new { id = newRegister.RegisterId }, newRegister);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/Registers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRegister(int id)
        {
            try
            {
                await _registerBusiness.DeleteRegisterAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
