using System;
using System.Collections.Generic;
using System.Windows;
using ProjetSGBD_part1_THIJSSEN_LOIC.DataAccess;
using ProjetSGBD_part1_THIJSSEN_LOIC.Metier;

namespace ProjetSGBD_part_2_THIJSSEN_LOIC_WPF
{
    public partial class AnimalWindow : Window
    {
        private readonly AnimalRepository _animalRepository;

        public AnimalWindow()
        {
            InitializeComponent();

            string connectionString = "Host=localhost;Port=5432;Database=projet_sgbd;Username=postgres;Password=Postgres2026";
            _animalRepository = new AnimalRepository(connectionString);
        }

        private void btnLister_Click(object sender, RoutedEventArgs e)
        {
            dgAnimaux.ItemsSource = _animalRepository.GetAll();
        }

        private void btnAfficherUn_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(txtId.Text, out int id))
            {
                MessageBox.Show("ID invalide.");
                return;
            }

            Animal? animal = _animalRepository.GetById(id);

            if (animal == null)
            {
                MessageBox.Show("Animal introuvable.");
                dgAnimaux.ItemsSource = null;
                return;
            }

            dgAnimaux.ItemsSource = new List<Animal> { animal };
        }

        private void btnListerRefuge_Click(object sender, RoutedEventArgs e)
        {
            dgAnimaux.ItemsSource = _animalRepository.GetAnimauxRefuge();
        }

        private void btnAjouter_Click(object sender, RoutedEventArgs e)
        {
            if (!DateTime.TryParse(txtDateNaissance.Text, out DateTime dateNaissance))
            {
                MessageBox.Show("Date de naissance invalide.");
                return;
            }

            DateTime? dateSterilisation = null;
            if (!string.IsNullOrWhiteSpace(txtDateSterilisation.Text) &&
                txtDateSterilisation.Text != "date stérilisation")
            {
                if (DateTime.TryParse(txtDateSterilisation.Text, out DateTime ds))
                    dateSterilisation = ds;
                else
                {
                    MessageBox.Show("Date de stérilisation invalide.");
                    return;
                }
            }

            DateTime? dateDeces = null;
            if (!string.IsNullOrWhiteSpace(txtDateDeces.Text) &&
                txtDateDeces.Text != "date décès")
            {
                if (DateTime.TryParse(txtDateDeces.Text, out DateTime dd))
                    dateDeces = dd;
                else
                {
                    MessageBox.Show("Date de décès invalide.");
                    return;
                }
            }

            Animal animal = new Animal
            {
                Nom = txtNom.Text,
                TypeAnimal = txtType.Text,
                Sexe = txtSexe.Text,
                DateNaissance = dateNaissance,
                Sterilise = chkSterilise.IsChecked == true,
                DateSterilisation = dateSterilisation,
                Couleurs = txtCouleurs.Text,
                Description = txtDescription.Text,
                Particularites = txtParticularites.Text,
                EtatCourant = txtEtat.Text,
                DateDeces = dateDeces
            };

            _animalRepository.Ajouter(animal);
            dgAnimaux.ItemsSource = _animalRepository.GetAll();

            MessageBox.Show("Animal ajouté.");
        }

        private void btnSupprimer_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(txtId.Text, out int id))
            {
                MessageBox.Show("ID invalide.");
                return;
            }

            _animalRepository.SupprimerUn(id);
            dgAnimaux.ItemsSource = _animalRepository.GetAll();

            MessageBox.Show("Suppression effectuée.");
        }
    }
}