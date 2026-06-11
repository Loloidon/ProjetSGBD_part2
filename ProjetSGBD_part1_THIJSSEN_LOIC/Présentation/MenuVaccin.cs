using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ProjetSGBD_part1_THIJSSEN_LOIC.DataAccess;

namespace ProjetSGBD_part1_THIJSSEN_LOIC.Présentation
{
    public class MenuVaccin
    {
        private readonly VaccinRepository _vaccinRepository;
        private readonly AnimalRepository _animalRepository;

        public MenuVaccin(VaccinRepository vaccinRepository, AnimalRepository animalRepository)
        {
            _vaccinRepository = vaccinRepository;
            _animalRepository = animalRepository;
        }

        public void AjouterVaccinAnimal()
        {
            Console.WriteLine("=== Liste des animaux ===");
            _animalRepository.AfficherTout();

            int idAnimal = LireEntier("ID de l'animal : ");

            Console.WriteLine("=== Liste des vaccins ===");
            _vaccinRepository.AfficherVaccins();

            int idVaccin = LireEntier("ID du vaccin : ");

            DateTime dateVaccination = LireDate("Date de vaccination (yyyy-mm-dd) : ");

            _vaccinRepository.AjouterVaccination(idAnimal, idVaccin, dateVaccination);
        }

        public void AfficherVaccinsAnimal()
        {
            Console.WriteLine("=== Liste des animaux ===");
            _animalRepository.AfficherTout();

            int idAnimal = LireEntier("ID de l'animal : ");

            _vaccinRepository.AfficherVaccinsAnimal(idAnimal);
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

                Console.WriteLine("Date invalide. Exemple : 2026-05-16");
            }
        }
    }
}
