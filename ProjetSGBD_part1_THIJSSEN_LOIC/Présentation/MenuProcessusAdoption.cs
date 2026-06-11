using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ProjetSGBD_part1_THIJSSEN_LOIC.DataAccess;
using ProjetSGBD_part1_THIJSSEN_LOIC.Metier;

namespace ProjetSGBD_part1_THIJSSEN_LOIC.Présentation
{
    public class MenuProcessusAdoption
    {
        private readonly ProcessusAdoptionRepository _adoptionRepository;
        private readonly AnimalRepository _animalRepository;
        private readonly ContactRepository _contactRepository;

        public MenuProcessusAdoption(
            ProcessusAdoptionRepository adoptionRepository,
            AnimalRepository animalRepository,
            ContactRepository contactRepository)
        {
            _adoptionRepository = adoptionRepository;
            _animalRepository = animalRepository;
            _contactRepository = contactRepository;
        }

        public void AjouterAdoption()
        {
            ProcessusAdoption adoption = new ProcessusAdoption();

            Console.WriteLine("=== Animaux ===");
            _animalRepository.AfficherTout();
            adoption.IdAnimal = LireEntier("ID de l'animal : ");

            Console.WriteLine("=== Contacts ===");
            _contactRepository.AfficherTout();
            adoption.IdContact = LireEntier("ID du contact adoptant : ");

            adoption.DateDemande = LireDate("Date de demande (yyyy-mm-dd) : ");

            adoption.StatutAdoption = LireChoix(
                "Statut (demande/acceptee/rejet_environnement/rejet_comportement) : ",
                new[] { "demande", "acceptee", "rejet_environnement", "rejet_comportement" }
            );

            _adoptionRepository.Ajouter(adoption);

            Console.WriteLine("Adoption ajoutée !");
        }

        public void ListerAdoptions()
        {
            _adoptionRepository.Lister();
        }

        public void ModifierStatut()
        {
            _adoptionRepository.Lister();

            int idProcessus = LireEntier("ID de l'adoption à modifier : ");

            string nouveauStatut = LireChoix(
                "Nouveau statut (demande/acceptee/rejet_environnement/rejet_comportement) : ",
                new[] { "demande", "acceptee", "rejet_environnement", "rejet_comportement" }
            );

            _adoptionRepository.ModifierStatut(idProcessus, nouveauStatut);
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

        private string LireChoix(string message, string[] choixValides)
        {
            while (true)
            {
                Console.Write(message);
                string valeur = Console.ReadLine()?.Trim() ?? "";

                string? choixTrouve = choixValides
                    .FirstOrDefault(c => c.Equals(valeur, StringComparison.OrdinalIgnoreCase));

                if (choixTrouve != null)
                    return choixTrouve;

                Console.WriteLine("Choix invalide.");
            }
        }
    }
}