using Microsoft.EntityFrameworkCore;
using productivity_hub_api.Models;

namespace productivity_hub_api.Repository.AuthRepository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private StoreContext _context;

        public UsuarioRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task<Usuario?> Validate(string email, string password)
        {
            var usuario = await _context.Usuarios.Include(u => u.Persona).SingleOrDefaultAsync(u => u.Email == email);

            if (usuario != null)
            {
                return usuario.CheckPassword(password) ? usuario : null;
            }

            return usuario;
        }

        public async Task<Usuario?> GetByIdAsync(int id) => await _context.Usuarios.Include(u => u.Persona)
            .FirstOrDefaultAsync(u => u.Id == id);

        public async Task AddAsync(Usuario usuario) => await _context.Usuarios.AddAsync(usuario);

        public void Update(Usuario usuario)
        {
            _context.Attach(usuario);
            _context.Usuarios.Entry(usuario).State = EntityState.Modified;
        }

        public async Task<bool> EmailExistsAsync(string email, CancellationToken cancellationToken)
        {
            return await _context.Usuarios.AnyAsync(u => u.Email == email, cancellationToken);
        }
    }
}
