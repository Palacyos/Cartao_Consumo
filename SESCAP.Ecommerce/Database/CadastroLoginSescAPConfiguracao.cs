using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SESCAP.Ecommerce.Models;

namespace SESCAP.Ecommerce.Database
{
    public class CadastroLoginSescAPConfiguracao: IEntityTypeConfiguration<CadastroLoginSescAP>
    {
        public CadastroLoginSescAPConfiguracao()
        {
        }

        public void Configure(EntityTypeBuilder<CadastroLoginSescAP> builder)
        {

            builder.ToTable("CADASTRO_LOGIN_SESCAP");

            /*
             * -> chave primária 
             */
            builder.HasKey(cd => cd.ID);

            builder.Property(cd => cd.MATRICULA).HasMaxLength(13).IsRequired();
            builder.Property(cd => cd.EMAIL).HasMaxLength(255).IsRequired();
            builder.Property(cd => cd.CPF).HasMaxLength(14).IsRequired();
            builder.Property(cd => cd.SENHA).HasMaxLength(255).IsRequired();
        }
    }
}
