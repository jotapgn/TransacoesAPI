using TransacoesAPI.Models.Entity;
using TransacoesAPI.Models.Requests;
using TransacoesAPI.Repository.Base;

namespace TransacoesAPI.Repository.Interface
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        public Usuario GetUserByEmail(string email);
    }
}
