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

        public Dictionary<string, List<ProjektnaMetrika>> PrilagodiMetrikeZaPeriod(DateTime datumOd, DateTime datumDo)
        {
            return _komponenta1.DohvatiMetrikeZaPeriod(datumOd, datumDo);
        }

        public Dictionary<string, ProjektnaMetrika> NajviseZavrsenihZadatakaPoProjektu()
        {
            return _komponenta1.Metrike
                .GroupBy(m => m.ProjekatId)
                .Select(grupa =>
                {
                    var najbolja = grupa.OrderByDescending(x => x.BrojZavrsenihZadataka).First();
                    return new
                    {
                        Naziv = _komponenta1.PronadjiNazivProjekta(grupa.Key),
                        Metrika = najbolja
                    };
                })
                .ToDictionary(x => x.Naziv, x => x.Metrika);
        }

        public Dictionary<string, string> ProsecnoBrojZadatakaPoProjektu()
        {
            return _komponenta1.Metrike
                .GroupBy(m => m.ProjekatId)
                .Select(grupa => new
                {
                    Naziv = _komponenta1.PronadjiNazivProjekta(grupa.Key),
                    Tekst = string.Format("prosek zadataka = {0:0.00}, predvidjeni rok = {1:yyyy-MM-dd}", grupa.Average(x => x.BrojZadataka), grupa.Max(x => x.RokZavrsetka))
                })
                .ToDictionary(x => x.Naziv, x => x.Tekst);
        }

        public int BrojKasnjenja()
        {
            return _komponenta1.Metrike.Count(m => m.Stanje == StanjeProjekta.Kasnjenje);
        }
    }
}
