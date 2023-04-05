using Swashbuckle.AspNetCore.Filters;
using System.Reflection.Metadata.Ecma335;
using TransacoesAPI.Constants;
using TransacoesAPI.Data;
using TransacoesAPI.Models.Entity;
using TransacoesAPI.Models.Result;
using TransacoesAPI.Repository.Interface;

namespace TransacoesAPI.Repository
{
    public class TransacaoRepository : Repository<Transacao>, ITransacaoRepository
    {
        private readonly AppDbContext _context;
        public TransacaoRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }      
        public Transacao GetTransacaoById(int id)
        {
            return _context.Transacoes.Where(x => x.Id == id).FirstOrDefault();
        }

        public List<Transacao> GetAll(int usuarioId)
        {
            return _context.Transacoes.Where(x => x.UsuarioId == usuarioId).ToList();
        }
        public List<Transacao> GetByTipoDaMovimentacao(int usuarioId, int tipoDaMovimentacao)
        {
            return _context.Transacoes
                            .Where(x => x.UsuarioId == usuarioId && x.TipoDaMovimentacao == tipoDaMovimentacao)
                            .ToList();
        }     
        public decimal GetSaldo(int usuarioId, int tipoDaMovimentacao)
        {
            return _context.Transacoes
                             .Where(x => x.UsuarioId == usuarioId && x.TipoDaMovimentacao == tipoDaMovimentacao)
                             .ToList()
                             .Sum(y => y.Valor);
        }
    }
}
