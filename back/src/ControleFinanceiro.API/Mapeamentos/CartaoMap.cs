using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControleFinanceiro.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ControleFinanceiro.API.Mapeamentos
{
    public class CartaoMap : IEntityTypeConfiguration<Cartao>
    {
        public void Configure(EntityTypeBuilder<Cartao> builder)
        {
            builder.HasKey(c => c.CartaoId);
            builder.Property(c => c.Nome).IsRequired().HasMaxLength(20);
            builder.HasIndex(c => c.Nome).IsUnique();

            builder.Property(c => c.Bandeira).IsRequired().HasMaxLength(15);

            builder.Property(c => c.Numero).IsRequired().HasMaxLength(20);
            builder.HasIndex(c => c.Numero).IsUnique();

            builder.Property(c => c.Limite).IsRequired();
                   
                   
            // Mapeamento de relacionamento entre as tabelas                                                                                                    // se caso ouver exclusão                        
            builder.HasOne(c => c.Usuario).WithMany( c=> c.Cartoes)
                .HasForeignKey(C => C.UsuarioId).IsRequired().OnDelete(DeleteBehavior.NoAction);
                
            builder.HasMany(c => c.Despesas).WithOne(C => C.Cartao);

            builder.ToTable("Cartoes");
        }
    }
}