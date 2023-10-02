using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SESCAP.Ecommerce.Models;

namespace SESCAP.Ecommerce.Database
{
    public class MoedaPgtoConfiguracao : IEntityTypeConfiguration<MOEDAPGTO>
    {
        public MoedaPgtoConfiguracao()
        {
        }

        public void Configure(EntityTypeBuilder<MOEDAPGTO> builder)
        {
            builder.ToTable("MOEDAPGTO");

            /*
            * -> chave primária 
            */
            builder.HasKey( m => m.CDMOEDAPGT);

            builder.Property(m => m.CDMOEDAPGT).IsRequired();
            builder.Property(m => m.DSMOEDAPGT).HasMaxLength(50).IsRequired();
            builder.Property(m => m.DTATU).IsRequired();
            builder.Property(m => m.HRATU).IsRequired();
            builder.Property(m => m.LGATU).HasMaxLength(10).IsRequired();
            builder.Property(m => m.VBOBS).HasDefaultValue(0).IsRequired();
            builder.Property(m => m.OBSRECIBO).HasMaxLength(200);
            builder.Property(m => m.IDOBS).HasMaxLength(3);
            builder.Property(m => m.VBTPCONC).HasDefaultValue(0).IsRequired();
            builder.Property(m => m.TPCONTRL).HasDefaultValue(0).IsRequired();
            builder.Property(m => m.VBATIVO).HasDefaultValue(1).IsRequired();
            builder.Property(m => m.GRCONTA_AR);
            builder.Property(m => m.CDCONTA_AR).HasMaxLength(11);
            builder.Property(m => m.MAREFINI_AR);
            builder.Property(m => m.NUELEM_AR);


        }
    }
}
