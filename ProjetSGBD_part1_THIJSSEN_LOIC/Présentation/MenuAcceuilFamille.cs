using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ProjetSGBD_part1_THIJSSEN_LOIC.DataAccess;
using ProjetSGBD_part1_THIJSSEN_LOIC.Metier;

namespace ProjetSGBD_part1_THIJSSEN_LOIC.Présentation
{
    public class MenuAccueilFamille
    {
        private readonly AccueilFamilleRepository _accueilRepository;
        private readonly AnimalRepository _animalRepository;
        private readonly ContactRepository _contactRepository;

        public MenuAccueilFamille(
            AccueilFamilleRepository accueilRepository,
            AnimalRepository animalRepository,
            ContactRepository contactRepository)
        {
            _accueilRepository = accueilRepository;
            _animalRepository = animalRepository;
            _contactRepository = contactRepository;
        }

        public void AjouterAccueil()
        {
            AccueilFamille accueil = new AccueilFamille();

            Console.WriteLine("=== Animaux ===");
            _animalRepository.AfficherTout();
            accueil.IdAnimal = LireEntier("ID de l'animal : ");

            Console.WriteLine("=== Contacts ===");
            _contactRepository.AfficherTout();
            accueil.IdContact = LireEntier("ID du contact / famille d'accueil : ");

            accueil.DateDebut = LireDate("Date d'arrivée dans la famille (yyyy-mm-dd) : ");

            if (LireOuiNon("Y a-t-il une date de fin ? oui/non : "))
            {
                accueil.DateFin = LireDate("Date fin (yyyy-mm-dd) : ");
            }

            _accueilRepository.Ajouter(accueil);
            Console.WriteLine("Accueil famille ajouté !");
        }

        public void ListerFamillesParAnimal()
        {
            Console.WriteLine("=== Animaux ===");
            _animalRepository.AfficherTout();

            int idAnimal = LireEntier("ID de l'animal : ");
            _accueilRepository.ListerFamilleAnimalPasse(idAnimal);
        }

        public void ListerAnimauxParFamille()
        {
            Console.WriteLine("=== Contacts ===");
            _contactRepository.AfficherTout();

            int idContact = LireEntier("ID du contact / famille d'accueil : ");
            _accueilRepository.ListerAnimauxAcceuillisFamille(idContact);
        }

        private int LireEntier(string message)
        {
            int valeur;
            while (true)
            {
                Console.Write(message);
                if (int.TryParse(Console.ReadLine(), out valeur))
                    return valeur;

                Console.WriteLine("Veuillez entrer un nombre valide.");
            }
        }

        private DateTime LireDate(string message)
        {
            DateTime date;
            while (true)
            {
                Console.Write(message);
                if (DateTime.TryParse(Console.ReadLine(), out date))
                    return date;

                Console.WriteLine("Date invalide. Exemple : 2026-05-17");
            }
        }

        private bool LireOuiNon(string message)
        {
            while (true)
            {
                Console.Write(message);
                string reponse = Console.ReadLine()?.Trim().ToLower() ?? "";

                if (reponse == "oui")
                    return true;

                if (reponse == "non")
                    return false;

                Console.WriteLine("Réponse invalide.");
            }
        }
    }
}