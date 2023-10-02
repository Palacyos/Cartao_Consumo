using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SESCAP.Ecommerce.Models;

namespace SESCAP.Ecommerce.Database
{
    public class CartaoConfiguracao : IEntityTypeConfiguration<CARTAO>
    {

        public void Configure(EntityTypeBuilder<CARTAO> builder)
        {
            builder.ToTable("CARTAO");

            /*
             * -> chave primária 
             */
            builder.HasKey(cart => cart.NUMCARTAO);

            builder.Property(cart => cart.NUMCARTAO).HasColumnType("INTEGER").IsRequired();
            builder.Property(cart => cart.TPCONTRL).IsRequired();
            builder.Property(cart => cart.PSWCART).HasMaxLength(40).IsRequired();
            builder.Property(cart => cart.DTATU).IsRequired();
            builder.Property(cart => cart.HRATU).IsRequired();
            builder.Property(cart => cart.LGATU).HasMaxLength(10).IsRequired();
            builder.Property(cart => cart.DTCADASTRO).HasDefaultValueSql("CURRENT_DATE").IsRequired();
            builder.Property(cart => cart.CDBARRA).HasMaxLength(20);
            builder.Property(cart => cart.NUCHIP).HasMaxLength(20);
            builder.Property(cart => cart.TPCARTAO).HasDefaultValue(9).IsRequired();
            builder.Property(cart => cart.STCARTAO).HasDefaultValue(1).IsRequired();


        }
    }
}
