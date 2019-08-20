using Transacao.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Transacao.Infra.Data.Mappings
{
    public class TransacaoMap : IEntityTypeConfiguration<TransacaoEntity>
    {
        public void Configure(EntityTypeBuilder<TransacaoEntity> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
               .ValueGeneratedOnAdd();

            builder.Property(x => x.Valor)
                .IsRequired()
                .HasColumnType("money");

            builder.Property(x => x.Usuario)
                .IsRequired()
                .HasColumnType("varchar(10)")
                .HasMaxLength(10);

            builder.ToTable("TbTransacao");

        }
    }
}
