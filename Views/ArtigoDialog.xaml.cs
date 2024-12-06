using System;
using System.Windows;
using GestaoEmpresa.Data;
using GestaoEmpresa.Models;
using Microsoft.EntityFrameworkCore;

namespace GestaoEmpresa.Views
{
    public partial class ArtigoDialog : Window
    {
        private readonly Artigo _artigo; // readonly adicionado
        private readonly EmpresaContext _context;

        public ArtigoDialog(Artigo? artigo = null)
        {
            InitializeComponent();
            _context = new EmpresaContext(new DbContextOptionsBuilder<EmpresaContext>()
                .UseSqlServer("Server=.;Database=GestaoEmpresa;Trusted_Connection=True;").Options);

            _artigo = artigo ?? new Artigo();

            if (artigo != null)
            {
                // Preencher os campos com os dados do artigo selecionado
                ReferenciaBox.Text = artigo.Referencia;
                DescricaoBox.Text = artigo.Descricao;
                ValorSemIVABox.Text = artigo.ValorSemIVA.ToString();
                DescontoBox.Text = artigo.Desconto.ToString();
                IVABox.Text = artigo.IVA.ToString();
            }
        }

        private void BtnSalvar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Validações
                if (string.IsNullOrWhiteSpace(ReferenciaBox.Text)) throw new Exception("Referência é obrigatória.");
                if (string.IsNullOrWhiteSpace(DescricaoBox.Text)) throw new Exception("Descrição é obrigatória.");
                if (!decimal.TryParse(ValorSemIVABox.Text, out var valorSemIVA)) throw new Exception("Valor Sem IVA inválido.");
                if (!decimal.TryParse(DescontoBox.Text, out var desconto)) throw new Exception("Desconto inválido.");
                if (!decimal.TryParse(IVABox.Text, out var iva)) throw new Exception("IVA inválido.");

                // Atualizar valores
                _artigo.Referencia = ReferenciaBox.Text;
                _artigo.Descricao = DescricaoBox.Text;
                _artigo.ValorSemIVA = valorSemIVA;
                _artigo.Desconto = desconto;
                _artigo.IVA = iva;

                if (_artigo.Id == 0)
                {
                    _context.Artigos.Add(_artigo);
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
