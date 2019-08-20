using System.ComponentModel.DataAnnotations;
using Transacao.API.Extension;
using Transacao.API.Models;

namespace Transacao.API.Commands
{
    public class TransacaoInclusaoRequest
    {
        public ContaModel ContaOrigem { get; set; }

        public ContaModel ContaDestino { get; set; }

        [Required(ErrorMessage = "O parâmetro valor é obrigatório")]
        [Moeda(ErrorMessage ="Valor no formato errado")]
        public string Valor { get; set; }

        [Required(ErrorMessage = "O parâmetro usuário é obrigatório")]
        [StringLength(10, ErrorMessage = "O campo {0} precisa ter {1}", MinimumLength = 3)]
        public string Usuario { get; set; }
    }
}
