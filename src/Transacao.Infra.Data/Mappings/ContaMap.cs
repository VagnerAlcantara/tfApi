using Transacao.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Transacao.Infra.Data.Mappings
{
    public class ContaMap : IEntityTypeConfiguration<ContaEntity>
    {
        public void Configure(EntityTypeBuilder<ContaEntity> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Agencia)
                .HasColumnType("Int")
                .IsRequired();

            builder.Property(x => x.Numero)
                .HasColumnType("Int")
                .IsRequired();

            builder.Property(x => x.Saldo)
                .HasColumnType("money")
                .IsRequired();

            builder.Property(x => x.Agencia)
                .HasColumnType("Int");

            builder.ToTable("TbConta");

            builder.HasMany(x => x.Transacoes)
               .WithOne(x => x.ContaOrigem)
               .HasForeignKey(x => x.ContaOrigemId);
        }
    }
}
