using TransacoesAPI.Models.Entity;
using TransacoesAPI.Models.Request;
using TransacoesAPI.Models.Result;

namespace TransacoesAPI.Services.Interfaces
{
    public interface ITransacaoService
    {
        public void CreateTransaction(TransacaoRequest transacaoRequest, string email);
        public void UpdateTransaction(TransacaoUpdateRequest transacaoRequest, string email);
        public void DeleteTransaction(int id, string email);
        public List<Transacao> GetTransacoesList(string email, int tipoDeTransacao);
        public bool ValidateMovimentacao(int tipoDaMovimentacao);
        public bool ValidateTipoDeTransacao(int tipoDeTransacao);
        public bool ValidateTransacao(Transacao transacao, int usuarioId);
        public SaldoResult GetSaldo(string email, int tipoDeTransacao);
    }
}
