using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SESCAP.Ecommerce.Models;

namespace SESCAP.Ecommerce.Database
{
    public class ProdutoPdvConfiguracao :IEntityTypeConfiguration<PRODUTOPDV>
    {
        public ProdutoPdvConfiguracao()
        {
        }

        public void Configure(EntityTypeBuilder<PRODUTOPDV> builder)
        {

            builder.ToTable("PRODUTOPDV");

            /*
             * -> Chave primária
             */
            builder.HasKey(pdv => pdv.CDPRODUTO);

            builder.Property(pdv => pdv.CDPRODUTO).IsRequired();
            builder.Property(pdv => pdv.DSPRODUTO).HasMaxLength(80).IsRequired();
            builder.Property(pdv => pdv.DTATU).HasMaxLength(4).IsRequired();
            builder.Property(pdv => pdv.TPCTRLSALD).HasMaxLength(2).IsRequired();
            builder.Property(pdv => pdv.HRATU).HasMaxLength(3).IsRequired();
            builder.Property(pdv => pdv.TPACAOSALD).HasMaxLength(2).IsRequired();
            builder.Property(pdv => pdv.LGATU).HasMaxLength(10).IsRequired();
            builder.Property(pdv => pdv.STPRODUTO).HasMaxLength(2).IsRequired();
            builder.Property(pdv => pdv.DTCANCELAD).HasMaxLength(4);
            builder.Property(pdv => pdv.CDBARRA).HasMaxLength(13);
            builder.Property(pdv => pdv.VBQUILO).HasMaxLength(2).IsRequired();
            builder.Property(pdv => pdv.CDGRUPOPDV).HasMaxLength(4).IsRequired();
            builder.Property(pdv => pdv.CDSUBGRPDV).HasMaxLength(4).IsRequired();
            builder.Property(pdv => pdv.VBRECAGR).HasMaxLength(2).IsRequired();
            builder.Property(pdv => pdv.CDUNIDADE).HasMaxLength(4).HasDefaultValue("ZZZZ").IsRequired();
            builder.Property(pdv => pdv.TPCOMPROD).HasMaxLength(2).HasDefaultValue(0).IsRequired();
            builder.Property(pdv => pdv.VBPREPARO).HasMaxLength(2).HasDefaultValue(0).IsRequired();

        }
    }
}
