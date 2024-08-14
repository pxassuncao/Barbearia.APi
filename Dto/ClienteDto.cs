using System.ComponentModel.DataAnnotations;
using Microsoft.VisualBasic;

namespace Barbearia.APi.Dto

{
        public class ClienteDto
    {
            public int ClienteId { get; set; }

            [Required]
            public string? Nome { get; set; }
        
            [Required]
            public string? Telefone { get; set; }
            [EmailAddress(ErrorMessage ="Favor digite um endereço de email válido")]

            public string? Email { get; set; }
            [Display(Name = "Data de Nascimento")]
            public DateTime DataNascimento { get; set; }
    }


}
