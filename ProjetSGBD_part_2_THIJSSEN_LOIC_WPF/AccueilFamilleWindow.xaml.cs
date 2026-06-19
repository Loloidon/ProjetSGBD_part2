using System;
using System.Windows;
using ProjetSGBD_part1_THIJSSEN_LOIC.DataAccess;
using ProjetSGBD_part1_THIJSSEN_LOIC.Metier;

namespace ProjetSGBD_part_2_THIJSSEN_LOIC_WPF
{
    public partial class AccueilFamilleWindow : Window
    {
        private readonly AccueilFamilleRepository _accueilRepository;

        public AccueilFamilleWindow()
        {
            InitializeComponent();

            string connectionString =
                "Host=localhost;Port=5432;Database=projet_sgbd;Username=postgres;Password=Postgres2026";

            _accueilRepository = new AccueilFamilleRepository(connectionString);
        }

        private void btnAjouter_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(txtIdAnimal.Text, out int idAnimal))
            {
                MessageBox.Show("ID animal invalide.");
                return;
            }

            if (!int.TryParse(txtIdContact.Text, out int idContact))
            {
                MessageBox.Show("ID contact invalide.");
                return;
            }

            if (!DateTime.TryParse(txtDateDebut.Text, out DateTime dateDebut))
            {
                MessageBox.Show("Date début invalide.");
                return;
            }

            DateTime? dateFin = null;

            if (!string.IsNullOrWhiteSpace(txtDateFin.Text)
                && txtDateFin.Text != "date fin")
            {
                if (!DateTime.TryParse(txtDateFin.Text, out DateTime dateFinTemp))
                {
                    MessageBox.Show("Date fin invalide.");
                    return;
                }

                dateFin = dateFinTemp;
            }

            AccueilFamille accueil = new AccueilFamille
            {
                IdAnimal = idAnimal,
                IdContact = idContact,
                DateDebut = dateDebut,
                DateFin = dateFin
            };

            _accueilRepository.Ajouter(accueil);

            MessageBox.Show("Accueil ajouté.");

            dgAccueils.ItemsSource = _accueilRepository.GetFamillesAnimalPasse(idAnimal);
        }

        private void btnListerFamillesAnimal_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(txtIdAnimal.Text, out int idAnimal))
            {
                MessageBox.Show("ID animal invalide.");
                return;
            }

            dgAccueils.ItemsSource =
                _accueilRepository.GetFamillesAnimalPasse(idAnimal);
        }

        private void btnListerAnimauxFamille_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(txtIdContact.Text, out int idContact))
            {
                MessageBox.Show("ID contact invalide.");
                return;
            }

            dgAccueils.ItemsSource =
                _accueilRepository.GetAnimauxAccueillisFamille(idContact);
        }
    }
}