namespace ProjetSGBD_part1_THIJSSEN_LOIC.Metier
{
    public class ProcessusAdoption
    {
        public int IdProcessus { get; set; }
        public DateTime DateDemande { get; set; }
        public string StatutAdoption { get; set; } = "";
        public int IdAnimal { get; set; }
        public int IdContact { get; set; }

        // Affichage WPF
        public string NomAnimal { get; set; } = "";
        public string NomContact { get; set; } = "";
        public string PrenomContact { get; set; } = "";
    }
}