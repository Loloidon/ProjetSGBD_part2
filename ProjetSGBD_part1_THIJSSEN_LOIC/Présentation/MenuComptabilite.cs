using ProjetSGBD_part1_THIJSSEN_LOIC.DataAccess;
using ProjetSGBD_part1_THIJSSEN_LOIC.Metier;

namespace ProjetSGBD_part1_THIJSSEN_LOIC.Présentation
{
    public class MenuCompatibilite
    {
        private readonly CompatibiliteRepository _compatibiliteRepository;
        private readonly AnimalRepository _animalRepository;

        public MenuCompatibilite(
            CompatibiliteRepository compatibiliteRepository,
            AnimalRepository animalRepository)
        {
            _compatibiliteRepository = compatibiliteRepository;
            _animalRepository = animalRepository;
        }

        public void AjouterCompatibilite()
        {
            Compatibilite compatibilite = new Compatibilite();

            _animalRepository.AfficherTout();

            compatibilite.IdAnimal = LireEntier("Entrez l'ID de l'animal : ");

            compatibilite.TypeCompatibilite = LireChoix(
                "Type de compatibilité (chat/chien/jeune_enfant/enfant/jardin/poney) : ",
                new[] { "chat", "chien", "jeune_enfant", "enfant", "jardin", "poney" }
            );

            compatibilite.Valeur = LireChoix(
                "Valeur (oui/non/non_teste) : ",
                new[] { "oui", "non", "non_teste" }
            );

            Console.Write("Description optionnelle : ");
            compatibilite.Description = Console.ReadLine()?.Trim() ?? "";

            _compatibiliteRepository.Ajouter(compatibilite);

            Console.WriteLine("Compatibilité ajoutée !");
        }
        public void SupprimerCompatibilite()
        {
            _animalRepository.AfficherTout();

            int idAnimal = LireEntier("Entrez l'ID de l'animal : ");

            string typeCompatibilite = LireChoix(
                "Type de compatibilité à supprimer (chat/chien/jeune_enfant/enfant/jardin/poney) : ",
                new[] { "chat", "chien", "jeune_enfant", "enfant", "jardin", "poney" }
            );

            _compatibiliteRepository.Supprimer(idAnimal, typeCompatibilite);

            Console.WriteLine("Compatibilité supprimée !");
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