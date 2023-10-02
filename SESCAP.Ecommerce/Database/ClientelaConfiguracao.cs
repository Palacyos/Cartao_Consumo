using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SESCAP.Ecommerce.Models;


namespace SESCAP.Ecommerce.Database
{
    public class ClientelaConfiguracao : IEntityTypeConfiguration<CLIENTELA>
    {
        public void Configure(EntityTypeBuilder<CLIENTELA> builder)
        {
            builder.ToTable("CLIENTELA");

            /*
             * -> chave primária composta 
             */
            builder.HasKey(c => new { c.SQMATRIC, c.CDUOP });
            

            builder.Property(c => c.SQMATRIC).IsRequired();
            builder.Property(c => c.CDUOP).IsRequired();
            builder.Property(c => c.CDCLASSIF).HasMaxLength(5);
            builder.Property(c => c.NUDV).IsRequired();
            builder.Property(c => c.NUCGCCEI).HasMaxLength(14);
            builder.Property(c => c.CDCATEGORI).IsRequired();
            builder.Property(c => c.CDNIVEL);
            builder.Property(c => c.SQTITULMAT);
            builder.Property(c => c.CDUOTITUL);
            builder.Property(c => c.STMATRIC).IsRequired();
            builder.Property(c => c.DTINSCRI).IsRequired();
            builder.Property(c => c.CDMATRIANT).HasMaxLength(15);
            builder.Property(c => c.DTVENCTO).IsRequired();
            builder.Property(c => c.NMCLIENTE).HasMaxLength(80).IsRequired();
            builder.Property(c => c.DTNASCIMEN).IsRequired();
            builder.Property(c => c.NMPAI).HasMaxLength(80);
            builder.Property(c => c.CDSEXO).HasMaxLength(1).IsRequired();
            builder.Property(c => c.NMMAE).HasMaxLength(80);
            builder.Property(c => c.CDESTCIVIL).IsRequired();
            builder.Property(c => c.VBESTUDANT).IsRequired();
            builder.Property(c => c.NUULTSERIE);
            builder.Property(c => c.DSNATURAL).HasMaxLength(40).IsRequired();
            builder.Property(c => c.DSNACIONAL).HasMaxLength(40).IsRequired();
            builder.Property(c => c.NUDEPEND);
            builder.Property(c => c.NUCTPS).HasMaxLength(12);
            builder.Property(c => c.DTADMISSAO);
            builder.Property(c => c.DTDEMISSAO);
            builder.Property(c => c.NUREGGERAL).HasMaxLength(15);
            builder.Property(c => c.VLRENDA).HasColumnType("decimal(15,4)");
            builder.Property(c => c.NUCPF).HasMaxLength(11);
            builder.Property(c => c.NUPISPASEP).HasMaxLength(11);
            builder.Property(c => c.DSCARGO).HasMaxLength(40);
            builder.Property(c => c.DTEMIRG);
            builder.Property(c => c.IDORGEMIRG).HasMaxLength(15);
            builder.Property(c => c.DSPARENTSC);
            builder.Property(c => c.FOTO).HasMaxLength(32000);
            builder.Property(c => c.DTATU).IsRequired();
            builder.Property(c => c.STEMICART);
            builder.Property(c => c.HRATU).IsRequired();
            builder.Property(c => c.LGATU).HasMaxLength(10).IsRequired();
            builder.Property(c => c.SMFIELDATU).IsRequired();
            builder.Property(c => c.TEOBS).HasMaxLength(3000);
            builder.Property(c => c.NRVIACART).HasDefaultValue(1).IsRequired();
            builder.Property(c => c.PSWCLI).HasMaxLength(6).HasDefaultValue("123456").IsRequired();
            builder.Property(c => c.NUMCARTAO).HasColumnType("INTEGER");
            builder.Property(c => c.PSWCRIP).HasMaxLength(40).HasDefaultValue("012345").IsRequired();
            builder.Property(c => c.VLRENDAFAM).HasColumnType("decimal(15,2)");
            builder.Property(c => c.NMSOCIAL).HasMaxLength(80);
            builder.Property(c => c.SITUPROF).HasDefaultValue(0).IsRequired();
            builder.Property(c => c.TIPOIDENTIDADE).HasDefaultValue(0).IsRequired();
            builder.Property(c => c.COMPIDENTIDADE).HasMaxLength(70);
            builder.Property(c => c.VBPCG).HasDefaultValue(0).IsRequired();
            builder.Property(c => c.VBEMANCIPADO).HasDefaultValue(0).IsRequired();
            builder.Property(c => c.VBPCD).HasDefaultValue(0).IsRequired();
            builder.Property(c => c.IDNACIONAL).HasMaxLength(36);
            builder.Property(c => c.STONLINE).HasDefaultValue(0).IsRequired();
            builder.Property(c => c.VBNOMEAFETIVO).HasDefaultValue(0).IsRequired();

            /*
             * -> relacionamento 1:N UOP->CLIENTELA
             */
            builder.HasOne(c => c.UOP)
                 .WithMany(uop => uop.CLIENTELAS)
                 .HasForeignKey(c => c.CDUOP)
                 .HasConstraintName("FK_UOP_7");

            /*
             * -> relacionamento 1:N CATEGORIA->CLIENTELA 
             */
            builder.HasOne(c => c.CATEGORIA)
                .WithMany(categ => categ.CLIENTELAS)
                .HasForeignKey(c => c.CDCATEGORI)
                .HasConstraintName("FK_CATEGORIA_3");

            /*
             * -> relacionamento 1:1 CLIENTELA->CARTAO
             */
            builder.HasOne(c => c.CARTAO)
                .WithOne(cart => cart.CLIENTELA)
                .HasForeignKey<CLIENTELA>(c => c.NUMCARTAO)
                .HasConstraintName("FK_CARTAO_6");


        }
    }
}
