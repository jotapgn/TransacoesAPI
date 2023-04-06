using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
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

        public TransacaoController(ITransacaoService transacaoService)
        {
            _transacaoService = transacaoService;
        }
        /// <summary>
        ///Cadastrar Transação
        /// </summary>
        /// <remarks>
        /// Tipos de movimentações: 1:Entrada, 2:Saída.
        /// 
        /// </remarks>
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
        /// <summary>
        ///Alterar Transação
        /// </summary>
        /// <remarks>
        /// Tipos de movimentações: 1:Entrada, 2:Saída.
        /// 
        /// </remarks>
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
        /// <summary>
        ///Excluir Transação
        /// </summary>
        [HttpDelete("transacao")]
        [Authorize(Roles = "User")]
        public ActionResult DeleteTransacao(int transacao_id)
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
        /// <summary>
        ///Retornar Transações
        /// </summary>
        /// <param name="tipo_de_transacao">Tipos de transações: 0:Todas, 1:Receitas, 2:Gastos.</param>
        [HttpGet("transacoes")]
        [Authorize(Roles = "User")]
        public ActionResult GetTransacoes(int tipo_de_transacao)
        {
            try
            {
                string usuarioEmail = User.FindFirst(ClaimTypes.Email)?.Value;

                var transacoes = _transacaoService.GetTransacoesList(usuarioEmail, tipo_de_transacao);

                return Ok(transacoes);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        /// <summary>
        ///Retornar saldo por tipo da movimentação
        /// </summary>
        /// <param name="tipo_da_movimentacao">Tipos de movimentações: 1:Entrada, 2:Saída.</param>
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
