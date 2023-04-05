namespace TransacoesAPI.Models.Request
{
    public class TransacaoUpdateRequest
    {
        public int id { get; set; }
        public int tipo_da_movimentacao { get; set; }
        public decimal valor { get; set; }
    }
}
