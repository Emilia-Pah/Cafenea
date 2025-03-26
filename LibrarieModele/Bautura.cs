using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Cafe.GestiuneCafenea;

namespace Cafe
{
    public class Bautura
    {
        public TipBautura Tip { get; set; }
        public decimal Pret { get; set; }
        public int CantitateML { get; set; }

        public Bautura(TipBautura tip, decimal pret, int cantitateML)
        {
            Tip = tip;
            Pret = pret;
            CantitateML = cantitateML;
        }

        public override string ToString()
        {
            return $"{Tip} ({CantitateML}ml) - {Pret} lei";
        }
    }
}
