using TransacoesAPI.Models.Entity;

namespace TransacoesAPI.Models.Request
{
    public class TransacaoRequest
    {
        public int tipo_da_movimentacao { get; set; }
        public decimal valor { get; set; }
    }
}
