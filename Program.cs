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

            var datumDo = DateTime.Today;
            var datumOd = datumDo.AddDays(-2);

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
                RokZavrsetka = datumOd,
                BrojZadataka = 10,
                BrojZavrsenihZadataka = 8,
                BrojAngazovanihClanova = 20
            });

            komponenta1.DodajMetriku(new ProjektnaMetrika
            {
                ProjekatId = projekat1.Id,
                RokZavrsetka = datumOd.AddDays(1),
                BrojZadataka = 10,
                BrojZavrsenihZadataka = 9,
                BrojAngazovanihClanova = 20
            });

            komponenta1.DodajMetriku(new ProjektnaMetrika
            {
                ProjekatId = projekat1.Id,
                RokZavrsetka = datumDo,
                BrojZadataka = 10,
                BrojZavrsenihZadataka = 10,
                BrojAngazovanihClanova = 13
            });

            komponenta1.DodajMetriku(new ProjektnaMetrika
            {
                ProjekatId = projekat2.Id,
                RokZavrsetka = datumOd.AddDays(1),
                BrojZadataka = 15,
                BrojZavrsenihZadataka = 10,
                BrojAngazovanihClanova = 25
            });

            komponenta1.DodajMetriku(new ProjektnaMetrika
            {
                ProjekatId = projekat2.Id,
                RokZavrsetka = datumDo,
                BrojZadataka = 15,
                BrojZavrsenihZadataka = 15,
                BrojAngazovanihClanova = 35
            });

            Console.WriteLine("Pregled po periodu:");
            var pregled = komponenta2.PrilagodiMetrikeZaPeriod(datumOd, datumDo);
            foreach (var stavka in pregled)
            {
                foreach (var projekat in stavka.Value)
                {
                    Console.WriteLine($"{stavka.Key}: {projekat.NazivProjekta} -> [{projekat.BrojZadataka}, {projekat.BrojZavrsenihZadataka}, {projekat.BrojAngazovanihClanova}]");
                }
            }

        }
    }
}
