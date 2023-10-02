using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SESCAP.Ecommerce.Models;

namespace SESCAP.Ecommerce.Database
{
    public class CategoriaConfiguracao : IEntityTypeConfiguration<CATEGORIA>
    {
        public void Configure(EntityTypeBuilder<CATEGORIA> builder)
        {

            builder.ToTable("CATEGORIA");

            /*
             * -> chave primária 
             */
            builder.HasKey(categ => categ.CDCATEGORI);

            builder.Property(categ => categ.CDCATEGORI).IsRequired();
            builder.Property(categ => categ.DSCATEGORI).HasMaxLength(80);
            builder.Property(categ => categ.TPCATEGORI).IsRequired();
            builder.Property(categ => categ.DTATU).IsRequired();
            builder.Property(categ => categ.CDIMPRESS).HasMaxLength(20);
            builder.Property(categ => categ.HRATU).IsRequired();
            builder.Property(categ => categ.LGATU).HasMaxLength(10).IsRequired();
            builder.Property(categ => categ.VBCATSERV).HasDefaultValue(0).IsRequired();
            builder.Property(categ => categ.VBATIVA).HasDefaultValue(1).IsRequired();
            builder.Property(categ => categ.VBPDV).HasDefaultValue(0).IsRequired();
            builder.Property(categ => categ.VBEMPOBR).HasDefaultValue(1).IsRequired();
            builder.Property(categ => categ.IDCATEGORI).HasDefaultValue(0).IsRequired();
            builder.Property(categ => categ.VBCATCONV).HasDefaultValue(0).IsRequired();

        }
    }
}
