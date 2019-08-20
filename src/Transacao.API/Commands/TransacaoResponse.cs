using System;
using Transacao.API.Models;

namespace Transacao.API.Commands
{
    public class TransacaoResponse
    {
        public DateTime DataTransacao { get; set; }
        public string Usuario { get; set; }
        public virtual ContaModel ContaOrigem { get; set; }
        public int ContaDestinoId { get; set; }
        public ContaModel ContaDestino { get; set; }
        public string Valor { get; set; }
    }
}
