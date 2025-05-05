using FinalLabAppWebServ.DAL.Context;
using FinalLabAppWebServ.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace FinalLabAppWebServ.Business
{
    public class RegisterBusiness
    {
        private readonly AppDbContext _context;

        public RegisterBusiness(AppDbContext context)
        {
            _context = context;
        }

        // Obtener todos los registros
        public async Task<List<Register>> GetRegistersAsync()
        {
            return await _context.Register.ToListAsync();
        }

        // Obtener un registro por ID
        public async Task<Register> GetRegisterByIdAsync(int id)
        {
            return await _context.Register.FindAsync(id);
        }

        // Crear un nuevo registro
        public async Task<Register> CreateRegisterAsync(Register register)
        {
            // Verificar si ya existe un usuario con el mismo email
            var existingUser = await _context.Register
                .FirstOrDefaultAsync(r => r.RegisterEmail == register.RegisterEmail);
            if (existingUser != null)
            {
                throw new Exception("El email ya está registrado");
            }

            // Hash de la contraseña
            register.RegisterPasswordHash = BCrypt.Net.BCrypt.HashPassword(register.RegisterPasswordHash);

            _context.Register.Add(register);
            await _context.SaveChangesAsync();

            return register;
        }

        // Actualizar un registro
        public async Task<Register> UpdateRegisterAsync(int id, Register register)
        {
            var existingUser = await _context.Register.FindAsync(id);
            if (existingUser == null)
            {
                throw new Exception("Usuario no encontrado");
            }

            // Actualizar campos (excepto la contraseña, que se re-hashea si se proporciona)
            existingUser.RegisterName = register.RegisterName;
            existingUser.RegisterEmail = register.RegisterEmail;

            if (!string.IsNullOrWhiteSpace(register.RegisterPasswordHash))
            {
                existingUser.RegisterPasswordHash = BCrypt.Net.BCrypt.HashPassword(register.RegisterPasswordHash);
            }

            await _context.SaveChangesAsync();
            return existingUser;
        }

        // Eliminar un registro
        public async Task DeleteRegisterAsync(int id)
        {
            var register = await _context.Register.FindAsync(id);
            if (register == null)
            {
                throw new Exception("Usuario no encontrado");
            }

            _context.Register.Remove(register);
            await _context.SaveChangesAsync();
        }
    }
}
