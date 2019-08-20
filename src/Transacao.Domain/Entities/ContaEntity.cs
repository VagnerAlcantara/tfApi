using System.Collections.Generic;

namespace Transacao.Domain.Entities
{
    public class ContaEntity : Entity
    {
        public int Agencia { get; set; }
        public int Numero { get; set; }
        public int Digito { get; set; }
        public decimal Saldo { get; set; }

        public virtual IEnumerable<TransacaoEntity> Transacoes { get; set; }

        public void SubtrairSaldo(decimal valor)
        {
            Saldo -= valor;
        }

        public void AdicionarSaldo(decimal valor)
        {
            Saldo += valor;
        }
        public override string ToString()
        {
            return $"{Agencia} {Numero} - {Digito}";
        }
    }
}
