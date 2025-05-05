using FinalLabAppWebServ.Context;
using FinalLabAppWebServ.Entities;
using Microsoft.EntityFrameworkCore;

namespace FinalLabAppWebServ.Business
{
    public class AuthBusiness
    {
        private readonly AppDbContext _context;

        public AuthBusiness(AppDbContext context)
        {
            _context = context;
        }

        // Método para el login
        public async Task<string> LoginAsync(string email, string password)
        {
            var user = await _context.Register.FirstOrDefaultAsync(r => r.RegisterEmail == email);
            if (user == null)
            {
                throw new Exception("Necesitas registrarte");
            }

            // Verificar la contraseña hasheada
            if (!BCrypt.Net.BCrypt.Verify(password, user.RegisterPasswordHash))
            {
                throw new Exception("La contraseña es incorrecta");
            }

            return "Bienvenido";
        }
    }
}
