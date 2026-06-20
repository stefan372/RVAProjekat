using System;
using System;
using RVAProj.Components;
using RVAProj.Models;

namespace RVAProj
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var komponenta1 = new Komponenta1();
            var komponenta2 = new Komponenta2(komponenta1);

            var projekat1 = new SoftverskiProjekat
            {
                Id = Guid.NewGuid(),
                Naziv = "projekat1",
                Klijent = "Klijent A",
                BrojClanovaTima = 5,
                Opis = "Razvoj poslovne aplikacije"
            };

            var projekat2 = new SoftverskiProjekat
            {
                Id = Guid.NewGuid(),
                Naziv = "projekat2",
                Klijent = "Klijent B",
                BrojClanovaTima = 7,
                Opis = "Interni sistem za izvestaje"
            };

            komponenta1.DodajProjekat(projekat1);
            komponenta1.DodajProjekat(projekat2);

            komponenta1.DodajMetriku(new ProjektnaMetrika
            {
                ProjekatId = projekat1.Id,
                RokZavrsetka = new DateTime(2026, 5, 25),
                BrojZadataka = 10,
                BrojZavrsenihZadataka = 8,
                BrojAngazovanihClanova = 20
            });

            komponenta1.DodajMetriku(new ProjektnaMetrika
            {
                ProjekatId = projekat1.Id,
                RokZavrsetka = new DateTime(2026, 5, 26),
                BrojZadataka = 10,
                BrojZavrsenihZadataka = 9,
                BrojAngazovanihClanova = 20
            });

            komponenta1.DodajMetriku(new ProjektnaMetrika
            {
                ProjekatId = projekat1.Id,
                RokZavrsetka = new DateTime(2026, 5, 27),
                BrojZadataka = 10,
                BrojZavrsenihZadataka = 10,
                BrojAngazovanihClanova = 13
            });

            komponenta1.DodajMetriku(new ProjektnaMetrika
            {
                ProjekatId = projekat2.Id,
                RokZavrsetka = new DateTime(2026, 5, 26),
                BrojZadataka = 15,
                BrojZavrsenihZadataka = 10,
                BrojAngazovanihClanova = 25
            });

            komponenta1.DodajMetriku(new ProjektnaMetrika
            {
                ProjekatId = projekat2.Id,
                RokZavrsetka = new DateTime(2026, 5, 28),
                BrojZadataka = 15,
                BrojZavrsenihZadataka = 15,
                BrojAngazovanihClanova = 35
            });

            Console.WriteLine("Pregled po periodu:");
            var pregled = komponenta2.PrilagodiMetrikeZaPeriod(new DateTime(2026, 5, 25), new DateTime(2026, 5, 27));
            foreach (var stavka in pregled)
            {
                Console.WriteLine(stavka.Key);
                foreach (var metrika in stavka.Value)
                {
                    Console.WriteLine($"{metrika.ProjekatId} -> [{metrika.BrojZadataka}, {metrika.BrojZavrsenihZadataka}, {metrika.BrojAngazovanihClanova}]");
                }
            }

            Console.WriteLine();
            Console.WriteLine("Statistika:");
            foreach (var zapis in komponenta2.NajviseZavrsenihZadatakaPoProjektu())
            {
                Console.WriteLine($"{zapis.Key}: max zavrsenih = {zapis.Value.BrojZavrsenihZadataka}, angazovanih = {zapis.Value.BrojAngazovanihClanova}");
            }

            foreach (var zapis in komponenta2.ProsecnoBrojZadatakaPoProjektu())
            {
                Console.WriteLine($"{zapis.Key}: {zapis.Value}");
            }

            Console.WriteLine($"Broj kasnjenja: {komponenta2.BrojKasnjenja()}");
        }
    }
}
