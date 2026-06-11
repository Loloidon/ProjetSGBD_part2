using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetSGBD_part1_THIJSSEN_LOIC.Metier
{
    public class AccueilFamille
    {
        public int IdAccueil {  get; set; }
        public DateTime DateDebut { get; set; }
        public DateTime? DateFin {  get; set; }
        public int IdAnimal { get; set; }
        public int IdContact { get; set; }
    }
}
