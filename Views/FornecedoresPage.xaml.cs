using System.Linq;
using System.Windows;
using System.Windows.Controls;
using GestaoEmpresa.Data;
using GestaoEmpresa.Models;
using Microsoft.EntityFrameworkCore;

namespace GestaoEmpresa.Views
{
    public partial class FornecedoresPage : Page
    {
        private readonly EmpresaContext _context;

        public FornecedoresPage()
        {
            InitializeComponent();
            _context = new EmpresaContext(new DbContextOptionsBuilder<EmpresaContext>()
                .UseSqlServer("Server=.;Database=GestaoEmpresa;Trusted_Connection=True;").Options);

            LoadFornecedores();
        }

        private void LoadFornecedores()
        {
            var fornecedores = _context.Fornecedores.ToList();
            FornecedoresDataGrid.ItemsSource = fornecedores;
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = SearchBox.Text;

            var filteredFornecedores = _context.Fornecedores
                .Where(f => f.Nome.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
                            f.Contribuinte.Contains(searchText, StringComparison.OrdinalIgnoreCase))
                .ToList();

            FornecedoresDataGrid.ItemsSource = filteredFornecedores;
        }

        private void BtnAdicionar_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new FornecedorDialog();
            if (dialog.ShowDialog() == true)
            {
                LoadFornecedores();
            }
        }

        private void BtnEditar_Click(object sender, RoutedEventArgs e)
        {
            if (FornecedoresDataGrid.SelectedItem is Fornecedor fornecedorSelecionado)
            {
                var dialog = new FornecedorDialog(fornecedorSelecionado);
                if (dialog.ShowDialog() == true)
                {
                    LoadFornecedores();
                }
            }
        }

        private void BtnRemover_Click(object sender, RoutedEventArgs e)
        {
            if (FornecedoresDataGrid.SelectedItem is Fornecedor fornecedorSelecionado)
            {
                if (MessageBox.Show("Tem certeza que deseja remover este fornecedor?", "Confirmar Remoção", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    _context.Fornecedores.Remove(fornecedorSelecionado);
                    _context.SaveChanges();
                    LoadFornecedores();
                }
            }
        }
    }
}