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
            gfx.DrawString($"Fatura #{fatura.Id}", fontTitle, XBrushes.Black, new XRect(0, 20, page.Width, 30), XStringFormats.TopCenter);
            gfx.DrawString($"Fornecedor: {fornecedorNome}", fontNormal, XBrushes.Black, new XPoint(50, 80));
            gfx.DrawString($"Data da Fatura: {fatura.DataFatura:dd/MM/yyyy}", fontNormal, XBrushes.Black, new XPoint(50, 100));
            gfx.DrawString($"Data de Vencimento: {fatura.DataVencimento:dd/MM/yyyy}", fontNormal, XBrushes.Black, new XPoint(50, 120));
            gfx.DrawString($"Valor Total: {fatura.ValorTotal:C}", fontNormal, XBrushes.Black, new XPoint(50, 140));

            // Tabela de Produtos
            int yOffset = 180;
            gfx.DrawString("Produtos:", fontTitle, XBrushes.Black, new XPoint(50, yOffset));
            yOffset += 30;

            gfx.DrawString("Referência", fontNormal, XBrushes.Black, new XPoint(50, yOffset));
            gfx.DrawString("Descrição", fontNormal, XBrushes.Black, new XPoint(150, yOffset));
            gfx.DrawString("Quantidade", fontNormal, XBrushes.Black, new XPoint(350, yOffset));
            gfx.DrawString("Preço", fontNormal, XBrushes.Black, new XPoint(450, yOffset));
            yOffset += 20;

            foreach (var produto in produtos)
            {
                gfx.DrawString(produto.ReferenciaArtigo, fontNormal, XBrushes.Black, new XPoint(50, yOffset));
                gfx.DrawString(produto.Descricao, fontNormal, XBrushes.Black, new XPoint(150, yOffset));
                gfx.DrawString(produto.Quantidade.ToString(), fontNormal, XBrushes.Black, new XPoint(350, yOffset));
                gfx.DrawString($"{produto.ValorComIVA:C}", fontNormal, XBrushes.Black, new XPoint(450, yOffset));
                yOffset += 20;
            }

            pdf.Save(caminhoArquivo);
        }
    }
}