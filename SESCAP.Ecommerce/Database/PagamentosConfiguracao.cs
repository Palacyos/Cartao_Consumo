using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SESCAP.Ecommerce;

public class PagamentosConfiguracao : IEntityTypeConfiguration<PAGAMENTOS>
{
    public void Configure(EntityTypeBuilder<PAGAMENTOS> builder)
    {
       builder.ToTable("PAGAMENTOS");

       builder.HasKey(p => new {p.SQCAIXA, p.CDPESSOA, p.SQPAGAMENT, p.CDMOEDAPGT});

       builder.Property(p => p.IDUSUARIO).HasMaxLength(10);
       builder.Property(p => p.DTRECEBIDO).IsRequired();
       builder.Property(p => p.SQCAIXA).IsRequired();
       builder.Property(p => p.HRRECEBIDO).IsRequired();
       builder.Property(p => p.CDMOEDAPGT).IsRequired();
       builder.Property(p => p.SQPAGAMENT).IsRequired();
       builder.Property(p => p.IDCLASSE).HasMaxLength(5).IsRequired();
       builder.Property(p => p.CDELEMENT).HasMaxLength(30).IsRequired();
       builder.Property(p => p.SQCOBRANCA).IsRequired();
       builder.Property(p => p.VLRECEBIDO).HasColumnType("DECIMAL(15,2)").IsRequired();
       builder.Property(p => p.VLJUROS).HasColumnType("DECIMAL(15,2)");
       builder.Property(p => p.SMFIELDATU).IsRequired();
       builder.Property(p => p.STCANCELAD).IsRequired();
       builder.Property(p => p.DSCANCELAD).HasMaxLength(200);
       builder.Property(p => p.VLACRESCIM).HasColumnType("DECIMAL(15,2)");
       builder.Property(p => p.VLDESCONTO).HasColumnType("DECIMAL(15,2)");
       builder.Property(p => p.NUIMPVIA2).IsRequired();
       builder.Property(p => p.CDUOPPGTO).IsRequired();
       builder.Property(p => p.SQVENDA);
       builder.Property(p => p.LGCANCEL).HasMaxLength(10);
       builder.Property(p => p.DTCANCEL);
       builder.Property(p => p.HRCANCEL);
       builder.Property(p => p.NMESTACAO).HasMaxLength(50);
       builder.Property(p => p.CDPESSOA).IsRequired();
       builder.Property(p => p.IDCOBRANCA);

       builder.HasOne(p => p.COBRANCA)
       .WithMany(c => c.PAGAMENTOS)
       .HasForeignKey(p => new {p.IDCLASSE, p.CDELEMENT, p.SQCOBRANCA})
       .HasConstraintName("FK_COBRANCA_3");

       builder.HasOne(pg => pg.PESSOA)
       .WithMany(p => p.PAGAMENTOS)
       .HasForeignKey(pg => pg.CDPESSOA)
       .HasConstraintName("FK_USUARIO_PAGAMEN");

       builder.HasOne(p => p.CACAIXA)
       .WithMany(c => c.PAGAMENTOS)
       .HasForeignKey(p => new {p.CDPESSOA, p.SQCAIXA})
       .HasConstraintName("FK_CACAIXA_PAGAMEN");
    }
}
