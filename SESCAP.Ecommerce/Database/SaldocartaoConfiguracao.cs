using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SESCAP.Ecommerce.Models;

namespace SESCAP.Ecommerce.Database
{
    public class SaldocartaoConfiguracao: IEntityTypeConfiguration<SALDOCARTAO>
    {
      

        public void Configure(EntityTypeBuilder<SALDOCARTAO> builder)
        {
            builder.ToTable("SALDOCARTAO");

            /*
             * -> chave primária composta 
             */
            builder.HasKey(sld => new { sld.NUMCARTAO, sld.CDPRODUTO});

            builder.Property(sld => sld.NUMCARTAO).IsRequired();
            builder.Property(sld => sld.CDPRODUTO).IsRequired();
            builder.Property(sld => sld.SLDQTCART).HasColumnType("decimal(15,2)").IsRequired();
            builder.Property(sld => sld.SLDQTBLOQ).HasColumnType("decimal(15,2)").IsRequired();
            builder.Property(sld => sld.SLDVLCART).HasColumnType("decimal(15,2)").IsRequired();
            builder.Property(sld => sld.SLDVLBLOQ).HasColumnType("decimal(15,2)").IsRequired();



            /*
            * -> relacionamento 1:1 CARTAO->SALDOCARTAO
            */
            builder.HasOne(sld => sld.CARTAO)
                .WithOne(cart => cart.SALDOCARTAO)
                .HasForeignKey<SALDOCARTAO>(sld => sld.NUMCARTAO)
                .HasConstraintName("FK_CARTAO_SALDOCAR");


            /*
            * -> relacionamento 1:1 PRODUTOPDV->SALDOCARTAO
            */
            builder.HasOne(sld => sld.PRODUTOPDV)
                .WithOne(pdv => pdv.SALDOCARTAO)
                .HasForeignKey<SALDOCARTAO>(sld => sld.CDPRODUTO)
                .HasConstraintName("FK_PRODUTOPDV_SLD");


        }
    }
}
