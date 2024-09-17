using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SESCAP.Ecommerce;

public class RespcliConfiguracao : IEntityTypeConfiguration<RESPCLI>
{
    public void Configure(EntityTypeBuilder<RESPCLI> builder)
    {
        builder.ToTable("RESPCLI");

        /*
        * -> chave primária 
        */
        builder.HasKey(rc => new { rc.SQMATRIC, rc.CDUOP });

        builder.Property(rc => rc.SQMATRIC).IsRequired();
        builder.Property(rc => rc.CDUOP).IsRequired();
        builder.Property(rc => rc.NUCPF).HasMaxLength(11).IsRequired();
        builder.Property(rc =>rc.DTATU).IsRequired();
        builder.Property(rc => rc.HRATU).IsRequired();
        builder.Property(rc => rc.LGATU).HasMaxLength(10).IsRequired();


        /*
        * -> relacionamento 1:N RESPONSAVEIS -> RESPCLI
        */
        builder.HasOne(rc => rc.RESPONSAVEIS)
        .WithMany(resp => resp.RESPCLIs)
        .HasForeignKey(rc => rc.NUCPF)
        .HasConstraintName("FK_RESPONSAVEIS_1");

        /*
        * -> relacionamento 1:N CLIENTELA -> RESPCLI
        */
        builder.HasOne(rc => rc.CLIENTELA)
        .WithMany(c => c.RESPCLIs)
        .HasForeignKey(rc => new {rc.SQMATRIC, rc.CDUOP})
        .HasConstraintName("FK_CLIENTELA_27");
    }
}
