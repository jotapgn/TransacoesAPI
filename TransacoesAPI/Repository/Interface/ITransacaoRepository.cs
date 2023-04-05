using TransacoesAPI.Models.Entity;
using TransacoesAPI.Models.Request;
using TransacoesAPI.Models.Requests;
using TransacoesAPI.Models.Result;
using TransacoesAPI.Repository.Base;

namespace TransacoesAPI.Repository.Interface
{
    public interface ITransacaoRepository : IRepository<Transacao>
    {
        public List<Transacao> GetAll(int id);
        public Transacao GetTransacaoById(int id);
        public List<Transacao> GetByTipoDaMovimentacao(int id, int tipoDaMovimentacao);
        public decimal GetSaldo(int usuarioId, int tipoDaMovimentacao);
    }
}
