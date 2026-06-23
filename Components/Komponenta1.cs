using System;
using System.Collections.Generic;
using System.Linq;
using RVAProj.Models;
using RVAProj.Patterns;

namespace RVAProj.Components
{
    public class Komponenta1
    {
        private readonly List<SoftverskiProjekat> _projekti = new List<SoftverskiProjekat>();
        private readonly List<ProjektnaMetrika> _metrike = new List<ProjektnaMetrika>();
        private readonly List<IObserver> _observers = new List<IObserver>();

        public void PrijaviObserver(IObserver observer)
        {
            if (observer == null)
            {
                throw new ArgumentNullException(nameof(observer), "Observer ne moze biti null.");
            }

            _observers.Add(observer);
        }

        public void OdjaviObserver(IObserver observer)
        {
            if (observer != null)
            {
                _observers.Remove(observer);
            }
        }

        private void NotifikujObservere(string poruka)
        {
            foreach (var observer in _observers)
            {
                observer.Update(poruka);
            }
        }

        public void DodajProjekat(SoftverskiProjekat projekat)
        {
            if (projekat == null)
            {
                throw new ArgumentNullException(nameof(projekat), "Projekat ne moze biti null.");
            }

            try
            {
                if (_projekti.Any(p => p.Id == projekat.Id))
                {
                    throw new InvalidOperationException($"Projekat sa ID-em {projekat.Id} vec postoji.");
                }

                _projekti.Add(projekat);
                NotifikujObservere($"Projekat dodat: {projekat.Naziv}");
            }
            catch (ArgumentNullException)
            {
                throw;
            }
            catch (InvalidOperationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Greska prilikom dodavanja projekta.", ex);
            }
        }

        public void DodajMetriku(ProjektnaMetrika metrika)
        {
            if (metrika == null)
            {
                throw new ArgumentNullException(nameof(metrika), "Metrika ne moze biti null.");
            }

            try
            {
                if (!_projekti.Any(p => p.Id == metrika.ProjekatId))
                {
                    throw new InvalidOperationException($"Projekat sa ID-em {metrika.ProjekatId} ne postoji.");
                }

                metrika.AzurirajStanje();
                _metrike.Add(metrika);
                NotifikujObservere($"Metrika dodata za projekat ID: {metrika.ProjekatId}");
            }
            catch (ArgumentNullException)
            {
                throw;
            }
            catch (InvalidOperationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Greska prilikom dodavanja metrike.", ex);
            }
        }

        public IReadOnlyList<SoftverskiProjekat> Projekti
        {
            get { return _projekti.AsReadOnly(); }
        }

        public IReadOnlyList<ProjektnaMetrika> Metrike
        {
            get { return _metrike.AsReadOnly(); }
        }

        public List<ProjektnaMetrika> DohvatiMetrikeZaPeriod(DateTime datumOd, DateTime datumDo)
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
                return _metrike
                    .Where(m => m.RokZavrsetka.Date >= datumOd.Date && m.RokZavrsetka.Date <= datumDo.Date)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Greska prilikom dohvatanja metrika za period.", ex);
            }
        }

        public string PronadjiNazivProjekta(Guid projekatId)
        {
            if (projekatId == Guid.Empty)
            {
                throw new ArgumentException("ID projekta ne moze biti prazan.");
            }

            try
            {
                var projekat = _projekti.FirstOrDefault(p => p.Id == projekatId);
                return projekat != null ? projekat.Naziv : projekatId.ToString();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Greska prilikom pronalazenja naziva projekta.", ex);
            }
        }
    }
}
