using System.Linq;
using System.Windows;
using System.Windows.Controls;
using GestaoEmpresa.Data;
using GestaoEmpresa.Models;
using GestaoEmpresa.Utils;
using Microsoft.EntityFrameworkCore;

namespace GestaoEmpresa.Views
{
    public partial class FaturasPage : Page
    {
        private readonly EmpresaContext _context;

        public FaturasPage()
        {
            InitializeComponent();
            _context = new EmpresaContext(new DbContextOptionsBuilder<EmpresaContext>()
                .UseSqlServer("Server=.;Database=GestaoEmpresa;Trusted_Connection=True;").Options);

            LoadFornecedores();
        }

        private void LoadFornecedores()
        {
            var fornecedores = _context.Fornecedores.ToList();
            FornecedoresListBox.ItemsSource = fornecedores;
            FornecedoresListBox.DisplayMemberPath = "Nome";
        }

        private void FornecedoresListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FornecedoresListBox.SelectedItem is Fornecedor fornecedorSelecionado)
            {
                var faturas = _context.Faturas
                    .Where(f => f.FornecedorId == fornecedorSelecionado.Id)
                    .ToList();

                FaturasDataGrid.ItemsSource = faturas;
            }
        }

        private void BtnExportar_Click(object sender, RoutedEventArgs e)
        {
            if (FaturasDataGrid.SelectedItem is Fatura faturaSelecionada)
            {
                var produtos = _context.ProdutosFatura
                    .Where(p => p.FaturaId == faturaSelecionada.Id)
                    .Join(_context.Artigos,
                          produto => produto.ReferenciaArtigo,
                          artigo => artigo.Referencia,
                          (produto, artigo) => new ProdutoExportado
                          {
                              ReferenciaArtigo = produto.ReferenciaArtigo,
                              Descricao = artigo.Descricao,
                              Quantidade = produto.Quantidade,
                              ValorComIVA = produto.ValorComIVA
                          })
                    .ToList();

                var fornecedor = _context.Fornecedores.Find(faturaSelecionada.FornecedorId);
                if (fornecedor == null)
                {
                    MessageBox.Show("Fornecedor não encontrado.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                string caminhoArquivo = System.IO.Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                    $"Fatura_{faturaSelecionada.Id}.pdf");

                PdfExporter.ExportFatura(faturaSelecionada, produtos, fornecedor.Nome, caminhoArquivo);

                MessageBox.Show($"Fatura exportada com sucesso para:\n{caminhoArquivo}", "Exportação Concluída", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Selecione uma fatura para exportar.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }

    public class ProdutoExportado
    {
        public string ReferenciaArtigo { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public int Quantidade { get; set; }
        public decimal ValorComIVA { get; set; }
    }
}