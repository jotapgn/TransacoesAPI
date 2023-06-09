﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Annotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TransacoesAPI.Models.Entity;
using TransacoesAPI.Models.Request;
using TransacoesAPI.Models.Requests;
using TransacoesAPI.Repository.Interface;
using TransacoesAPI.Services;
using TransacoesAPI.Services.Interfaces;

namespace TransacoesAPI.Controllers
{
    [Route("api/")]
    [ApiController]

    public class AuthController : ControllerBase
    {
        public readonly IUsuarioService _usuarioService;

        private readonly IConfiguration _configuration;
        public AuthController(IUsuarioService usuarioService, IConfiguration configuration)
        {
            _usuarioService = usuarioService;
            _configuration = configuration;
        }

        /// <summary>
        /// Registrar Usuário
        /// </summary>
        /// <remarks>
        /// Registrar o usuário na API.
        /// </remarks>
        [HttpPost("register")]
        public ActionResult Register(UsuarioRequest usuarioRequest)
        {
            try
            {

                _usuarioService.RegisterUser(usuarioRequest);
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        /// <summary>
        /// Login do Usuário
        /// </summary>
        /// <remarks>
        /// Login do usuário na API para retorno do token
        /// </remarks>      
        [HttpPost("login")]
        public async Task<ActionResult<dynamic>>Login(LoginRequest usuarioRequest)
        {
            try
            {
                if (_usuarioService.ValidateLogin(usuarioRequest))
                {
                    var usuario = _usuarioService.GetUsuarioByEmail(usuarioRequest.email);

                    var token = TokenService.CreateToken(usuario, _configuration);
                    return Ok(token);
                }

                return NotFound("Erro no login, verifique se o email e senha estão digitados corretamente");


            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        

    }
}
