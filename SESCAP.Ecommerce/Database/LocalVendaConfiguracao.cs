using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SESCAP.Ecommerce.Models;

namespace SESCAP.Ecommerce.Database
{
    public class LocalVendaConfiguracao : IEntityTypeConfiguration<LOCALVENDA>
	{
		
        public void Configure(EntityTypeBuilder<LOCALVENDA> builder)
        {
            builder.ToTable("LOCALVENDA");

            /*
             * -> chave primária 
             */
            builder.HasKey(lv => lv.CDLOCVENDA);

            builder.Property(lv => lv.CDLOCVENDA).IsRequired();
            builder.Property(lv => lv.CDUOP).IsRequired();
            builder.Property(lv => lv.DSLOCVENDA).HasMaxLength(50).IsRequired();
            builder.Property(lv => lv.DTATU).IsRequired();
            builder.Property(lv => lv.HRATU).IsRequired();
            builder.Property(lv => lv.LGATU).HasMaxLength(10).IsRequired();
            builder.Property(lv => lv.MSGCUPOM).HasMaxLength(200);
            builder.Property(lv => lv.STLOCVENDA).HasDefaultValue(1).IsRequired();


            /*
             * -> relacionamento 1:N UOP-LOCALVENDA
             */
            builder.HasOne(lv => lv.UOP)
                .WithMany(uop => uop.LOCALVENDAS)
                .HasForeignKey(lv => lv.CDUOP)
                .HasConstraintName("FK_UOP_16");


        }
    }
}

