using ProjetSGBD_part1_THIJSSEN_LOIC.DataAccess;
using ProjetSGBD_part1_THIJSSEN_LOIC.Présentation;

string connectionString =
    "Host=localhost;Port=5432;Database=projet_sgbd;Username=postgres;Password=Postgres2026";

AnimalRepository animalRepository = new AnimalRepository(connectionString);
MenuAnimal menuAnimal = new MenuAnimal(animalRepository);

CompatibiliteRepository compatibiliteRepository = new CompatibiliteRepository(connectionString);
MenuCompatibilite menuCompatibilite = new MenuCompatibilite(compatibiliteRepository, animalRepository);

ContactRepository contactRepository = new ContactRepository(connectionString);
MenuContact menuContact = new MenuContact(contactRepository);

VaccinRepository vaccinRepository = new VaccinRepository(connectionString);
MenuVaccin menuVaccin = new MenuVaccin(vaccinRepository, animalRepository);

AccueilFamilleRepository accueilFamilleRepository = new AccueilFamilleRepository(connectionString);
MenuAccueilFamille menuAccueilFamille = new MenuAccueilFamille(
    accueilFamilleRepository,
    animalRepository,
    contactRepository
);

ProcessusAdoptionRepository adoptionRepository =
    new ProcessusAdoptionRepository(connectionString);

MenuProcessusAdoption menuAdoption =
    new MenuProcessusAdoption(adoptionRepository, animalRepository, contactRepository);
bool continuer = true;

while (continuer)
{
    Console.WriteLine("=== Refuge Animaux ===");
    Console.WriteLine("1. Ajouter un animal");
    Console.WriteLine("2. Afficher tous les animaux");
    Console.WriteLine("3. Afficher un animal");
    Console.WriteLine("4. Supprimer un animal");
    Console.WriteLine("5. Ajouter une compatibilité à un animal");
    Console.WriteLine("6. Supprimer une compatibilité à un animal");
    Console.WriteLine("7. Ajouter un contact");
    Console.WriteLine("8. Afficher un contact");
    Console.WriteLine("9. Supprimer un contact");
    Console.WriteLine("10. Modifier un contact");
    Console.WriteLine("11. Ajouter un vaccin à un animal");
    Console.WriteLine("12. Afficher les vaccins d'un animal");
    Console.WriteLine("13. Afficher tout les animaux présent dans le refuge");
    Console.WriteLine("14. Ajouter une famille d'accueil");
    Console.WriteLine("15. Lister familles d'accueil par animal");
    Console.WriteLine("16. Lister animaux accueillis par une famille");
    Console.WriteLine("17. Ajouter une adoption");
    Console.WriteLine("18. Lister les adoptions");
    Console.WriteLine("19. Modifier le statut d'une adoption");
    Console.WriteLine("0. Quitter");
    Console.Write("Choix : ");

    string choix = Console.ReadLine() ?? "";

    switch (choix)
    {
        case "1":
            menuAnimal.AjouterAnimal();
            break;

        case "2":
            menuAnimal.AfficherTout();
            break;

        case "3":
            menuAnimal.AfficherUn();
            break;

        case "4":
            menuAnimal.SupprimerUn();
            break;

        case "5":
            menuCompatibilite.AjouterCompatibilite();
            break;
        case "6":
            menuCompatibilite.SupprimerCompatibilite();
            break;
        case "7":
            menuContact.Ajout();
            break;

        case "8":
            menuContact.AfficherTout();
            menuContact.AfficherUn();
            break;
        case "9":
            menuContact.SupprimerUn();
            break;
        case "10":
            menuContact.Modifier();
            break;
        case "11":
            menuVaccin.AjouterVaccinAnimal();
            break;

        case "12":
            menuVaccin.AfficherVaccinsAnimal();
            break;
        case "13":
            menuAnimal.ListerAnimauxRefuge();
            break;
        case "14":
            menuAccueilFamille.AjouterAccueil();
            break;

        case "15":
            menuAccueilFamille.ListerFamillesParAnimal();
            break;

        case "16":
            menuAccueilFamille.ListerAnimauxParFamille();
            break;
        case "17":
            menuAdoption.AjouterAdoption();
            break;

        case "18":
            menuAdoption.ListerAdoptions();
            break;

        case "19":
            menuAdoption.ModifierStatut();
            break;
        case "0":
            continuer = false;
            break;

        default:
            Console.WriteLine("Choix invalide.");
            break;
    }
}