using System.Linq;
using System.Windows;
using System.Windows.Controls;
using GestaoEmpresa.Data;
using GestaoEmpresa.Models;

namespace GestaoEmpresa.Views
{
    public partial class FaturasPage : Page
    {
        private readonly EmpresaContext _context;

        public FaturasPage()
        {
            InitializeComponent();
            _context = new EmpresaContext();
            LoadFornecedores();
        }

        private void LoadFornecedores()
        {
            var fornecedores = _context.Fornecedores.ToList();
            FornecedoresListBox.ItemsSource = fornecedores;
        }

        private void FornecedoresListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FornecedoresListBox.SelectedItem is Fornecedor fornecedorSelecionado)
            {
                var faturas = _context.Faturas
                    .Where(f => f.FornecedorId == fornecedorSelecionado.Id)
                    .ToList();
                FaturasDataGrid.ItemsSource = faturas;

                ProdutosDataGrid.ItemsSource = null; // Limpar produtos ao mudar fornecedor
            }
        }

        private void FaturasDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FaturasDataGrid.SelectedItem is Fatura faturaSelecionada)
            {
                var produtos = _context.ProdutosFatura
                    .Where(p => p.FaturaId == faturaSelecionada.Id)
                    .Select(p => new
                    {
                        p.ReferenciaArtigo,
                        p.Descricao,
                        p.Quantidade,
                        p.ValorComIVA,
                        p.Desconto,
                        p.IVA
                    })
                    .ToList();

                ProdutosDataGrid.ItemsSource = produtos;
            }
        }

        private void BtnExportar_Click(object sender, RoutedEventArgs e)
        {
            if (FaturasDataGrid.SelectedItem is Fatura faturaSelecionada)
            {
                var fornecedor = _context.Fornecedores.FirstOrDefault(f => f.Id == faturaSelecionada.FornecedorId);
                var produtos = _context.ProdutosFatura
                    .Where(p => p.FaturaId == faturaSelecionada.Id)
                    .Select(p => new
                    {
                        p.ReferenciaArtigo,
                        p.Descricao,
                        p.Quantidade,
                        p.ValorComIVA
                    })
                    .ToList();

                if (fornecedor != null)
                {
                    string caminhoArquivo = $"Fatura_{faturaSelecionada.Id}.pdf";
                    PdfExporter.ExportFatura(faturaSelecionada, produtos, fornecedor.Nome, caminhoArquivo);
                    MessageBox.Show($"Fatura exportada para {caminhoArquivo}", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }
    }
}
