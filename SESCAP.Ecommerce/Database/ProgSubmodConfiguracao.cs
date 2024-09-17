using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SESCAP.Ecommerce;

public class ProgSubmodConfiguracao : IEntityTypeConfiguration<PROGSUBMOD>
{
    public void Configure(EntityTypeBuilder<PROGSUBMOD> builder)
    {
       builder.ToTable("PROGSUBMOD");

       builder.HasKey(psub => new {psub.CDPROGRAMA, psub.ANOPROG});

       builder.Property(psub => psub.CDPROGRAMA).IsRequired();
       builder.Property(psub => psub.ANOPROG).IsRequired();
       builder.Property(psub => psub.CDADMIN);
       builder.Property(psub => psub.CDMAPA);
       builder.Property(psub => psub.CDREALIZAC);
       builder.Property(psub => psub.CDMODALIDA);
       builder.Property(psub => psub.CDSUBMODAL);
       builder.Property(psub => psub.DTATU).IsRequired();
       builder.Property(psub => psub.HRATU).IsRequired();
       builder.Property(psub => psub.LGATU).HasMaxLength(10).IsRequired();
       builder.Property(psub => psub.CDDEBFOL);
       builder.Property(psub => psub.GRCONTA_AR);
       builder.Property(psub => psub.CDCONTA_AR).HasMaxLength(11);
       builder.Property(psub => psub.MAREFINI_AR);


       builder.HasOne(psub => psub.PROGRAMAS)
       .WithMany(p => p.PROGSUBMODS)
       .HasForeignKey(psub => psub.CDPROGRAMA)
       .HasConstraintName("FK_PROGRAMAS_9");

       builder.HasOne(psub => psub.SGP_SUBMODALID)
       .WithMany(sgp => sgp.PROGSUBMODS)
       .HasForeignKey(psub => new {psub.CDADMIN, psub.CDMAPA, psub.CDREALIZAC, psub.CDMODALIDA, psub.CDSUBMODAL})
       .HasConstraintName("FK_SGP_SUBM");

    }
}
