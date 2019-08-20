using System.ComponentModel.DataAnnotations;

namespace Transacao.API.Models
{
    public class ContaModel
    {
        [Required(ErrorMessage = "O parâmetro agência é obrigatório")]
        [StringLength(4, ErrorMessage = "O parâmetro agência precisa ter {1} caracter", MinimumLength = 4)]
        public string Agencia { get; set; }

        [StringLength(5, ErrorMessage = "O parâmetro número precisa ter {1} caracter", MinimumLength = 5)]
        [Required(ErrorMessage = "O parâmetro número é obrigatório")]
        public string Numero { get; set; }

        [StringLength(1, ErrorMessage = "O parâmetro dígito precisa ter {1} caracter", MinimumLength = 1)]
        [Required(ErrorMessage = "O parâmetro dígito é obrigatório")]
        public string Digito { get; set; }
    }
}
