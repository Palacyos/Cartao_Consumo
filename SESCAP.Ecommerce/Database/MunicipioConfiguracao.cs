using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SESCAP.Ecommerce.Models;

namespace SESCAP.Ecommerce.Database
{
    public class MunicipioConfiguracao : IEntityTypeConfiguration<MUNICIPIO>
    {
        public void Configure(EntityTypeBuilder<MUNICIPIO> builder)
        {

            builder.ToTable("MUNICIPIO");

            /*
             * -> chave primária
             */
            builder.HasKey(m => m.CDMUNICIP);

            builder.Property(m => m.CDMUNICIP).HasMaxLength(2).IsRequired();
            builder.Property(m => m.CDMUNINSS).HasMaxLength(2);
            builder.Property(m => m.SIESTADO).HasMaxLength(2);
            builder.Property(m => m.DSMUNICIP).HasMaxLength(50);
            builder.Property(m => m.DSMUNMASK).HasMaxLength(15);
            builder.Property(m => m.DTATU).HasMaxLength(4);
            builder.Property(m => m.HRATU).HasMaxLength(3);
            builder.Property(m => m.LGATU).HasMaxLength(10);


            /*
             * -> relacioanmento 1:N ESTADO-MUNICIPIO
             */
            builder.HasOne(m => m.ESTADO)
                .WithMany(e => e.MUNICIPIOS)
                .HasForeignKey(m => m.SIESTADO)
                .HasConstraintName("FK_ESTADO_7");



        }
    }
}
