using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TransacoesAPI.Models.Entity;
using TransacoesAPI.Models.Request;
using TransacoesAPI.Models.Requests;
using TransacoesAPI.Repository;
using TransacoesAPI.Repository.Interface;
using TransacoesAPI.Services.Interfaces;

namespace TransacoesAPI.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public bool ValidateLogin(LoginRequest request)
        {
            if (request.email == null)
                return false;

            var usuario = _usuarioRepository.GetUserByEmail(request.email);

            if (usuario == null)
                return false;

            if (!BCrypt.Net.BCrypt.Verify(request.password, usuario.Password))
                return false;

            return true;

        }
        public bool ValidateUser(UsuarioRequest request)
        {
            if (request.email == null)
                throw new Exception("Necessário informar o email.");

            var usuario = _usuarioRepository.GetUserByEmail(request.email);

            if (usuario != null)
                throw new Exception("Usuário já cadastrado.");

            if (!TransacoesAPI.Services.EmailService.ValidateEmail(request.email))
                throw new Exception("Email Inválido.");

            return true;

        }
        public Usuario GetUsuarioByEmail(string email)
        {
            return _usuarioRepository.GetUserByEmail(email);
        }
        public void RegisterUser(UsuarioRequest usuarioRequest)
        {
            try
            {
                if(ValidateUser(usuarioRequest))
                {
                    string passwordHash = BCrypt.Net.BCrypt.HashPassword(usuarioRequest.password);

                    Usuario usuario = new Usuario
                    {
                        Nome = usuarioRequest.nome,
                        Email = usuarioRequest.email.Trim(),
                        Password = passwordHash
                    };

                    _usuarioRepository.Insert(usuario);

                }      
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }


    }
}
