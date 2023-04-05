using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace TransacoesAPI.Models.Entity
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

    }
}
