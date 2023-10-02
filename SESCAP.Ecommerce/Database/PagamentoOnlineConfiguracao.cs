using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SESCAP.Ecommerce.Models;

namespace SESCAP.Ecommerce.Database
{
    
    public class PagamentoOnlineConfiguracao : IEntityTypeConfiguration<PagamentoOnline>
    {
        public PagamentoOnlineConfiguracao()
        {
        }

        public void Configure(EntityTypeBuilder<PagamentoOnline> builder)
        {
            builder.ToTable("PAGAMENTO_ONLINE");

            builder.HasKey(po => po.Id);

            builder.Property(po => po.OrderId).HasMaxLength(60);
            builder.Property(po => po.FormaPgto).HasMaxLength(20);
            builder.Property(po => po.Total);
            builder.Property(po => po.Transacao).HasMaxLength(3000);
            builder.Property(po => po.DataPgto);
            builder.Property(po => po.Status).HasMaxLength(50);
            builder.Property(po => po.SQMATRIC);
            builder.Property(po => po.CDUOP);


            /*
             * -> relacionamento 1:N - CadastroLoginClientela->PagamentoOnline
             */
            builder.HasOne(po => po.Clientela)
                .WithMany(c => c.PagamentosOnline)
                .HasForeignKey(po => new { po.SQMATRIC, po.CDUOP });

        }
    }
}
