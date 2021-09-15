using DevIO.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevIO.Data.Mappings
{
    public class FornecedorMapping : IEntityTypeConfiguration<Fornecedor>
    {
        public void Configure(EntityTypeBuilder<Fornecedor> builder)
        {
            builder.HasKey(keyExpression: p => p.Id);

            builder.Property(p => p.Nome)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(p => p.Documento)
                .IsRequired()
                .HasColumnType("varchar(14)");

            // 1 : 1 => Fornecedor : Endereço
            builder.HasOne(navigationExpression: e => e.Endereco)
                  .WithOne(navigationExpression: f => f.Fornecedor);

            // 1 : N => Fornecedor : Produtos
            builder.HasMany(navigationExpression: p => p.Produtos)
                  .WithOne(navigationExpression: f => f.Fornecedor)
                  .HasForeignKey(f => f.FornecedorId);

            builder.ToTable("Fornecedores");
        }
    }
}
