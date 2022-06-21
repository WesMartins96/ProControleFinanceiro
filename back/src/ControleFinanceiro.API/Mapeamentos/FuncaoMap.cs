using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControleFinanceiro.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ControleFinanceiro.API.Mapeamentos
{
    public class FuncaoMap : IEntityTypeConfiguration<Funcao>
    {
        public void Configure(EntityTypeBuilder<Funcao> builder)
        {
                                    // Gera automaticamente o valor
            builder.Property(f => f.Id).ValueGeneratedOnAdd();

            builder.Property(f => f.Descricao).IsRequired().HasMaxLength(50);

            // Relacionamento já vem configurado, o que eu quero aqui é
            // que alguns dados da tabela sejam criados com o BD
            builder.HasData(
                new Funcao{
                    Id = Guid.NewGuid().ToString(),
                    Name = "Administrador",
                    NormalizedName = "ADMINISTRADOR",
                    Descricao = "Administrador do sistema"
                },  // NormalizeName, é oq a função usa para comparar valores
                new Funcao{
                    Id = Guid.NewGuid().ToString(),
                    Name = "Usuario",
                    NormalizedName = "USUARIO",
                    Descricao = "Usuario do sistema"
                });

            builder.ToTable("Funcoes");    
        }
    }
}