using System.Windows;
using ProjetSGBD_part1_THIJSSEN_LOIC.DataAccess;

namespace ProjetSGBD_part_2_THIJSSEN_LOIC_WPF
{
    public partial class VaccinWindow : Window
    {
        private readonly VaccinRepository _vaccinRepository;

        public VaccinWindow()
        {
            InitializeComponent();

            string connectionString =
                "Host=localhost;Port=5432;Database=projet_sgbd;Username=postgres;Password=Postgres2026";

            _vaccinRepository = new VaccinRepository(connectionString);
        }

        private void btnAfficherVaccins_Click(object sender, RoutedEventArgs e)
        {
            dgVaccins.ItemsSource = _vaccinRepository.GetAll();
        }

        private void btnVaccinsAnimal_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(txtIdAnimal.Text, out int idAnimal))
            {
                MessageBox.Show("ID animal invalide.");
                return;
            }

            dgVaccins.ItemsSource = _vaccinRepository.GetVaccinsAnimal(idAnimal);
        }

        private void btnAjouterVaccination_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(txtIdAnimal.Text, out int idAnimal))
            {
                MessageBox.Show("ID animal invalide.");
                return;
            }

            if (!int.TryParse(txtIdVaccin.Text, out int idVaccin))
            {
                MessageBox.Show("ID vaccin invalide.");
                return;
            }

            if (!DateTime.TryParse(txtDateVaccination.Text, out DateTime dateVaccination))
            {
                MessageBox.Show("Date vaccination invalide.");
                return;
            }

            _vaccinRepository.AjouterVaccination(idAnimal, idVaccin, dateVaccination);

            dgVaccins.ItemsSource = _vaccinRepository.GetVaccinsAnimal(idAnimal);

            MessageBox.Show("Vaccination ajoutée.");
        }
    }
}