using Transacao.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Transacao.Infra.Data.Context
{
    public class TransacaoContext : DbContext
    {
        public TransacaoContext(DbContextOptions options) : base(options){}
        public DbSet<ContaEntity> ContaOrigem { get; set; }
        public DbSet<TransacaoEntity> Transacao { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes()
                .SelectMany(x => x.GetProperties()
                .Where(p => p.ClrType == typeof(string))))
            {
                property.Relational().ColumnType = "varchar(100)";
            }

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TransacaoContext).Assembly);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(x => x.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;
            }

            //Seed customizado para o projeto
            modelBuilder.SeedTransacao();

            base.OnModelCreating(modelBuilder);
        }
    }
}
