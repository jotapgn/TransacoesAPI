using TransacoesAPI.Data;
using TransacoesAPI.Models.Entity;
using TransacoesAPI.Models.Requests;
using TransacoesAPI.Repository.Interface;

namespace TransacoesAPI.Repository
{
    public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
    {
        private readonly AppDbContext _context;
        public UsuarioRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
       
        public Usuario GetUserByEmail(string email)
        {
            var usuario = _context.Usuarios.FirstOrDefault(x => x.Email.Equals(email));
            return usuario;
        }
    }
}
