using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace TransacoesAPI.Models.Entity
{
    public class Transacao
    {  
        public int Id { get; set; }
        public int TipoDaMovimentacao { get; set; }
        public DateTime DataHoraCriacao { get; set; }
        public decimal Valor { get; set; }
        public int UsuarioId { get; set; }
        internal Usuario Usuario { get; set; }
    }
}
