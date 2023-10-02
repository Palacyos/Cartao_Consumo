using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SESCAP.Ecommerce.Models;

namespace SESCAP.Ecommerce.Database
{
    public class CxDepRetPdvConfiguracao : IEntityTypeConfiguration<CXDEPRETPDV>
    {
        public void Configure(EntityTypeBuilder<CXDEPRETPDV> builder)
        {
            builder.ToTable("CXDEPRETPDV");

            /*
            * -> chave primária composta 
            */

            builder.HasKey(cx => new { cx.SQCAIXA, cx.CDPESSOA, cx.TPDEPRET, cx.SQDEPRET });

            builder.Property(cx => cx.TPDEPRET).IsRequired();
            builder.Property(cx => cx.SQDEPRET).IsRequired();
            builder.Property(cx => cx.IDUSUARIO).HasMaxLength(10);
            builder.Property(cx => cx.SQCAIXA).IsRequired();
            builder.Property(cx => cx.VLDEPRET).HasColumnType("decimal(15,2)").IsRequired();
            builder.Property(cx => cx.VLENCARGOS).HasColumnType("decimal(15,2)").IsRequired();
            builder.Property(cx => cx.DTDEPRET).IsRequired();
            builder.Property(cx => cx.HRDEPRET).IsRequired();
            builder.Property(cx => cx.STDEPRET).IsRequired();
            builder.Property(cx => cx.DSSTATUS).HasMaxLength(100);
            builder.Property(cx => cx.CDMOEDAPGT).IsRequired();
            builder.Property(cx => cx.NUMCARTAO).IsRequired();
            builder.Property(cx => cx.CDPESSOA).IsRequired();


            /*
            * -> relacionamento 1:N CACAIXA->CXDEPRETPDV
            */
            builder.HasOne(cx => cx.CACAIXA)
                .WithMany(c => c.CXDEPRETPDVs)
                .HasForeignKey(cx => new { cx.CDPESSOA, cx.SQCAIXA })
                .HasConstraintName("FK_CACAIXA_CXDEPRE");

            /*
            * -> relacionamento 1:N CARTAO->CXDEPRETPDV
            */
            builder.HasOne(cx => cx.CARTAO)
              .WithMany(cart => cart.CXDEPRETPDVs)
              .HasForeignKey(cx => cx.NUMCARTAO)
              .HasConstraintName("FK_CARTAO_CXDEPRET");



        }
    }
}
