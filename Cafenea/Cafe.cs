using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Cafe.GestiuneCafenea;

namespace Cafe
{
    class Cafe
    {
        private List<Bautura> meniu = new List<Bautura>();
        private List<Comanda> comenzi = new List<Comanda>();
        private string numeCafenea;
        private int nextOrderId = 1;

        public Cafe(string nume)
        {
            numeCafenea = nume;
            InitializeazaMeniu();
        }

        private void InitializeazaMeniu()
        {
            meniu.Add(new Bautura(TipBautura.Espresso, 8.50m, 30));
            meniu.Add(new Bautura(TipBautura.Cappuccino, 12.00m, 200));
            meniu.Add(new Bautura(TipBautura.Latte, 13.50m, 250));
            meniu.Add(new Bautura(TipBautura.Americano, 10.00m, 150));
            meniu.Add(new Bautura(TipBautura.Ceai, 7.00m, 250));
            meniu.Add(new Bautura(TipBautura.Suc, 9.00m, 330));
            meniu.Add(new Bautura(TipBautura.Apa, 5.00m, 500));
        }

        public void AfiseazaMeniu()
        {
            Console.WriteLine($"\nMeniu {numeCafenea}:");
            for (int i = 0; i < meniu.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {meniu[i]}");
            }
        }

        public void PlasareComanda()
        {
            AfiseazaMeniu();
            Console.Write("\nSelectati numarul bauturii dorite: ");
            if (int.TryParse(Console.ReadLine(), out int optiune) && optiune >= 1 && optiune <= meniu.Count)//verifica daca optiunea este valida
            {
                var bauturaSelectata = meniu[optiune - 1];//optine bautura aleasa
                var comanda = new Comanda(nextOrderId++, bauturaSelectata);//creeaza o comanda noua auto-incrementata
                comenzi.Add(comanda);//adauga comanda in lista
                Console.WriteLine($"\nAti plasat comanda #{comanda.Id}: {bauturaSelectata.Tip}");
            }
            else
            {
                Console.WriteLine("Optiune invalida!");
            }
        }

        public void ActualizeazaComanda()
        {
            AfiseazaComenzi();
            if (comenzi.Count == 0) return;

            Console.Write("\nIntroduceti ID-ul comenzii de actualizat: ");
            if (int.TryParse(Console.ReadLine(), out int idComanda))
            {
                var comanda = comenzi.Find(c => c.Id == idComanda);
                if (comanda != null)
                {
                    Console.WriteLine("\nStari disponibile:");
                    foreach (StareComanda stare in Enum.GetValues(typeof(StareComanda)))//parcurge toate starile posibile
                    {
                        Console.WriteLine($"{(int)stare}. {stare}");
                    }
                    Console.Write("Selectati noua stare: ");
                    if (int.TryParse(Console.ReadLine(), out int optiuneStare) &&
                        Enum.IsDefined(typeof(StareComanda), optiuneStare))
                    {
                        comanda.ActualizeazaStare((StareComanda)optiuneStare);
                    }
                    else
                    {
                        Console.WriteLine("Stare invalida!");
                    }
                }
                else
                {
                    Console.WriteLine("Comanda nu a fost gasita!");
                }
            }
            else
            {
                Console.WriteLine("ID invalid!");
            }
        }
        public void AfiseazaComenzi()
        {
            Console.WriteLine($"\nComenzi active in {numeCafenea}:");
            if (comenzi.Count == 0)
            {
                Console.WriteLine("Nu exista comenzi active.");
                return;
            }

            foreach (var comanda in comenzi)
            {
                Console.WriteLine(comanda);
            }
        }

        public void SalveazaDatele()
        {
            try
            {
                using (StreamWriter sw = new StreamWriter("date_cafenea.txt"))//creeaza sau suprascrie fisierul
                {
                    // Salvăm comenzile
                    foreach (var comanda in comenzi)
                    {
                        sw.WriteLine($"COMANDA|{comanda.Id}|{(int)comanda.Bautura.Tip}|{comanda.Bautura.Pret}|" +
                                     $"{comanda.Bautura.CantitateML}|{(int)comanda.Stare}|{comanda.DataPlasare:yyyy-MM-dd HH:mm:ss}");
                    }
                }
                Console.WriteLine("Datele au fost salvate cu succes in 'date_cafenea.txt'.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Eroare la salvarea datelor: {ex.Message}");
            }
        }

        public void IncarcaDatele()
        {
            try
            {
                if (!File.Exists("date_cafenea.txt"))
                {
                    Console.WriteLine("Fisierul cu date nu exista.");
                    return;
                }
                comenzi.Clear();
                using (StreamReader sr = new StreamReader("date_cafenea.txt"))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        var parts = line.Split('|');
                        if (parts[0] == "COMANDA" && parts.Length == 7)
                        {
                            var tipBautura = (TipBautura)int.Parse(parts[2]);
                            //cauta bautura in meniu sau creeaza una noua
                            var bautura = meniu.Find(b => b.Tip == tipBautura) ??
                                         new Bautura(tipBautura, decimal.Parse(parts[3]), int.Parse(parts[4]));

                            var comanda = new Comanda(int.Parse(parts[1]), bautura)
                            {
                                Stare = (StareComanda)int.Parse(parts[5]),
                                DataPlasare = DateTime.Parse(parts[6])
                            };
                            comenzi.Add(comanda);
                            nextOrderId = Math.Max(nextOrderId, comanda.Id + 1);
                        }
                    }
                }
                Console.WriteLine("Datele au fost incarcate cu succes din 'date_cafenea.txt'.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Eroare la incarcarea datelor: {ex.Message}");
            }
        }
    }
}
