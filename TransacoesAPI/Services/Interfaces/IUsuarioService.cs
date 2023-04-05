using TransacoesAPI.Models.Entity;
using TransacoesAPI.Models.Request;
using TransacoesAPI.Models.Requests;

namespace TransacoesAPI.Services.Interfaces
{
    public interface IUsuarioService
    {
        public bool ValidateLogin(LoginRequest request);
        public void RegisterUser(UsuarioRequest usuarioRequest);
        public Usuario GetUsuarioByEmail(string email);
    }
}
