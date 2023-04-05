using TransacoesAPI.Constants;
using TransacoesAPI.Enum;
using TransacoesAPI.Models.Entity;
using TransacoesAPI.Models.Request;
using TransacoesAPI.Models.Requests;
using TransacoesAPI.Models.Result;
using TransacoesAPI.Repository;
using TransacoesAPI.Repository.Interface;
using TransacoesAPI.Services.Interfaces;

namespace TransacoesAPI.Services
{
    public class TransacaoService : ITransacaoService
    {
        private readonly ITransacaoRepository _transacaoRepository;
        private readonly IUsuarioRepository _usuarioRepository;

        public TransacaoService(ITransacaoRepository transacaoRepository, IUsuarioRepository usuarioRepository)
        {
            _transacaoRepository = transacaoRepository;
            _usuarioRepository = usuarioRepository;
        }
        public void CreateTransaction(TransacaoRequest transacaoRequest, string email)
        {
            try
            {
                var usuario = GetUsuario(email);

                if (ValidateMovimentacao(transacaoRequest.tipo_da_movimentacao))
                {
                    Transacao transacao = new Transacao
                    {
                        TipoDaMovimentacao = transacaoRequest.tipo_da_movimentacao,
                        Valor = Convert.ToDecimal(transacaoRequest.valor),
                        DataHoraCriacao = DateTime.Now,
                        UsuarioId = usuario.Id
                    };

                    _transacaoRepository.Insert(transacao);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void UpdateTransaction(TransacaoUpdateRequest transacaoRequest, string email)
        {
            try
            {
                var usuario = GetUsuario(email);

                if (ValidateMovimentacao(transacaoRequest.tipo_da_movimentacao))
                {
                    var transacao = _transacaoRepository.GetTransacaoById(transacaoRequest.id);

                    if (ValidateTransacao(transacao, usuario.Id))
                    {
                        transacao.Valor = Convert.ToDecimal(transacaoRequest.valor);
                        transacao.TipoDaMovimentacao = transacaoRequest.tipo_da_movimentacao;

                        _transacaoRepository.Update(transacao);
                    }

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void DeleteTransaction(int id, string email)
        {
            try
            {
                var usuario = GetUsuario(email);

                var transacao = _transacaoRepository.GetTransacaoById(id);
                if (ValidateTransacao(transacao, usuario.Id))
                {
                    _transacaoRepository.Delete(transacao);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public SaldoResult GetSaldo(string email, int tipoDeMovimentacao)
        {
            try
            {
                if (ValidateMovimentacao(tipoDeMovimentacao))
                {
                    var usuario = GetUsuario(email);

                    var saldo = new SaldoResult();
                   ;

                    if (tipoDeMovimentacao == TipoDaMovimentacao.Saida.GetHashCode())
                    {
                        saldo.tipo_da_movimentacao = TipoDaMovimentacao.Saida.GetHashCode();
                        saldo.valor = _transacaoRepository.GetSaldo(usuario.Id, TipoDaMovimentacao.Saida.GetHashCode());
                        return saldo;
                    }

                    if (tipoDeMovimentacao == TipoDaMovimentacao.Entrada.GetHashCode())
                    {
                        saldo.tipo_da_movimentacao = TipoDaMovimentacao.Entrada.GetHashCode();
                        saldo.valor = _transacaoRepository.GetSaldo(usuario.Id, TipoDaMovimentacao.Entrada.GetHashCode());
                        return saldo;
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        public List<Transacao> GetTransacoesList(string email, int tipoDeTransacao)
        {
            try
            {
                if (ValidateTipoDeTransacao(tipoDeTransacao))
                {
                    var usuario = GetUsuario(email);

                    if (tipoDeTransacao == TipoDeTransacao.Todas.GetHashCode())
                        return _transacaoRepository.GetAll(usuario.Id);

                    if (tipoDeTransacao == TipoDeTransacao.Gastos.GetHashCode())
                        return _transacaoRepository.GetByTipoDaMovimentacao(usuario.Id, TipoDaMovimentacao.Saida.GetHashCode());

                    if (tipoDeTransacao == TipoDeTransacao.Receitas.GetHashCode())
                        return _transacaoRepository.GetByTipoDaMovimentacao(usuario.Id, TipoDaMovimentacao.Entrada.GetHashCode());



                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        private Usuario GetUsuario(string email)
        {
            if (String.IsNullOrEmpty(email))
                throw new Exception("Email Inválido");

            var usuario = _usuarioRepository.GetUserByEmail(email);

            if (usuario == null)
                throw new Exception("Usuário não encontrado");

            return usuario;
        }
        public bool ValidateTransacao(Transacao transacao, int usuarioId)
        {
            if (transacao == null) throw new Exception("Transação não encontrada");

            if (transacao.UsuarioId != usuarioId) throw new Exception("Alteração não permitida para o usuário solicitante");

            return true;

        }
        public bool ValidateMovimentacao(int tipoDaMovimentacao)
        {
            if (tipoDaMovimentacao != TipoDaMovimentacao.Saida.GetHashCode() &&
                tipoDaMovimentacao != TipoDaMovimentacao.Entrada.GetHashCode())
                throw new Exception("Tipo da movimentação inválida");

            return true;
        }
        public bool ValidateTipoDeTransacao(int tipoDeTransacao)
        {
            if (tipoDeTransacao != TipoDeTransacao.Todas.GetHashCode() &&
                tipoDeTransacao != TipoDeTransacao.Gastos.GetHashCode() &&
                tipoDeTransacao != TipoDeTransacao.Receitas.GetHashCode())
                throw new Exception("Tipo da movimentação inválida");

            return true;
        }

    }
}
