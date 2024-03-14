using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace SESCAP.Ecommerce;

public class ComprovanteRecarga :IDocument
{
   
    public string Cnpj {get; set;}
    public string NumeroFechamento {get; set;}
    public short SqDepRet {get; set;}
    public DateTime DataDepRet {get; set;}
    public TimeSpan HrDepRet {get; set;}
    public string MatFormat {get; set;}
    public string NomeClientela {get; set;}
    public string DsProduto {get; set;}
    public string FormaPgto {get; set;}
    public decimal Valor {get; set;}
    public decimal ValorSaldo {get; set;}
    public string Logo {get; set;}

    public ComprovanteRecarga(string cnpj, string numeroFechamento, short sqDepRet, DateTime dataDepRet, TimeSpan hrDepRet,
    string matFormat, string nomeClientela, string dsProduto, string formaPgto, decimal valor, decimal valorSaldo, string logo)
    {
        Cnpj = cnpj;
        NumeroFechamento = numeroFechamento;
        SqDepRet = sqDepRet;
        DataDepRet = dataDepRet;
        HrDepRet = hrDepRet;
        MatFormat = matFormat;
        NomeClientela = nomeClientela;
        DsProduto = dsProduto;
        FormaPgto = formaPgto;
        Valor = valor;
        ValorSaldo = valorSaldo;
        Logo = logo;
    }
    
    public DocumentMetadata GetMetadata() => DocumentMetadata.Default;
    public DocumentSettings GetSettings() => DocumentSettings.Default;


    public void Compose(IDocumentContainer container)
    {
        container.Page(page =>
        {
            page.Margin(50);
        
            page.Header().Element(ComposeHeader);
            page.Content().Element(ComposeContent);
          
        });

    }

    void ComposeHeader(IContainer container)
    {
        var titleStyle = TextStyle.Default.FontSize(20).ExtraBold().ExtraBlack();

        container.Row(row =>{

            row.RelativeItem().Column(column =>
            {
                column.Item().Text("SESC - AP").Style(titleStyle);

                column.Item().Text(text =>
                {
                    text.Span("CNPJ: ").SemiBold().FontSize(10);
                    text.Span($"{Cnpj}").FontSize(10);
                });

                column.Item().Text(text =>
                {
                    text.Span("CAIXA: ").SemiBold().FontSize(10);
                    text.Span($"Nº FECH: {NumeroFechamento} DEPÓSITO Nº {SqDepRet} - {DataDepRet.ToShortDateString()} - {HrDepRet}").FontSize(10);
                });

                column.Item().Text(text =>
                {
                    text.Span("CLIENTE: ").SemiBold().FontSize(10);
                    text.Span($"{MatFormat} - {NomeClientela}").FontSize(10);
                });

                column.Item().PaddingTop(30).PaddingLeft(100).AlignCenter().Text("COMPROVANTE  DE  RECARGA").FontSize(14).ExtraBlack().ExtraBold();
            });

           
            row.ConstantItem(100).Height(50).Image($"{Logo}");

        });


    }

    void ComposeContent(IContainer container)
    {
        container.PaddingVertical(40).Column(column =>{

            column.Spacing(5);
            column.Item().Element(ComposeTable);
            column.Item().PaddingTop(15).Element(ComposeComments);
            
        });
    }

    void ComposeTable(IContainer container)
    {
        container.Table(table =>
        {

            table.ColumnsDefinition(columns =>
            {
                columns.RelativeColumn(9);
                columns.RelativeColumn();
                
            });

            table.Header(header =>
            {
                header.Cell().Element(CellStyle).Text("DESCRIÇÃO");
                header.Cell().Element(CellStyle).Text("VALOR");

                static IContainer CellStyle(IContainer container)
                {
                    return container.DefaultTextStyle(x => x.SemiBold()).PaddingVertical(5).BorderBottom(1).BorderColor(Colors.Black);
                }
            });

            table.Cell().Element(CellStyle).Text($"{DsProduto}\n{FormaPgto}").FontSize(10);
            table.Cell().Element(CellStyle).Text($"R$ {Valor}").FontSize(10);
            table.Cell().Element(CellStyle).Text("VALOR PAGO PELO CLIENTE").FontSize(10).ExtraBlack().ExtraBold();
            table.Cell().Element(CellStyle).Text($"R$ {Valor}").FontSize(10).ExtraBlack().ExtraBold();
            table.Cell().Element(CellStyle).Text("VALOR DEPOSITADO NO CARTAO").FontSize(10);
            table.Cell().Element(CellStyle).Text($"R$ {Valor}").FontSize(10);
            table.Cell().Element(CellStyle).Text("SALDO ATUAL").FontSize(10);
            table.Cell().Element(CellStyle).Text($"R$ {ValorSaldo}").FontSize(10);


            static IContainer CellStyle(IContainer container)
            {
                return container.BorderBottom(1).BorderColor(Colors.Grey.Lighten2).PaddingVertical(5);
            }
        });
    }

    void ComposeComments(IContainer container)
    {
        container.Background(Colors.Grey.Lighten3).Padding(10).Column(column =>
        {
            column.Spacing(5);
            column.Item().Text("O SESC GOZA DE IMUNIDADE FISCAL E TRIBUTARIA NOS TERMOS DO ART, 150 VI, C. S 40 CRFB/88").FontSize(10);
        });
    }

    public static void GerarPDF(string cnpj, string numeroFechamento, short sqDepRet, DateTime dataDepRet, TimeSpan hrDepRet, string matFormat, string nomeClientela, string dsProduto, string formaPgto, decimal valor, decimal valorSaldo, string caminhoArquivo, string logo)
    {
        QuestPDF.Settings.License = LicenseType.Community;
        var documento = new ComprovanteRecarga(cnpj, numeroFechamento, sqDepRet, dataDepRet, hrDepRet, matFormat, nomeClientela, dsProduto, formaPgto, valor, valorSaldo, logo);
        Document.Create(documento.Compose).GeneratePdf(caminhoArquivo);
    }

}
