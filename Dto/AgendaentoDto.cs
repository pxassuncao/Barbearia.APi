using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Barbearia.APi.Models;

namespace Barbearia.APi;

public class AgendaentoDto
{
        public int AgendamentoID { get; set; }
        [Required]
        public DateTime DataHora { get; set; }
        public string? Observacoes { get; set; }
        [Required]
        public int Status { get; set; }
        public Cliente Cliente { get; set; }
        public virtual ICollection<Servico>Servicos{get; set;}

}
