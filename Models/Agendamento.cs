using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Barbearia.APi.Models;

namespace Barbearia.APi.Models
{
    public class Agendamento
    {
        [Key]
        public int AgendamentoID { get; set; }
        [Required]
        public DateTime DataHora { get; set; }
        public string? Observacoes { get; set; }
        [Required]
        public int Status { get; set; }
        [Required]
        public int ClienteId { get; set; }
        [ForeignKey("ClienteId")]
        public Cliente Cliente { get; set; }

        public virtual ICollection<Servico>Servicos{get; set;}

    }
}