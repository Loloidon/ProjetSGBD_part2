using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetSGBD_part1_THIJSSEN_LOIC.Metier
{
    public class Contact
    {
        public int IdContact { get; set; }
        public string Nom { get; set; } = "";
        public string Prenom { get; set; } = "";
        public string Registre { get; set; } = "";
        public string Adresse { get; set; } = "";
        public string Gsm { get; set; } = "";
        public string Mail { get; set; } = "";
    }
}
