using ProjetSGBD_part1_THIJSSEN_LOIC.DataAccess;
using ProjetSGBD_part1_THIJSSEN_LOIC.Metier;

namespace ProjetSGBD_part1_THIJSSEN_LOIC.Présentation
{
    public class MenuContact
    {
        private readonly ContactRepository _contactRepository;

        public MenuContact(ContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        public void Ajout()
        {
            Contact contact = new Contact();

            contact.Nom = LireTexteObligatoire("Entrez le nom de la personne de contact : ");

            contact.Prenom = LireTexteObligatoire("Entrez le prénom de la personne de contact : ");

            contact.Registre = LireNombreLongueur(
                "Entrez le numéro de registre national (11 chiffres) : ",
                11
            );

            Console.Write("Entrez l'adresse de la personne de contact : ");
            contact.Adresse = Console.ReadLine() ?? "";

            contact.Gsm = LireNombreLongueur(
                "Entrez le GSM (10 chiffres) : ",
                10
            );

            Console.Write("Entrez le mail de la personne de contact : ");
            contact.Mail = Console.ReadLine() ?? "";

            _contactRepository.Ajouter(contact);

            Console.WriteLine("Contact ajouté !");
        }

        public void AfficherTout()
        {
            _contactRepository.AfficherTout();
        }

        public void AfficherUn()
        {
            int id = LireEntier(
                "Entrez l'ID du contact que vous souhaitez afficher : "
            );

            _contactRepository.AfficherUn(id);
        }

        public void SupprimerUn()
        {
            int id = LireEntier(
                "Entrez l'ID du contact que vous souhaitez supprimer : "
            );

            _contactRepository.Supprimer(id);
        }

        public void Modifier()
        {
            int id = LireEntier(
                "Entrez l'ID du contact à modifier : "
            );

            Contact contact = new Contact();

            contact.Nom = LireTexteObligatoire("Nom : ");

            contact.Prenom = LireTexteObligatoire("Prénom : ");

            contact.Registre = LireNombreLongueur(
                "Registre national (11 chiffres) : ",
                11
            );

            Console.Write("Adresse : ");
            contact.Adresse = Console.ReadLine() ?? "";

            contact.Gsm = LireNombreLongueur(
                "GSM (10 chiffres) : ",
                10
            );

            Console.Write("Mail : ");
            contact.Mail = Console.ReadLine() ?? "";

            _contactRepository.Modifier(id, contact);

            Console.WriteLine("Contact modifié !");
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

        private string LireTexteObligatoire(string message)
        {
            while (true)
            {
                Console.Write(message);

                string valeur = Console.ReadLine()?.Trim() ?? "";

                if (!string.IsNullOrWhiteSpace(valeur))
                    return valeur;

                Console.WriteLine("Ce champ est obligatoire.");
            }
        }

        private string LireNombreLongueur(string message, int longueur)
        {
            while (true)
            {
                Console.Write(message);

                string valeur = Console.ReadLine()?.Trim() ?? "";

                if (valeur.Length == longueur && valeur.All(char.IsDigit))
                    return valeur;

                Console.WriteLine(
                    $"Veuillez entrer exactement {longueur} chiffres."
                );
            }
        }
    }
}