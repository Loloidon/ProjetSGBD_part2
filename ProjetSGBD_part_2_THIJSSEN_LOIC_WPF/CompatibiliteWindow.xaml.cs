using ProjetSGBD_part1_THIJSSEN_LOIC.DataAccess;
using ProjetSGBD_part1_THIJSSEN_LOIC.Metier;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace ProjetSGBD_part_2_THIJSSEN_LOIC_WPF
{
    public partial class CompatibiliteWindow : Window
    {
        private readonly CompatibiliteRepository _compatibiliteRepository;

        public CompatibiliteWindow()
        {
            InitializeComponent();

            string connectionString =
                "Host=localhost;Port=5432;Database=projet_sgbd;Username=postgres;Password=Postgres2026";

            _compatibiliteRepository = new CompatibiliteRepository(connectionString);
        }

        private void btnAjouter_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(txtIdAnimal.Text, out int idAnimal))
            {
                MessageBox.Show("ID animal invalide.");
                return;
            }

            string typeCompatibilite =
                ((ComboBoxItem)cmbType.SelectedItem).Content.ToString() ?? "";

            string valeur =
                ((ComboBoxItem)cmbValeur.SelectedItem).Content.ToString() ?? "";

            Compatibilite compatibilite = new Compatibilite
            {
                IdAnimal = idAnimal,
                TypeCompatibilite = typeCompatibilite,
                Valeur = valeur,
                Description = txtDescription.Text
            };

            _compatibiliteRepository.Ajouter(compatibilite);

            MessageBox.Show("Compatibilité ajoutée.");
        }

        private void btnSupprimer_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(txtIdAnimal.Text, out int idAnimal))
            {
                MessageBox.Show("ID animal invalide.");
                return;
            }

            string typeCompatibilite =
                ((ComboBoxItem)cmbType.SelectedItem).Content.ToString() ?? "";

            _compatibiliteRepository.Supprimer(idAnimal, typeCompatibilite);

            MessageBox.Show("Suppression effectuée.");
        }
    }
}