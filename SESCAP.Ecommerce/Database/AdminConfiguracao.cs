using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SESCAP.Ecommerce.Models;

namespace SESCAP.Ecommerce.Database
{
    
    public class AdminConfiguracao : IEntityTypeConfiguration<ADMIN>
    {
        public void Configure(EntityTypeBuilder<ADMIN> builder)
        {
            builder.ToTable("ADMIN");

            /*
             * -> chave primária
             */
            builder.HasKey(ad => ad.CDADMIN);

            builder.Property(ad => ad.CDADMIN).HasMaxLength(2).IsRequired();
            builder.Property(ad => ad.NUORDEMANU).HasMaxLength(2);
            builder.Property(ad => ad.SIADMIN).HasMaxLength(2);
            builder.Property(ad => ad.NMADMIN).HasMaxLength(40).IsRequired();
            builder.Property(ad => ad.DTATU).HasMaxLength(4).IsRequired();
            builder.Property(ad => ad.HRATU).HasMaxLength(3).IsRequired();
            builder.Property(ad => ad.DTENDOMKT).HasMaxLength(4);
            builder.Property(ad => ad.HRENDOMKT).HasMaxLength(3);
            builder.Property(ad => ad.LGATU).HasMaxLength(10).IsRequired();
            builder.Property(ad => ad.NUCNPJ).HasMaxLength(15);
            builder.Property(ad => ad.SIESTADO).HasMaxLength(2);


            /*
             * relacionamento 1:1 ESTADO-ADMIN
             */
            builder.HasOne(a => a.ESTADO)
                .WithOne(e => e.ADMIN)
                .HasForeignKey<ADMIN>(a => a.SIESTADO)
                .HasConstraintName("FK_ESTADO_1");
        }
    }
}
