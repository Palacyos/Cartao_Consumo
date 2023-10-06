using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SESCAP.Ecommerce.Models;

namespace SESCAP.Ecommerce.Database
{
    public class CartCredConfiguracao : IEntityTypeConfiguration<CARTCRED>
    {
        public void Configure(EntityTypeBuilder<CARTCRED> builder)
        {
            builder.ToTable("CARTCRED");


            /*
           * -> chave primária composta 
           */
            builder.HasKey(cc => new { cc.NUMCARTAO, cc.CDPRODUTO });

            builder.Property(cc => cc.NUMCARTAO).IsRequired();
            builder.Property(cc => cc.CDPRODUTO).IsRequired();
            builder.Property(cc => cc.QTDPRODCRE).HasColumnType("decimal(10,2)");
            builder.Property(cc => cc.VALPRODCRE).HasColumnType("decimal(15,2)");
            builder.Property(cc => cc.QTDPRODBLO).HasColumnType("decimal(10,2)");
            builder.Property(cc => cc.VBATIVO).IsRequired();
            builder.Property(cc => cc.VALPRODBLO).HasColumnType("decimal(15,2)");
            builder.Property(cc => cc.DTATU).IsRequired();
            builder.Property(cc => cc.HRATU).IsRequired();
            builder.Property(cc => cc.LGATU).HasMaxLength(10).IsRequired();


            /*
           * -> relacionamento 1:1 CARTAO->CARTCRED
           */
            builder.HasOne(cc => cc.CARTAO)
                .WithOne(cart => cart.CARTCRED)
                .HasForeignKey<CARTCRED>(cc => cc.NUMCARTAO)
                .HasConstraintName("FK_CARTAO_2");


            /*
           * -> relacionamento 1:1 PRODUTOPDV->CARTCRED
           */
            builder.HasOne(cc => cc.PRODUTOPDV)
                .WithOne(pdv => pdv.CARTCRED)
                .HasForeignKey<CARTCRED>(cc => cc.CDPRODUTO)
                .HasConstraintName("FK_PRODUTOPDV_1");

        }
    }
}

