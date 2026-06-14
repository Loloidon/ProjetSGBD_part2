using System.Windows;

namespace ProjetSGBD_part_2_THIJSSEN_LOIC_WPF
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnAnimaux_Click(object sender, RoutedEventArgs e)
        {
            AnimalWindow window = new AnimalWindow();
            window.Show();
        }
    }
}