using System;
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
            _komponenta1 = komponenta1;
        }

        public Dictionary<string, List<MetrikaZaPrikaz>> PrilagodiMetrikeZaPeriod(DateTime datumOd, DateTime datumDo)
        {
            var kljuc = string.Format("({0:yyyy-MM-dd}, {1:yyyy-MM-dd})", datumOd, datumDo);

            var rezultati = _komponenta1.DohvatiMetrikeZaPeriod(datumOd, datumDo);
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

    }
}
