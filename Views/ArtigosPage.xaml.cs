using System.Linq;
using System.Windows;
using System.Windows.Controls;
using GestaoEmpresa.Data;
using GestaoEmpresa.Models;

namespace GestaoEmpresa.Views
{
    public partial class ArtigosPage : Page
    {
        private readonly EmpresaContext _context;

        public ArtigosPage()
        {
            InitializeComponent();
            _context = new EmpresaContext();
            LoadArtigos();
        }

        private void LoadArtigos()
        {
            var artigos = _context.Artigos
                .Join(_context.Fornecedores,
                      artigo => artigo.FornecedorId,
                      fornecedor => fornecedor.Id,
                      (artigo, fornecedor) => new
                      {
                          artigo.Referencia,
                          artigo.Descricao,
                          artigo.ValorSemIVA,
                          artigo.Desconto,
                          artigo.IVA,
                          artigo.ValorComIVA,
                          NomeFornecedor = fornecedor.Nome
                      })
                .ToList();

            ArtigosDataGrid.ItemsSource = artigos;
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = SearchBox.Text.ToLower();

            var filteredArtigos = _context.Artigos
                .Join(_context.Fornecedores,
                      artigo => artigo.FornecedorId,
                      fornecedor => fornecedor.Id,
                      (artigo, fornecedor) => new
                      {
                          artigo.Referencia,
                          artigo.Descricao,
                          artigo.ValorSemIVA,
                          artigo.Desconto,
                          artigo.IVA,
                          artigo.ValorComIVA,
                          NomeFornecedor = fornecedor.Nome
                      })
                .Where(a => a.Referencia.ToLower().Contains(searchText) ||
                            a.Descricao.ToLower().Contains(searchText) ||
                            a.NomeFornecedor.ToLower().Contains(searchText))
                .ToList();

            ArtigosDataGrid.ItemsSource = filteredArtigos;
        }

        private void BtnAdicionar_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new ArtigoDialog();
            if (dialog.ShowDialog() == true)
            {
                LoadArtigos();
            }
        }

        private void BtnEditar_Click(object sender, RoutedEventArgs e)
        {
            if (ArtigosDataGrid.SelectedItem is Artigo artigoSelecionado)
            {
                var dialog = new ArtigoDialog(artigoSelecionado);
                if (dialog.ShowDialog() == true)
                {
                    LoadArtigos();
                }
            }
        }

        private void BtnRemover_Click(object sender, RoutedEventArgs e)
        {
            if (ArtigosDataGrid.SelectedItem is Artigo artigoSelecionado)
            {
                if (MessageBox.Show("Tem certeza que deseja remover este artigo?", "Confirmar Remoção", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    _context.Artigos.Remove(artigoSelecionado);
                    _context.SaveChanges();
                    LoadArtigos();
                }
            }
        }
    }
}
