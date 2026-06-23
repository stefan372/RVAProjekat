using System;
using System.Collections.Generic;
using System.Linq;
using RVAProj.Models;

namespace RVAProj.Components
{
    public class Komponenta2
    {
        private readonly Komponenta1 _komponenta1;

        public Komponenta2(Komponenta1 komponenta1)
        {
            if (komponenta1 == null)
            {
                throw new ArgumentNullException(nameof(komponenta1), "Komponenta1 ne moze biti null.");
            }

            _komponenta1 = komponenta1;
        }

        public Dictionary<string, List<MetrikaZaPrikaz>> PrilagodiMetrikeZaPeriod(DateTime datumOd, DateTime datumDo)
        {
            if (datumOd == DateTime.MinValue || datumDo == DateTime.MinValue)
            {
                throw new ArgumentException("Datumi moraju biti validni.");
            }

            if (datumOd > datumDo)
            {
                throw new ArgumentException("Datum 'od' mora biti manji ili jednak datumu 'do'.");
            }

            try
            {
                var kljuc = string.Format("({0:yyyy-MM-dd}, {1:yyyy-MM-dd})", datumOd, datumDo);

                var rezultati = _komponenta1.DohvatiMetrikeZaPeriod(datumOd, datumDo);

                if (rezultati == null)
                {
                    throw new InvalidOperationException("Rezultati ne mogu biti null.");
                }

                var prikaz = rezultati
                    .Select(m => new MetrikaZaPrikaz
                    {
                        NazivProjekta = _komponenta1.PronadjiNazivProjekta(m.ProjekatId),
                        BrojZadataka = m.BrojZadataka,
                        BrojZavrsenihZadataka = m.BrojZavrsenihZadataka,
                        BrojAngazovanihClanova = m.BrojAngazovanihClanova
                    })
                    .ToList();

                return new Dictionary<string, List<MetrikaZaPrikaz>>
                {
                    { kljuc, prikaz }
                };
            }
            catch (ArgumentException)
            {
                throw;
            }
            catch (InvalidOperationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Greska prilikom prilagodjavanja metrika za period.", ex);
            }
        }

        public (int BrojZavrsenihZadataka, int BrojAngazovanihClanova) NajveciBrojZavrsenihZadataka()
        {
            try
            {
                var metrike = _komponenta1.Metrike;

                if (metrike == null || metrike.Count == 0)
                {
                    throw new InvalidOperationException("Nema dostupnih metrika.");
                }

                var maksimalnaMetrika = metrike
                    .OrderByDescending(m => m.BrojZavrsenihZadataka)
                    .FirstOrDefault();

                if (maksimalnaMetrika == null)
                {
                    throw new InvalidOperationException("Nije pronadjena maksimalna metrika.");
                }

                return (maksimalnaMetrika.BrojZavrsenihZadataka, maksimalnaMetrika.BrojAngazovanihClanova);
            }
            catch (InvalidOperationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Greska prilikom trazenja najveceg broja zavrsenih zadataka.", ex);
            }
        }

        public Dictionary<string, (double ProsecanBrojZadataka, List<DateTime> DatumiZavrsetka)> ProsecanBrojZadatakaPoProjekatima()
        {
            try
            {
                var projekti = _komponenta1.Projekti;
                var metrike = _komponenta1.Metrike;

                if (projekti == null || projekti.Count == 0)
                {
                    throw new InvalidOperationException("Nema dostupnih projekata.");
                }

                if (metrike == null || metrike.Count == 0)
                {
                    throw new InvalidOperationException("Nema dostupnih metrika.");
                }

                var rezultat = new Dictionary<string, (double, List<DateTime>)>();

                foreach (var projekat in projekti)
                {
                    var metrikeZaProjekat = metrike
                        .Where(m => m.ProjekatId == projekat.Id)
                        .ToList();

                    if (metrikeZaProjekat.Count > 0)
                    {
                        var prosek = metrikeZaProjekat.Average(m => m.BrojZadataka);
                        var datumi = metrikeZaProjekat
                            .Select(m => m.RokZavrsetka)
                            .Distinct()
                            .OrderBy(d => d)
                            .ToList();

                        rezultat[projekat.Naziv] = (prosek, datumi);
                    }
                }

                return rezultat;
            }
            catch (InvalidOperationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Greska prilikom izracunavanja prosecnog broja zadataka.", ex);
            }
        }

        public Dictionary<string, int> BrojKasnjenjaPoProjekatima()
        {
            try
            {
                var projekti = _komponenta1.Projekti;
                var metrike = _komponenta1.Metrike;

                if (projekti == null || projekti.Count == 0)
                {
                    throw new InvalidOperationException("Nema dostupnih projekata.");
                }

                if (metrike == null || metrike.Count == 0)
                {
                    throw new InvalidOperationException("Nema dostupnih metrika.");
                }

                var rezultat = new Dictionary<string, int>();

                foreach (var projekat in projekti)
                {
                    var brojKasnjenja = metrike
                        .Where(m => m.ProjekatId == projekat.Id && m.Stanje == StanjeProjekta.Kasnjenje)
                        .Count();

                    rezultat[projekat.Naziv] = brojKasnjenja;
                }

                return rezultat;
            }
            catch (InvalidOperationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Greska prilikom prebrojavanje kasnjenja.", ex);
            }
        }
    }
}
