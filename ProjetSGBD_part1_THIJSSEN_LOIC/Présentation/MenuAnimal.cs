using ProjetSGBD_part1_THIJSSEN_LOIC.DataAccess;
using ProjetSGBD_part1_THIJSSEN_LOIC.Metier;

namespace ProjetSGBD_part1_THIJSSEN_LOIC.Présentation
{
    public class MenuAnimal
    {
        private readonly AnimalRepository _animalRepository;
        private readonly CompatibiliteRepository _compatibiliteRepository;
        public MenuAnimal(AnimalRepository animalRepository)
        {
            _animalRepository = animalRepository;
        }
        

        public void AjouterAnimal()
        {
            Animal animal = new Animal();

            animal.Nom = LireTexteObligatoire("Nom : ");
            animal.TypeAnimal = LireChoix("Type animal (chat/chien) : ", new[] { "chat", "chien" });
            animal.Sexe = LireChoix("Sexe (M/F) : ", new[] { "M", "F" });
            animal.DateNaissance = LireDate("Date naissance (yyyy-mm-dd) : ");
            animal.Sterilise = LireOuiNon("Stérilisé ? oui/non : ");

            if (animal.Sterilise)
            {
                animal.DateSterilisation = LireDate("Date stérilisation (yyyy-mm-dd) : ");
            }

            animal.Couleurs = LireTexteOptionnel("Couleurs : ");
            animal.Description = LireTexteOptionnel("Description : ");
            animal.Particularites = LireTexteOptionnel("Particularités : ");

            animal.EtatCourant = LireChoix(
                "État courant (refuge/proprietaire/accueilli/adopte/decede) : ",
                new[] { "refuge", "proprietaire", "accueilli", "adopte", "decede" }
            );

            if (animal.EtatCourant == "decede")
            {
                animal.DateDeces = LireDate("Date décès (yyyy-mm-dd) : ");
            }

            _animalRepository.Ajouter(animal);

            Console.WriteLine("Animal ajouté !");
        }

        public void AfficherTout()
        {
            _animalRepository.AfficherTout();
        }
        public void AfficherUn()
        {
            int id = 0;
            Console.WriteLine("Entrez l'ID de l'animal que vous souhaitez afficher");
            while(!int.TryParse(Console.ReadLine(),out id))
            {
                Console.WriteLine("Veuillez entrer un nombre valide");
            }
            _animalRepository.AfficherUn(id);
        }
        public void SupprimerUn()
        {
            int id = 0;
            Console.WriteLine("Entrez l'ID de l'animal que vous souhaitez supprimer");
            while (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.WriteLine("Veuillez entrer un nombre valide");
            }
            _animalRepository.SupprimerUn(id);
        }
        public void ListerAnimauxRefuge()
        {
            _animalRepository.ListerAnimauxRefuge();
        }
        private string LireTexteObligatoire(string message)
        {
            string valeur;

            do
            {
                Console.Write(message);
                valeur = Console.ReadLine()?.Trim() ?? "";

                if (string.IsNullOrWhiteSpace(valeur))
                    Console.WriteLine("Ce champ est obligatoire.");
            }
            while (string.IsNullOrWhiteSpace(valeur));

            return valeur;
        }

        private string LireTexteOptionnel(string message)
        {
            Console.Write(message);
            return Console.ReadLine()?.Trim() ?? "";
        }

        private string LireChoix(string message, string[] choixValides)
        {
            string valeur;

            do
            {
                Console.Write(message);
                valeur = Console.ReadLine()?.Trim() ?? "";

                string? choixTrouve = choixValides
                    .FirstOrDefault(c => c.Equals(valeur, StringComparison.OrdinalIgnoreCase));

                if (choixTrouve != null)
                    return choixTrouve;

                Console.WriteLine("Choix invalide.");
            }
            while (true);
        }

        private DateTime LireDate(string message)
        {
            DateTime date;

            while (true)
            {
                Console.Write(message);
                string saisie = Console.ReadLine()?.Trim() ?? "";

                if (DateTime.TryParse(saisie, out date))
                    return date;

                Console.WriteLine("Date invalide. Exemple attendu : 2024-05-14");
            }
        }

        private bool LireOuiNon(string message)
        {
            string reponse = LireChoix(message, new[] { "oui", "non" });
            return reponse == "oui";
        }
    }
}