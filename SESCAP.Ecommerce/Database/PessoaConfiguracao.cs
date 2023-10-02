using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SESCAP.Ecommerce.Models;

namespace SESCAP.Ecommerce.Database
{
    public class PessoaConfiguracao : IEntityTypeConfiguration<PESSOA>
    {
        public void Configure(EntityTypeBuilder<PESSOA> builder)
        {
            builder.ToTable("PESSOA");

            /*
             * -> chave primária
             */
            builder.HasKey(p => p.CDPESSOA);

            builder.Property(p => p.CDADMIN).HasMaxLength(2);
            builder.Property(p => p.NMPESSOA).HasMaxLength(80);
            builder.Property(p => p.CDPESSOA).HasMaxLength(4).IsRequired();
            builder.Property(p => p.DTCADASTRO).HasMaxLength(4);
            builder.Property(p => p.DTATU).HasMaxLength(4).IsRequired();
            builder.Property(p => p.HRATU).HasMaxLength(3).IsRequired();
            builder.Property(p => p.TPPESSOA).HasMaxLength(2).IsRequired();
            builder.Property(p => p.LGATU).HasMaxLength(10).IsRequired();


            /*
             * -> relacionamento 1:N ADMIN-PESSOA
             */
            builder.HasOne(p => p.ADMIN)
                .WithMany(ad => ad.PESSOAS)
                .HasForeignKey(p => p.CDADMIN)
                .HasConstraintName("FK_ADMIN_49");
        }
    }
}
