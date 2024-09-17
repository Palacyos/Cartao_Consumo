using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SESCAP.Ecommerce;

public class ProgocorrConfiguracao : IEntityTypeConfiguration<PROGOCORR>
{
    public void Configure(EntityTypeBuilder<PROGOCORR> builder)
    {
        builder.ToTable("PROGOCORR");


        /*
        * -> chave primária composta 
        */
        builder.HasKey(po => new { po.CDPROGRAMA, po.CDCONFIG, po.SQOCORRENC });

        builder.Property(po => po.CDPROGRAMA).IsRequired();
        builder.Property(po => po.CDCONFIG).IsRequired();
        builder.Property(po => po.SQOCORRENC).IsRequired();
        builder.Property(po => po.DSUSUARIO).HasMaxLength(80).IsRequired();
        builder.Property(po => po.DTLINSCRI);
        builder.Property(po => po.DTUINSCRI);
        builder.Property(po => po.DTLPREINSC);
        builder.Property(po => po.DTUPREINSC);
        builder.Property(po => po.DTLTRANC);
        builder.Property(po => po.DTUTRANC);
        builder.Property(po => po.DTINIOCORR);
        builder.Property(po => po.DTFIMOCORR);
        builder.Property(po => po.VBINSCRUOP).IsRequired();
        builder.Property(po => po.IDUSRRESP).HasMaxLength(10).IsRequired();
        builder.Property(po => po.NUVAGAS).IsRequired();
        builder.Property(po => po.NUVAGASOCP).HasDefaultValue(0).IsRequired();
        builder.Property(po => po.NUMINVOCUP).IsRequired();
        builder.Property(po => po.VBOCORRAPR).IsRequired();
        builder.Property(po => po.CDUOPCAD).IsRequired();
        builder.Property(po => po.DTAPROV);
        builder.Property(po => po.DTATU).IsRequired();
        builder.Property(po => po.HRATU).IsRequired();
        builder.Property(po => po.LGATU).HasMaxLength(10).IsRequired();
        builder.Property(po => po.DURAULA).HasDefaultValue(60).IsRequired();
        builder.Property(po => po.IDADEMIN).HasColumnType("DECIMAL(4,2)").HasDefaultValue(0.01).IsRequired();
        builder.Property(po => po.IDADEMAX).HasColumnType("DECIMAL(4,2)").HasDefaultValue(99.11).IsRequired();
        builder.Property(po => po.VBCANCELA).HasDefaultValue(0).IsRequired();
        builder.Property(po => po.AAMODA).HasMaxLength(4);
        builder.Property(po => po.CDMODA);
        builder.Property(po => po.VBBOLBAN).HasDefaultValue(1).IsRequired();
        builder.Property(po => po.DTINIFER);
        builder.Property(po => po.DTFIMFER);
        builder.Property(po => po.IDTURMASGP).HasMaxLength(36);
        builder.Property(po => po.IDVARIAVELTIPO).HasMaxLength(36);
        builder.Property(po => po.VLMEDIO).HasColumnType("DECIMAL(14,2)");
    }
}
