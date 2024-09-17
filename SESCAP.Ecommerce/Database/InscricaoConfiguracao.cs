using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SESCAP.Ecommerce;

public class InscricaoConfiguracao : IEntityTypeConfiguration<INSCRICAO>
{
    public void Configure(EntityTypeBuilder<INSCRICAO> builder)
    {
        builder.ToTable("INSCRICAO");

        builder.HasKey(i => new {i.CDPROGRAMA, i.CDUOP, i.SQMATRIC, i.CDCONFIG, i.SQOCORRENC});

        builder.Property(i => i.CDUOP).IsRequired();
        builder.Property(i => i.CDPROGRAMA).IsRequired();
        builder.Property(i => i.SQMATRIC).IsRequired();
        builder.Property(i => i.CDCONFIG).IsRequired();
        builder.Property(i => i.SQOCORRENC).IsRequired();
        builder.Property(i => i.CDDESCONTO);
        builder.Property(i => i.CDFONTEINF).IsRequired();
        builder.Property(i => i.CDFORMATO).IsRequired();
        builder.Property(i => i.CDPERFIL);
        builder.Property(i => i.STINSCRI).IsRequired();
        builder.Property(i => i.DTPREINSCR);
        builder.Property(i => i.DTINSCRI).IsRequired();
        builder.Property(i => i.LGINSCRI).HasMaxLength(10).IsRequired();
        builder.Property(i => i.DTPRIVENCT);
        builder.Property(i => i.NUCOBRANCA).IsRequired();
        builder.Property(i => i.CDUOPINSC).IsRequired();
        builder.Property(i => i.DTSTATUS).IsRequired();
        builder.Property(i => i.HRSTATUS).IsRequired();
        builder.Property(i => i.LGSTATUS).HasMaxLength(10).IsRequired();
        builder.Property(i => i.CDUOPSTAT).IsRequired();
        builder.Property(i => i.DSSTATUS).HasMaxLength(200);
        builder.Property(i => i.STCANCELAD);
        builder.Property(i => i.CDCATEGORI);

        builder.HasOne(i => i.CATEGORIA)
        .WithMany(ct => ct.INSCRICOES)
        .HasForeignKey(i => i.CDCATEGORI)
        .HasConstraintName("FK_CATEG_INSCRICAO");

        builder.HasOne(i => i.CLIENTELA)
        .WithMany(c => c.INSCRICOES)
        .HasForeignKey( i => new {i.SQMATRIC, i.CDUOP})
        .HasConstraintName("FK_CLIENTELA_21");

        builder.HasOne(i => i.PROGOCORR)
        .WithMany(po => po.INSCRICOES)
        .HasForeignKey(i => new {i.CDPROGRAMA, i.CDCONFIG, i.SQOCORRENC})
        .HasConstraintName("FK_PROGOCORR_31");

    }
}
