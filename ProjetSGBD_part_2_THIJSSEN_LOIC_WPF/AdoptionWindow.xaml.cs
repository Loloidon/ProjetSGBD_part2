using ProjetSGBD_part1_THIJSSEN_LOIC.DataAccess;
using ProjetSGBD_part1_THIJSSEN_LOIC.Metier;
using System.Windows;
using System.Windows.Controls;


namespace ProjetSGBD_part_2_THIJSSEN_LOIC_WPF
{
    public partial class AdoptionWindow : Window
    {
        private readonly ProcessusAdoptionRepository _adoptionRepository;

        public AdoptionWindow()
        {
            InitializeComponent();

            string connectionString = "Host=localhost;Port=5432;Database=projet_sgbd;Username=postgres;Password=Postgres2026";
            _adoptionRepository = new ProcessusAdoptionRepository(connectionString);
        }

        private void btnLister_Click(object sender, RoutedEventArgs e)
        {
            dgAdoptions.ItemsSource = _adoptionRepository.GetAll();
        }

        private void btnAjouter_Click(object sender, RoutedEventArgs e)
        {
            if (!DateTime.TryParse(txtDateDemande.Text, out DateTime dateDemande))
            {
                MessageBox.Show("Date invalide.");
                return;
            }

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

            ProcessusAdoption adoption = new ProcessusAdoption
            {
                DateDemande = dateDemande,
                IdAnimal = idAnimal,
                IdContact = idContact
            };

            _adoptionRepository.Ajouter(adoption);
            dgAdoptions.ItemsSource = _adoptionRepository.GetAll();

            MessageBox.Show("Demande d'adoption ajoutée.");
        }

        private void btnModifierStatut_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(txtIdProcessus.Text, out int idProcessus))
            {
                MessageBox.Show("ID adoption invalide.");
                return;
            }

            string nouveauStatut = ((ComboBoxItem)cmbStatut.SelectedItem).Content.ToString() ?? "";

            _adoptionRepository.ModifierStatut(idProcessus, nouveauStatut);
            dgAdoptions.ItemsSource = _adoptionRepository.GetAll();

            MessageBox.Show("Statut modifié.");
        }
    }
}