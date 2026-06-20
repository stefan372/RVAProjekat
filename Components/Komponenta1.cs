using System;
using System;
using System.Collections.Generic;
using System.Linq;
using RVAProj.Models;

namespace RVAProj.Components
{
    public class Komponenta1
    {
        private readonly List<SoftverskiProjekat> _projekti = new List<SoftverskiProjekat>();
        private readonly List<ProjektnaMetrika> _metrike = new List<ProjektnaMetrika>();

        public void DodajProjekat(SoftverskiProjekat projekat)
        {
            _projekti.Add(projekat);
        }

        public void DodajMetriku(ProjektnaMetrika metrika)
        {
            metrika.AzurirajStanje();
            _metrike.Add(metrika);
        }

        public IReadOnlyList<SoftverskiProjekat> Projekti
        {
            get { return _projekti.AsReadOnly(); }
        }

        public IReadOnlyList<ProjektnaMetrika> Metrike
        {
            get { return _metrike.AsReadOnly(); }
        }

        public Dictionary<string, List<ProjektnaMetrika>> DohvatiMetrikeZaPeriod(DateTime datumOd, DateTime datumDo)
        {
            var kljuc = string.Format("{0:yyyy-MM-dd} - {1:yyyy-MM-dd}", datumOd, datumDo);

            var vrednosti = _metrike
                .Where(m => m.RokZavrsetka.Date >= datumOd.Date && m.RokZavrsetka.Date <= datumDo.Date)
                .ToList();

            return new Dictionary<string, List<ProjektnaMetrika>>
            {
                { kljuc, vrednosti }
            };
        }

        public string PronadjiNazivProjekta(Guid projekatId)
        {
            var projekat = _projekti.FirstOrDefault(p => p.Id == projekatId);
            return projekat != null ? projekat.Naziv : projekatId.ToString();
        }
    }
}
