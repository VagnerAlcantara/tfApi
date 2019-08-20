using System;

namespace Transacao.Domain.Entities
{
    public class TransacaoEntity : Entity
    {
        public TransacaoEntity()
        {
            DataTransacao = DateTime.Now;
        }
        public DateTime DataTransacao { get; set; }
        public string Usuario { get; set; }
        public int ContaOrigemId { get; set; }
        public virtual ContaEntity ContaOrigem { get; set; }
        public int ContaDestinoId { get; set; }
        public virtual ContaEntity ContaDestino { get; set; }
        public decimal Valor { get; set; }
    }
}
