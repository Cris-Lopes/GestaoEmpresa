using System;
using System.Windows;
using GestaoEmpresa.Data;
using GestaoEmpresa.Models;
using Microsoft.EntityFrameworkCore;

namespace GestaoEmpresa.Views
{
    public partial class FornecedorDialog : Window
    {
        private readonly Fornecedor _fornecedor;
        private readonly EmpresaContext _context;

        public FornecedorDialog(Fornecedor? fornecedor = null)
        {
            InitializeComponent();
            _context = new EmpresaContext(new DbContextOptionsBuilder<EmpresaContext>()
                .UseSqlServer("Server=.;Database=GestaoEmpresa;Trusted_Connection=True;").Options);

            _fornecedor = fornecedor ?? new Fornecedor();

            if (fornecedor != null)
            {
                NomeBox.Text = fornecedor.Nome;
                ContribuinteBox.Text = fornecedor.Contribuinte;
                CondicoesPagamentoBox.Text = fornecedor.CondicoesPagamento;
            }
        }

        private void BtnSalvar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(NomeBox.Text)) throw new Exception("Nome é obrigatório.");
                if (string.IsNullOrWhiteSpace(ContribuinteBox.Text)) throw new Exception("Contribuinte é obrigatório.");
                if (string.IsNullOrWhiteSpace(CondicoesPagamentoBox.Text)) throw new Exception("Condições de pagamento são obrigatórias.");

                _fornecedor.Nome = NomeBox.Text;
                _fornecedor.Contribuinte = ContribuinteBox.Text;
                _fornecedor.CondicoesPagamento = CondicoesPagamentoBox.Text;

                if (_fornecedor.Id == 0)
                {
                    _context.Fornecedores.Add(_fornecedor);
                }

                _context.SaveChanges();
                DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}