using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SESCAP.Ecommerce.Models;

namespace SESCAP.Ecommerce.Database
{
    public class CaixaLancaConfiguracao : IEntityTypeConfiguration<CAIXALANCA>
    {
		public CaixaLancaConfiguracao()
		{
		}

        public void Configure(EntityTypeBuilder<CAIXALANCA> builder)
        {
            builder.ToTable("CAIXALANCA");

            /*
            * -> chave primária composta
            */  
            builder.HasKey(cl => new {cl.SQLANCAMEN, cl.SQCAIXA, cl.CDPESSOA });

            builder.Property(cl => cl.IDUSUARIO).HasMaxLength(10);
            builder.Property(cl => cl.SQCAIXA).IsRequired();
            builder.Property(cl => cl.SQLANCAMEN).IsRequired();
            builder.Property(cl => cl.TPLANCAMEN).IsRequired();
            builder.Property(cl => cl.IDUSRLANCA).HasMaxLength(10).IsRequired();
            builder.Property(cl => cl.DTLANCAMEN).IsRequired();
            builder.Property(cl => cl.HRLANCAMEN).IsRequired();
            builder.Property(cl => cl.DSLANCAMEN).HasMaxLength(200).IsRequired();
            builder.Property(cl => cl.VLLANCAMEN).HasColumnType("decimal(15,2)").IsRequired();
            builder.Property(cl => cl.STLANCAMEN).IsRequired();
            builder.Property(cl => cl.DSSTATUS).HasMaxLength(200);
            builder.Property(cl => cl.CDPESSOA).IsRequired();


            /*
            * -> relacionamento 1:N PESSOA->CAIXALANCA
            */
            builder.HasOne(cl => cl.PESSOA)
                  .WithMany(p => p.LANCAMENTOSCAIXA)
                  .HasForeignKey(cl => cl.CDPESSOA)
                  .HasConstraintName("FK_PESSOA_CAIXALAN");

           /*
           * -> relacionamento 1:N CACAIXA->CAIXALANCA
           */
            builder.HasOne(cl => cl.CAIXA)
                .WithMany(cx => cx.LANCAMENTOSCAIXA)
                .HasForeignKey(cl => new { cl.CDPESSOA, cl.SQCAIXA })
                .HasConstraintName("FK_CACAIXA_CAIXALA");



        }
    }
}

