using System;

namespace Transacao.API.Commands
{
    public class ContaResponse
    {
        public int Id { get; set; }
        public string Agencia { get; set; }
        public string Numero { get; set; }
        public string Digito { get; set; }
        public string Saldo { get; set; }
    }
}
