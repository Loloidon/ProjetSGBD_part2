namespace ProjetSGBD_part1_THIJSSEN_LOIC.Metier
{
    public class AccueilFamille
    {
        public int IdAccueil { get; set; }
        public DateTime DateDebut { get; set; }
        public DateTime? DateFin { get; set; }
        public int IdAnimal { get; set; }
        public int IdContact { get; set; }

        public string Nom { get; set; } = "";
        public string Prenom { get; set; } = "";
        public string Gsm { get; set; } = "";
        public string Mail { get; set; } = "";

        public string TypeAnimal { get; set; } = "";
        public string Sexe { get; set; } = "";
    }
}