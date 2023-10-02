using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SESCAP.Ecommerce.Models;

namespace SESCAP.Ecommerce.Database
{
    public class CacaixaConfiguracao : IEntityTypeConfiguration<CACAIXA>
    {
        public void Configure(EntityTypeBuilder<CACAIXA> builder)
        {
            builder.ToTable("CACAIXA");

            /*
             * -> chave primária composta
             */
            builder.HasKey(ca => new { ca.CDPESSOA, ca.SQCAIXA });

            builder.Property(ca => ca.IDUSUARIO).HasMaxLength(10);
            builder.Property(ca => ca.SQCAIXA).HasMaxLength(4).IsRequired();
            builder.Property(ca => ca.DTABERTURA).IsRequired();
            builder.Property(ca => ca.NUFECHAMEN).HasMaxLength(4).IsRequired();
            builder.Property(ca => ca.HRABERTURA).HasMaxLength(3).IsRequired();
            builder.Property(ca => ca.DTFECHAMEN).HasMaxLength(4);
            builder.Property(ca => ca.HRFECHAMEN).HasMaxLength(3);
            builder.Property(ca => ca.STCAIXA).HasMaxLength(2).IsRequired();
            builder.Property(ca => ca.VLSALDOANT).HasColumnType("decimal(15,2)").IsRequired();
            builder.Property(ca => ca.VLSALDOATU).HasColumnType("decimal(15,2)");
            builder.Property(ca => ca.SMFIELDATU).HasMaxLength(8).IsRequired();
            builder.Property(ca => ca.CDPDV);
            builder.Property(ca => ca.CDLOCVENDA).HasMaxLength(4);
            builder.Property(ca => ca.CDUOP).HasMaxLength(4).HasDefaultValue(84).IsRequired();
            builder.Property(ca => ca.LGFECHAMEN).HasMaxLength(10);
            builder.Property(ca => ca.NMESTACAO).HasMaxLength(25);
            builder.Property(ca => ca.CDPESSOA).HasMaxLength(4).IsRequired();
            builder.Property(ca => ca.DTAUTORIZ).HasMaxLength(4);
            builder.Property(ca => ca.CDUNIDADE).HasMaxLength(2);
            builder.Property(ca => ca.NUAUTORIZ).HasMaxLength(2);
            builder.Property(ca => ca.TPAUTORIZ).HasMaxLength(1);


            /*
             * -> relacionamento 1:N PESSOA->CACAIXA
             */
            builder.HasOne(ca => ca.PESSOA)
               .WithMany(p => p.CACAIXAS)
               .HasForeignKey(ca => ca.CDPESSOA)
               .HasConstraintName("FK_PESSOA_CACAIXA");


            /*
             * -> relacionamento 1:N UOP->CACAIXA
             */
            builder.HasOne(ca => ca.UOP)
                .WithMany(uop => uop.CACAIXAS)
                .HasForeignKey(ca => ca.CDUOP)
                .HasConstraintName("FK_UOP_4");

            /*
             * -> relacionamento 1:N CAPDV->CACAIXA
             */
            builder.HasOne(ca => ca.CAPDV)
                .WithMany(cpdv => cpdv.CACAIXAS)
                .HasForeignKey(ca => ca.CDPDV)
                .HasConstraintName("FK_CAPDV_CACAIXA");
                


        }
    }
}
