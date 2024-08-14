using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Barbearia.APi.Models
{
    public class Servico
    {
        [Key]
        public int Id { get; set; }
        [Required (ErrorMessage ="O nome do serviço é obrigatório")]
        [DisplayName ("Nome Serviço")]
        public string? NomeServico { get; set; }
        
        [DisplayName ("Descriçao")]
        public string? Descricao { get; set; }

        [Range(1, 999, ErrorMessage ="A duração deve ficar acima de 10 minutos até 6 horas")]
        public int DuracaoMin { get; set; }

        [Range(0.01, 999.00, ErrorMessage ="O preço deve ser superior a um centavo e menor que 999 mil reais")]
        public decimal Preco { get; set; }
    }
}