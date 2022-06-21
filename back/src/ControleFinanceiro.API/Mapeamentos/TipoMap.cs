using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControleFinanceiro.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ControleFinanceiro.API.Mapeamentos
{
    public class TipoMap : IEntityTypeConfiguration<Tipo>
    {
        public void Configure(EntityTypeBuilder<Tipo> builder)
        {
            builder.HasKey(t => t.TipoId);
            builder.Property(t => t.Nome).IsRequired().HasMaxLength(20);

            // Mapeamento de relacionamento entre as tabelas
            builder.HasMany(t => t.Categorias).WithOne(t => t.Tipo);


            // HasData, Já preenche com valores
            builder.HasData(
                new Tipo{
                    TipoId = 1,
                    Nome = "Despesa"
                },
                new Tipo{
                    TipoId = 2,
                    Nome = "Ganho"
                });

            builder.ToTable("Tipos");    
        }
    }
}