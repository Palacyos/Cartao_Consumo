using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SESCAP.Ecommerce.Models;

namespace SESCAP.Ecommerce.Database
{
    public class UsuarioConfiguracao : IEntityTypeConfiguration<USUARIO>
    {
        public void Configure(EntityTypeBuilder<USUARIO> builder)
        {
            builder.ToTable("USUARIO");

            /*
            * -> chave primária 
            */

            builder.HasKey(u => u.CDPESSOA);

            builder.Property(u => u.CDPESSOA).IsRequired();
            builder.Property(u => u.NMLOGIN).HasMaxLength(30).IsRequired();
            builder.Property(u => u.NMSENHA).HasMaxLength(100).IsRequired();
            builder.Property(u => u.VBATIVO).IsRequired();
            builder.Property(u => u.DTSENHAEXP);
            builder.Property(u => u.VBSENHAEXP).IsRequired();
            builder.Property(u => u.NMCHAVEAUTENT).HasMaxLength(100).IsRequired();
            builder.Property(u => u.IDUSUARIO).HasMaxLength(10);
            builder.Property(u => u.NMNICK).HasMaxLength(30).HasDefaultValue("admin").IsRequired();
            builder.Property(u => u.VBACESSAPORTAL).HasDefaultValue(1).IsRequired();


            /*
             * -> relacionamento 1:1 USUARIO->PESSOA
             */
            builder.HasOne(u => u.PESSOA)
                .WithOne(p => p.USUARIO)
                .HasForeignKey<USUARIO>(u => u.CDPESSOA)
                .HasConstraintName("FK_PESSOA_4");


        }
    }
}
