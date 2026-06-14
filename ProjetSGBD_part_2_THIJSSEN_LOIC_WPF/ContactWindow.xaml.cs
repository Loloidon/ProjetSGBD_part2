using System.Collections.Generic;
using System.Windows;
using ProjetSGBD_part1_THIJSSEN_LOIC.DataAccess;
using ProjetSGBD_part1_THIJSSEN_LOIC.Metier;

namespace ProjetSGBD_part_2_THIJSSEN_LOIC_WPF
{
    public partial class ContactWindow : Window
    {
        private readonly ContactRepository _contactRepository;

        public ContactWindow()
        {
            InitializeComponent();

            string connectionString = "Host=localhost;Port=5432;Database=projet_sgbd;Username=postgres;Password=Postgres2026";
            _contactRepository = new ContactRepository(connectionString);
        }

        private void btnLister_Click(object sender, RoutedEventArgs e)
        {
            dgContacts.ItemsSource = _contactRepository.GetAll();
        }

        private void btnAfficherUn_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(txtId.Text, out int id))
            {
                MessageBox.Show("ID invalide.");
                return;
            }

            Contact? contact = _contactRepository.GetById(id);

            if (contact == null)
            {
                MessageBox.Show("Contact introuvable.");
                dgContacts.ItemsSource = null;
                return;
            }

            dgContacts.ItemsSource = new List<Contact> { contact };
        }

        private void btnAjouter_Click(object sender, RoutedEventArgs e)
        {
            Contact contact = LireFormulaire();

            _contactRepository.Ajouter(contact);
            dgContacts.ItemsSource = _contactRepository.GetAll();

            MessageBox.Show("Contact ajouté.");
        }

        private void btnModifier_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(txtId.Text, out int id))
            {
                MessageBox.Show("ID invalide.");
                return;
            }

            Contact contact = LireFormulaire();

            _contactRepository.Modifier(id, contact);
            dgContacts.ItemsSource = _contactRepository.GetAll();

            MessageBox.Show("Contact modifié.");
        }

        private void btnSupprimer_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(txtId.Text, out int id))
            {
                MessageBox.Show("ID invalide.");
                return;
            }

            bool suppressionOk = _contactRepository.Supprimer(id);

            if (suppressionOk)
            {
                dgContacts.ItemsSource = _contactRepository.GetAll();
                MessageBox.Show("Contact supprimé.");
            }
            else
            {
                MessageBox.Show("Impossible de supprimer ce contact : il est lié à une adoption ou une famille d'accueil.");
            }
        }

        private Contact LireFormulaire()
        {
            return new Contact
            {
                Nom = txtNom.Text,
                Prenom = txtPrenom.Text,
                Registre = txtRegistre.Text,
                Adresse = txtAdresse.Text,
                Gsm = txtGsm.Text,
                Mail = txtMail.Text
            };
        }

    }
}