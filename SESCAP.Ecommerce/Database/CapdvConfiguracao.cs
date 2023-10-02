using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SESCAP.Ecommerce.Models;

namespace SESCAP.Ecommerce.Database
{
    public class CapdvConfiguracao : IEntityTypeConfiguration<CAPDV>
    {
        public void Configure(EntityTypeBuilder<CAPDV> builder)
        {
            builder.ToTable("CAPDV");

            /*
             * -> chave primária
             */
            builder.HasKey(cpdv => cpdv.CDPDV);

            builder.Property(cpdv => cpdv.CDPDV).IsRequired();
            builder.Property(cpdv => cpdv.CDLOCVENDA).HasMaxLength(4).IsRequired();
            builder.Property(cpdv => cpdv.DSPDV).HasMaxLength(50).IsRequired();
            builder.Property(cpdv => cpdv.NMESTACAO).HasMaxLength(70);
            builder.Property(cpdv => cpdv.VBHOTEL).HasMaxLength(2).IsRequired();
            builder.Property(cpdv => cpdv.DTATU).HasMaxLength(4).IsRequired();
            builder.Property(cpdv => cpdv.HRATU).HasMaxLength(3).IsRequired();
            builder.Property(cpdv => cpdv.LGATU).HasMaxLength(10).IsRequired();
            builder.Property(cpdv => cpdv.STPDV).HasMaxLength(2).IsRequired();


            /*
             * -> relacionamento 1:N LOCALVENDA-CAPDVS
             */
            builder.HasOne(cpdv => cpdv.LOCALVENDA)
                .WithMany(lv => lv.CAPDVS)
                .HasForeignKey(cpdv => cpdv.CDLOCVENDA)
                .HasConstraintName("FK_LOCALVENDA_1");
        }
    }
}
