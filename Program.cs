using System;
using System.Linq;
using RVAProj.Components;
using RVAProj.Models;
using RVAProj.Patterns.Observers;
using RVAProj.Patterns.Commands;

namespace RVAProj
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("=== DEMONSTRACIJA DIZAJN PATERNA ===\n");

                var komponenta1 = new Komponenta1();
                var komponenta2 = new Komponenta2(komponenta1);

                Console.WriteLine("--- OBSERVER PATTERN ---");
                var consoleLogger = new ConsoleLogger();
                var statistikaObserver = new StatistikaObserver();
                komponenta1.PrijaviObserver(consoleLogger);
                komponenta1.PrijaviObserver(statistikaObserver);
                Console.WriteLine("Observers prijavljeni.\n");

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

                Console.WriteLine("\n--- COMMAND PATTERN ---");
                var invoker = new CommandInvoker();
                invoker.DodajKomandu(new DodajProjekatCommand(komponenta1, projekat1));
                invoker.DodajKomandu(new DodajProjekatCommand(komponenta1, projekat2));
                Console.WriteLine("Komande za dodavanje projekata kreiranje.\n");

                Console.WriteLine("Izvrsavam komande:");
                invoker.IzvrsiSve();

                Console.WriteLine("\n--- STRATEGY PATTERN ---");
                Console.WriteLine("Koristim StandardnaStrategija za metrike projekta1:");
                invoker.DodajKomandu(new DodajMetrikuCommand(komponenta1, new ProjektnaMetrika
                {
                    ProjekatId = projekat1.Id,
                    RokZavrsetka = datumOd,
                    BrojZadataka = 10,
                    BrojZavrsenihZadataka = 8,
                    BrojAngazovanihClanova = 20
                }));

                invoker.DodajKomandu(new DodajMetrikuCommand(komponenta1, new ProjektnaMetrika
                {
                    ProjekatId = projekat1.Id,
                    RokZavrsetka = datumOd.AddDays(1),
                    BrojZadataka = 10,
                    BrojZavrsenihZadataka = 9,
                    BrojAngazovanihClanova = 20
                }));

                invoker.DodajKomandu(new DodajMetrikuCommand(komponenta1, new ProjektnaMetrika
                {
                    ProjekatId = projekat1.Id,
                    RokZavrsetka = datumDo,
                    BrojZadataka = 10,
                    BrojZavrsenihZadataka = 10,
                    BrojAngazovanihClanova = 13
                }));

                invoker.IzvrsiSve();

                Console.WriteLine("\nDodajem metrike za projekat2:");
                invoker.DodajKomandu(new DodajMetrikuCommand(komponenta1, new ProjektnaMetrika
                {
                    ProjekatId = projekat2.Id,
                    RokZavrsetka = datumOd.AddDays(1),
                    BrojZadataka = 15,
                    BrojZavrsenihZadataka = 10,
                    BrojAngazovanihClanova = 25
                }));

                invoker.DodajKomandu(new DodajMetrikuCommand(komponenta1, new ProjektnaMetrika
                {
                    ProjekatId = projekat2.Id,
                    RokZavrsetka = datumDo,
                    BrojZadataka = 15,
                    BrojZavrsenihZadataka = 15,
                    BrojAngazovanihClanova = 35
                }));

                invoker.IzvrsiSve();

                Console.WriteLine("\n--- REZULTATI ---");
                Console.WriteLine("Pregled po periodu:");
                var pregled = komponenta2.PrilagodiMetrikeZaPeriod(datumOd, datumDo);
                foreach (var stavka in pregled)
                {
                    foreach (var projekat in stavka.Value)
                    {
                        Console.WriteLine($"{stavka.Key}: {projekat.NazivProjekta} -> [{projekat.BrojZadataka}, {projekat.BrojZavrsenihZadataka}, {projekat.BrojAngazovanihClanova}]");
                    }
                }

                Console.WriteLine("\n--- STATISTICKE METODE ---");

                Console.WriteLine("\n1. Najveci broj zavrsenih zadataka:");
                var maxZadaci = komponenta2.NajveciBrojZavrsenihZadataka();
                Console.WriteLine($"   Broj zavrsenih: {maxZadaci.BrojZavrsenihZadataka}, Broj angazovanih clanova: {maxZadaci.BrojAngazovanihClanova}");

                Console.WriteLine("\n2. Prosecan broj zadataka po projektima:");
                var proseci = komponenta2.ProsecanBrojZadatakaPoProjekatima();
                foreach (var projekat in proseci)
                {
                    Console.WriteLine($"   {projekat.Key}:");
                    Console.WriteLine($"      Prosecan broj zadataka: {projekat.Value.ProsecanBrojZadataka:F2}");
                    Console.WriteLine($"      Predvidjeni datumi zavrsetka: {string.Join(", ", projekat.Value.DatumiZavrsetka.Select(d => d.ToString("yyyy-MM-dd")))}");
                }

                Console.WriteLine("\n3. Broj kasnjenja po projektima:");
                var kasnjenja = komponenta2.BrojKasnjenjaPoProjekatima();
                foreach (var projekat in kasnjenja)
                {
                    Console.WriteLine($"   {projekat.Key}: {projekat.Value} kasnjenja");
                }
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine($"Greska: Argument je null. Detalji: {ex.Message}");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Greska: Nevazeci argument. Detalji: {ex.Message}");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Greska: Nevazeca operacija. Detalji: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Neocekivana greska: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
            }
            finally
            {
                Console.WriteLine("\nPritisnite bilo koji taster za izlaz...");
                Console.ReadKey();
            }
        }
    }
}
