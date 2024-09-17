using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SESCAP.Ecommerce;

public class ResponsaveisConfiguracao : IEntityTypeConfiguration<RESPONSAVEIS>
{
    public void Configure(EntityTypeBuilder<RESPONSAVEIS> builder)
    {
        builder.ToTable("RESPONSAVEIS");

        /*
        * -> chave primária 
        */
        builder.HasKey(r => r.NUCPF);

        builder.Property(r =>r.VBATIVO).IsRequired();
        builder.Property(r =>r.NUCPF).HasMaxLength(11).IsRequired();
        builder.Property(r => r.NUREGGERAL).HasMaxLength(15).IsRequired();
        builder.Property(r => r.DTEMIRG).IsRequired();
        builder.Property(r => r.IDORGEMIRG).HasMaxLength(15).IsRequired();
        builder.Property(r => r.NMRESPONSA).HasMaxLength(80).IsRequired();
        builder.Property(r => r.DTATU).IsRequired();
        builder.Property(r => r.HRATU).IsRequired();
        builder.Property(r => r.LGATU).HasMaxLength(10).IsRequired();


    }
}
