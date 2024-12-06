using System.Windows;

namespace GestaoEmpresa
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnArtigos_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Views.ArtigosPage());
        }

        private void BtnFaturas_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Views.FaturasPage());
        }

        private void BtnFornecedores_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Views.FornecedoresPage());
        }
    }
}