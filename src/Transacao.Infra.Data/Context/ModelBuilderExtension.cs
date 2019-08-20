using Microsoft.EntityFrameworkCore;
using Transacao.Domain.Entities;

namespace Transacao.Infra.Data.Context
{
    internal static class ModelBuilderExtension
    {
        public static void SeedTransacao(this ModelBuilder modelBuilder)
        {
            //Inserindo algumas contas que serão utilizadas como origem a príncipio
            modelBuilder.Entity<ContaEntity>().HasData(
                new ContaEntity() { Id = 1, Agencia = 1234, Numero = 12345, Digito = 3, Saldo = 10000M},
                new ContaEntity() { Id = 2, Agencia = 5678, Numero = 78901, Digito = 4, Saldo = 20000M},
                new ContaEntity() { Id = 3, Agencia = 9012, Numero = 23456, Digito = 5, Saldo = 30000M},
                new ContaEntity() { Id = 4, Agencia = 3456, Numero = 89012, Digito = 6, Saldo = 40000M}
            );
        }
    }
}
