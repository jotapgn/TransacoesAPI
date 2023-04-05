using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TransacoesAPI.Models.Request;
using TransacoesAPI.Models.Requests;
using TransacoesAPI.Repository.Interface;
using TransacoesAPI.Services;
using TransacoesAPI.Services.Interfaces;

namespace TransacoesAPI.Controllers
{
    [Route("api/")]
    [ApiController]
    public class TransacaoController : ControllerBase
    {
        public readonly ITransacaoService _transacaoService;

        private readonly IConfiguration _configuration;
        public TransacaoController(ITransacaoService transacaoService, IConfiguration configuration)
        {
            _transacaoService = transacaoService;
            _configuration = configuration;
        }
        [HttpPost("transacao")]
        [Authorize(Roles ="User")]
        public ActionResult CreateTransacao(TransacaoRequest transacaoRequest)
        {
            try
            {
                string usuarioEmail = User.FindFirst(ClaimTypes.Email)?.Value;

                _transacaoService.CreateTransaction(transacaoRequest, usuarioEmail);

                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpPut("transacao")]
        [Authorize(Roles = "User")]
        public ActionResult UpdateTransacao(TransacaoUpdateRequest transacaoRequest)
        {
            try
            {
                string usuarioEmail = User.FindFirst(ClaimTypes.Email)?.Value;

                _transacaoService.UpdateTransaction(transacaoRequest, usuarioEmail);

                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpDelete("transacao")]
        [Authorize(Roles = "User")]
        public ActionResult UpdateTransacao(int transacao_id)
        {
            try
            {
                string usuarioEmail = User.FindFirst(ClaimTypes.Email)?.Value;

                _transacaoService.DeleteTransaction(transacao_id, usuarioEmail);

                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpGet("transacoes")]
        [Authorize(Roles = "User")]
        public ActionResult GetTransacoes(int tipo_da_movimentacao)
        {
            try
            {
                string usuarioEmail = User.FindFirst(ClaimTypes.Email)?.Value;

                var transacoes = _transacaoService.GetTransacoesList(usuarioEmail, tipo_da_movimentacao);

                return Ok(transacoes);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpGet("saldo")]
        [Authorize(Roles = "User")]
        public ActionResult GetSaldo(int tipo_da_movimentacao)
        {
            try
            {
                string usuarioEmail = User.FindFirst(ClaimTypes.Email)?.Value;

                var saldo = _transacaoService.GetSaldo(usuarioEmail, tipo_da_movimentacao);

                return Ok(saldo);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
