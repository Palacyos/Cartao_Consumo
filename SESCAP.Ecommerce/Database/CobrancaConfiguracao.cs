using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SESCAP.Ecommerce;

public class CobrancaConfiguracao : IEntityTypeConfiguration<COBRANCA>
{
    public void Configure(EntityTypeBuilder<COBRANCA> builder)
    {
        builder.ToTable("COBRANCA");

        builder.HasKey(c => new {c.IDCLASSE, c.CDELEMENT, c.SQCOBRANCA});

        builder.Property(c => c.IDCLASSE).HasMaxLength(5).IsRequired();
        builder.Property(c => c.CDELEMENT).HasMaxLength(30).IsRequired();
        builder.Property(c => c.SQCOBRANCA).IsRequired();
        builder.Property(c => c.CDUOPCOB).IsRequired();
        builder.Property(c => c.CDUOP);
        builder.Property(c => c.SQMATRIC);
        builder.Property(c => c.DSCOBRANCA).HasMaxLength(80).IsRequired();
        builder.Property(c => c.RFCOBRANCA).IsRequired();
        builder.Property(c => c.VLCOBRADO).HasColumnType("DECIMAL(15,2)").IsRequired();
        builder.Property(c => c.DTVENCTO).IsRequired();
        builder.Property(c =>c.DTEMISSAO).IsRequired();
        builder.Property(c => c.STRECEBIDO).IsRequired();
        builder.Property(c => c.TPCOBRANCA);
        builder.Property(c => c.PCJUROS).HasColumnType("DECIMAL(10,4)");
        builder.Property(c => c.DTATU).IsRequired();
        builder.Property(c => c.SMFIELDATU).IsRequired();
        builder.Property(c => c.HRATU).IsRequired();
        builder.Property(c => c.LGATU).HasMaxLength(10).IsRequired();
        builder.Property(c => c.VLCARACTE1).HasMaxLength(40);
        builder.Property(c => c.VLCARACTE2).HasMaxLength(40);
        builder.Property(c => c.DDCOBJUROS);
        builder.Property(c => c.DDINIJUROS);
        builder.Property(c => c.PCMULTA).HasColumnType("DECIMAL(10,4)");
        builder.Property(c => c.DSCANCELAM).HasMaxLength(200);
        builder.Property(c => c.CDUOPREC).HasDefaultValue(84).IsRequired();
        builder.Property(c => c.NMESTACAO).HasMaxLength(50);
        builder.Property(c => c.CDCANCELA);
        builder.Property(c => c.TPMORA).HasDefaultValue(0).IsRequired();
        builder.Property(c => c.LGCANCEL).HasMaxLength(10).HasDefaultValue("").IsRequired();
        builder.Property(c => c.IDCOBRANCA);
        builder.Property(c => c.IDPEDIDO);

        builder.HasOne(c => c.CLIENTELA)
        .WithMany(cl => cl.COBRANCAS)
        .HasForeignKey( c => new {c.SQMATRIC, c.CDUOP})
        .HasConstraintName("FK_CLIENTELA_8");

        builder.HasOne(c =>c.UOP)
        .WithMany(u => u.COBRANCAS)
        .HasForeignKey(c => c.CDUOPREC)
        .HasConstraintName("FK_UOP_9");

    }
}
