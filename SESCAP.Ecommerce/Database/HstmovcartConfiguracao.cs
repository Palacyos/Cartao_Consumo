using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SESCAP.Ecommerce.Models;

namespace SESCAP.Ecommerce.Database
{
    public class HstmovcartConfiguracao : IEntityTypeConfiguration<HSTMOVCART>
    {
        public void Configure(EntityTypeBuilder<HSTMOVCART> builder)
        {
            builder.ToTable("HSTMOVCART");

            /*
             * -> chave primária composta
             */
            builder.HasKey(hst => new { hst.CDPRODUTO, hst.DTMOVIMENT, hst.NUMCARTAO, hst.SQMOVIMENT });

            builder.Property(hst => hst.CDPRODUTO).IsRequired();
            builder.Property(hst => hst.DTMOVIMENT).HasMaxLength(4).IsRequired();
            builder.Property(hst => hst.NUMCARTAO).HasColumnType("INTEGER").IsRequired();
            builder.Property(hst => hst.SQMOVIMENT).HasMaxLength(2).IsRequired();
            builder.Property(hst => hst.VBCREDEB).HasMaxLength(2).IsRequired();
            builder.Property(hst => hst.HRMOVIMENT).HasMaxLength(3).IsRequired();
            builder.Property(hst => hst.TPMOVIMENT).HasMaxLength(2).IsRequired();
            builder.Property(hst => hst.QTDPRODMOV).HasColumnType("decimal(10,3)").IsRequired();
            builder.Property(hst => hst.VLPRODMOV).HasColumnType("decimal(15,2)").IsRequired();
            builder.Property(hst => hst.OBSMOVIMEN).HasMaxLength(200).IsRequired();
            builder.Property(hst => hst.DTATU).HasMaxLength(4).IsRequired();
            builder.Property(hst => hst.HRATU).HasMaxLength(3).IsRequired();
            builder.Property(hst => hst.LGATU).HasMaxLength(10).IsRequired();
            builder.Property(hst => hst.SQCAIXA).IsRequired();
            builder.Property(hst => hst.IDCHECKOUT).IsRequired();
            builder.Property(hst => hst.IDHSTMOVCART).HasMaxLength(4).HasDefaultValue(0).IsRequired();
            builder.Property(hst => hst.CDPESSOA);
            builder.Property(hst => hst.IDUSUARIO).HasMaxLength(10);


            /*
             * -> relacionamento 1:N CACAIXA-HSTMOVCART
             */
            builder.HasOne(hst => hst.CACAIXA)
                .WithMany(ca => ca.HSTMOVCARTS)
                .HasForeignKey(hst => new { hst.CDPESSOA, hst.SQCAIXA })
                .HasConstraintName("FK_CACAIXA_HSTMOVC");

            /*
             * -> relacionamento 1:N CARTAO-HSTMOVCART
             */
            builder.HasOne(hst => hst.CARTAO)
                .WithMany(cart => cart.HSTMOVCARTS)
                .HasForeignKey(hst => hst.NUMCARTAO)
                .HasConstraintName("FK_CARTAO_HSTMOVCA");

            /*
             * -> relacionamento 1:N PRODUTOPDV-HSTMOVCART
             */
            builder.HasOne(hst => hst.PRODUTOPDV)
                .WithMany(pdv => pdv.HSTMOVCARTS)
                .HasForeignKey(hst => hst.CDPRODUTO)
                .HasConstraintName("FK_PRODUTOPDV_HSTM");

        }
    }
}
