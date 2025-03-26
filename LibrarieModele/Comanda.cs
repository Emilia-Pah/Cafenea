using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Cafe.GestiuneCafenea;

namespace Cafe
{
    public class Comanda
    {
        public int Id { get; set; }
        public Bautura Bautura { get; set; }
        public StareComanda Stare { get; set; }
        public DateTime DataPlasare { get; set; }

        public Comanda(int id, Bautura bautura)
        {
            Id = id;
            Bautura = bautura;
            Stare = StareComanda.Plasata;
            DataPlasare = DateTime.Now;
        }

        public void ActualizeazaStare(StareComanda nouaStare)
        {
            Stare = nouaStare;
            Console.WriteLine($"Comanda #{Id} ({Bautura.Tip}) actualizata la starea: {nouaStare}");
        }

        public override string ToString()
        {
            return $"Comanda #{Id}: {Bautura} | Stare: {Stare} | Plasata la: {DataPlasare:HH:mm:ss}";
        }
    }
}
