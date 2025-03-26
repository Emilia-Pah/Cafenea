using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Cafe
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Bine ati venit la sistemul de gestiune a cafenelei!");
            Console.Write("Introduceti numele cafenelei: ");
            string numeCafenea = Console.ReadLine();

            Cafe cafenea = new Cafe(numeCafenea);
            cafenea.IncarcaDatele();

            while (true)
            {
                Console.WriteLine("\nMeniu Principal:");
                Console.WriteLine("1. Afiseaza meniu");
                Console.WriteLine("2. Plaseaza comanda");
                Console.WriteLine("3. Afiseaza comenzi");
                Console.WriteLine("4. Actualizeaza comanda");
                Console.WriteLine("5. Salveaza datele");
                Console.WriteLine("6. Iesire");

                Console.Write("\nSelectati o optiune: ");
                string optiune = Console.ReadLine();

                switch (optiune)
                {
                    case "1":
                        cafenea.AfiseazaMeniu();
                        break;
                    case "2":
                        cafenea.PlasareComanda();
                        break;
                    case "3":
                        cafenea.AfiseazaComenzi();
                        break;
                    case "4":
                        cafenea.ActualizeazaComanda();
                        break;
                    case "5":
                        cafenea.SalveazaDatele();
                        break;
                    case "6":
                        Console.Write("Doriti sa salvati datele inainte de iesire? (da/nu): ");
                        if (Console.ReadLine().ToLower() == "da")
                        {
                            cafenea.SalveazaDatele();
                        }
                        Console.WriteLine("La revedere!");
                        return;
                    default:
                        Console.WriteLine("Optiune invalida!");
                        break;
                }
            }
        }
    }
    
}
