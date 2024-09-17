using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SESCAP.Ecommerce;

public class SgpSubmodalidadeConfiguracao : IEntityTypeConfiguration<SGP_SUBMODALID>
{
    public void Configure(EntityTypeBuilder<SGP_SUBMODALID> builder)
    {
        builder.ToTable("SGP_SUBMODALID");

        builder.HasKey(sub => new{sub.CDADMIN, sub.CDMAPA, sub.CDREALIZAC, sub.CDMODALIDA, sub.CDSUBMODAL});

        builder.Property(sub => sub.CDADMIN).IsRequired();
        builder.Property(sub => sub.CDMAPA).IsRequired();
        builder.Property(sub => sub.CDREALIZAC).IsRequired();
        builder.Property(sub => sub.CDMODALIDA).IsRequired();
        builder.Property(sub => sub.CDSUBMODAL).IsRequired();
        builder.Property(sub => sub.DSSUBMODAL).HasMaxLength(200).IsRequired();
        builder.Property(sub => sub.DTVALIDADE);
        builder.Property(sub => sub.DTINICIO).HasDefaultValue("2000-01-01").IsRequired();
        builder.Property(sub => sub.IDSGP).HasMaxLength(36);
    }
}
