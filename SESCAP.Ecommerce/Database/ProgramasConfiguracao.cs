using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SESCAP.Ecommerce;

public class ProgramasConfiguracao : IEntityTypeConfiguration<PROGRAMAS>
{
    public void Configure(EntityTypeBuilder<PROGRAMAS> builder)
    {

        builder.ToTable("PROGRAMAS");

        /*
        * -> chave primária 
        */
        builder.HasKey(p =>p.CDPROGRAMA);

        builder.Property(p => p.CDPROGRAMA).IsRequired();
        builder.Property(p => p.NMPROGRAMA).HasMaxLength(50).IsRequired();
        builder.Property(p => p.TECONTEUDO).HasColumnType("CLOB").HasMaxLength(4000).HasAnnotation("Db2:ColumnOptions", "LOGGED NOT COMPACT");
        builder.Property(p => p.VBINSCRI).IsRequired();
        builder.Property(p => p.DSDURACAO).HasMaxLength(200);
        builder.Property(p => p.DSPERIODO).HasMaxLength(200);
        builder.Property(p => p.CDPROGSUP);
        builder.Property(p => p.DTATU).IsRequired();
        builder.Property(p => p.HRATU).IsRequired();
        builder.Property(p => p.LGATU).HasMaxLength(10).IsRequired();
        builder.Property(p => p.STATUS).IsRequired();
        builder.Property(p => p.CDUOP).HasDefaultValue(84).IsRequired();


        builder.HasOne(p => p.UOP)
        .WithMany(u => u.PROGRAMAS)
        .HasForeignKey(p => p.CDUOP)
        .HasConstraintName("FK_UOP_21");

    }
}
