using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetSGBD_part1_THIJSSEN_LOIC.Metier
{
    public class ProcessusAdoption
    {
        public int IdProcessus {  get; set; }
        public DateTime DateDemande { get; set; }
        public string StatutAdoption { get; set; }
        public int IdAnimal { get; set; }
        public int IdContact { get; set; }

    }
}
