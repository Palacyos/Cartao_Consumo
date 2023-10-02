using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SESCAP.Ecommerce.Models;

namespace SESCAP.Ecommerce.Database
{
    public class UopConfiguracao : IEntityTypeConfiguration<UOP>
    {
        public void Configure(EntityTypeBuilder<UOP> builder)
        {
            builder.ToTable("UOP");

            /*
             * -> chave primária 
             */
            builder.HasKey(u => u.CDUOP);

            builder.Property(u => u.CDUOP).IsRequired();
            builder.Property(u => u.NMUOP).HasMaxLength(50).IsRequired();
            builder.Property(u => u.CDADMIN).IsRequired();
            builder.Property(u => u.NUCGCUOP).HasMaxLength(14).IsRequired();
            builder.Property(u => u.HRATU).IsRequired();
            builder.Property(u => u.DTATU).IsRequired();
            builder.Property(u => u.LGATU).HasMaxLength(10).IsRequired();
            builder.Property(u => u.NUCGCANT).HasMaxLength(14).IsRequired();
            builder.Property(u => u.VBDR).HasDefaultValue(0).IsRequired();
            builder.Property(u => u.VBCOLFER).HasDefaultValue(0).IsRequired();
            builder.Property(u => u.NUELEM_AR);
            builder.Property(u => u.AAMODA_AR).HasMaxLength(4);
            builder.Property(u => u.CDMODA_AR);
            builder.Property(u => u.STUOP).HasDefaultValue(1).IsRequired();
            builder.Property(u => u.STREALIZAMATRICULA).HasDefaultValue(1).IsRequired();


            /*
             * -> relacionamento 1:N ADMIN-UOP 
             */
            builder.HasOne(uop => uop.ADMIN)
                .WithMany(ad => ad.UOPS)
                .HasForeignKey(uop => uop.CDADMIN)
                .HasConstraintName("FK_ADMIN_62");


        }
    }
}
