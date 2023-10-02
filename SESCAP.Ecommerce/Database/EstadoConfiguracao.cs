using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SESCAP.Ecommerce.Models;

namespace SESCAP.Ecommerce.Database
{
    public class EstadoConfiguracao : IEntityTypeConfiguration<ESTADO>
    {
        public void Configure(EntityTypeBuilder<ESTADO> builder)
        {
            builder.ToTable("ESTADO");

            /*
             * -> chave primária
             */
            builder.HasKey(e => e.SIESTADO);

            builder.Property(e => e.SIESTADO).HasMaxLength(2).IsRequired();
            builder.Property(e => e.CDESTADO).HasMaxLength(2);
            builder.Property(e => e.CDREGIAO).HasMaxLength(2).IsRequired();
            builder.Property(e => e.DSESTADO).HasMaxLength(45);
            builder.Property(e => e.CDINSS).HasMaxLength(2);
            builder.Property(e => e.DSUFMASK).HasMaxLength(15);
            builder.Property(e => e.LGATU).HasMaxLength(10);
            builder.Property(e => e.DTATU).HasMaxLength(4);
            builder.Property(e => e.HRATU).HasMaxLength(3);
            builder.Property(e => e.CDMUNICIP).HasMaxLength(2);
        }
    }
}
