using PdfSharp.Pdf;
using PdfSharp.Drawing;
using GestaoEmpresa.Models;

namespace GestaoEmpresa.Utils
{
    public static class PdfExporter
    {
        public static void ExportFatura(Fatura fatura, List<dynamic> produtos, string fornecedorNome, string caminhoArquivo)
        {
            PdfDocument pdf = new();
            PdfPage page = pdf.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(page);
            XFont fontTitle = new("Verdana", 14, XFontStyle.Regular);
            XFont fontNormal = new("Verdana", 10, XFontStyle.Regular);

            // Cabeçalho
            gfx.DrawString($"Fatura #{fatura.Id}", fontTitle, XBrushes.Black, new XRect(0, XUnit.FromPoint(20).Point, page.Width, 30), XStringFormats.TopCenter);
            gfx.DrawString($"Fornecedor: {fornecedorNome}", fontNormal, XBrushes.Black, new XPoint(50, XUnit.FromPoint(80).Point));
            gfx.DrawString($"Data da Fatura: {fatura.DataFatura:dd/MM/yyyy}", fontNormal, XBrushes.Black, new XPoint(50, XUnit.FromPoint(100).Point));
            gfx.DrawString($"Data de Vencimento: {fatura.DataVencimento:dd/MM/yyyy}", fontNormal, XBrushes.Black, new XPoint(50, XUnit.FromPoint(120).Point));
            gfx.DrawString($"Valor Total: {fatura.ValorTotal:C}", fontNormal, XBrushes.Black, new XPoint(50, XUnit.FromPoint(140).Point));

            // Tabela de Produtos
            int yOffset = 180;
            gfx.DrawString("Produtos:", fontTitle, XBrushes.Black, new XPoint(50, XUnit.FromPoint(yOffset).Point));
            yOffset += 30;

            gfx.DrawString("Referência", fontNormal, XBrushes.Black, new XPoint(50, XUnit.FromPoint(yOffset).Point));
            gfx.DrawString("Descrição", fontNormal, XBrushes.Black, new XPoint(150, XUnit.FromPoint(yOffset).Point));
            gfx.DrawString("Quantidade", fontNormal, XBrushes.Black, new XPoint(350, XUnit.FromPoint(yOffset).Point));
            gfx.DrawString("Preço", fontNormal, XBrushes.Black, new XPoint(450, XUnit.FromPoint(yOffset).Point));
            yOffset += 20;

            foreach (var produto in produtos)
            {
                gfx.DrawString(produto.ReferenciaArtigo, fontNormal, XBrushes.Black, new XPoint(50, XUnit.FromPoint(yOffset).Point));
                gfx.DrawString(produto.Descricao, fontNormal, XBrushes.Black, new XPoint(150, XUnit.FromPoint(yOffset).Point));
                gfx.DrawString(produto.Quantidade.ToString(), fontNormal, XBrushes.Black, new XPoint(350, XUnit.FromPoint(yOffset).Point));
                gfx.DrawString($"{produto.ValorComIVA:C}", fontNormal, XBrushes.Black, new XPoint(450, XUnit.FromPoint(yOffset).Point));
                yOffset += 20;
            }

            pdf.Save(caminhoArquivo);
        }
    }
}
