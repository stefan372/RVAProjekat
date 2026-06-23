using System;
using RVAProj.Patterns;
using RVAProj.Patterns.Strategies;

namespace RVAProj.Models
{
    public class ProjektnaMetrika
    {
        private Guid _projekatId;
        private DateTime _rokZavrsetka;
        private int _brojZadataka;
        private int _brojZavrsenihZadataka;
        private int _brojAngazovanihClanova;
        private IStanjeStrategy _stanjeStrategy;

        public ProjektnaMetrika()
        {
            _stanjeStrategy = new StandardnaStrategija();
        }

        public ProjektnaMetrika(IStanjeStrategy stanjeStrategy)
        {
            _stanjeStrategy = stanjeStrategy ?? new StandardnaStrategija();
        }

        public Guid ProjekatId
        {
            get { return _projekatId; }
            set
            {
                if (value == Guid.Empty)
                {
                    throw new ArgumentException("ID projekta ne moze biti prazan.");
                }
                _projekatId = value;
            }
        }

        public DateTime RokZavrsetka
        {
            get { return _rokZavrsetka; }
            set
            {
                if (value == DateTime.MinValue)
                {
                    throw new ArgumentException("Rok zavrsetka mora biti validan datum.");
                }
                _rokZavrsetka = value;
            }
        }

        public int BrojZadataka
        {
            get { return _brojZadataka; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Broj zadataka ne moze biti negativan.");
                }
                _brojZadataka = value;
            }
        }

        public int BrojZavrsenihZadataka
        {
            get { return _brojZavrsenihZadataka; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Broj zavrsenih zadataka ne moze biti negativan.");
                }
                _brojZavrsenihZadataka = value;
            }
        }

        public int BrojAngazovanihClanova
        {
            get { return _brojAngazovanihClanova; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Broj angazovanih clanova ne moze biti negativan.");
                }
                _brojAngazovanihClanova = value;
            }
        }

        public StanjeProjekta Stanje { get; set; }

        public void PostaviStrategiju(IStanjeStrategy strategija)
        {
            if (strategija == null)
            {
                throw new ArgumentNullException(nameof(strategija), "Strategija ne moze biti null.");
            }

            _stanjeStrategy = strategija;
        }

        public void AzurirajStanje()
        {
            try
            {
                Stanje = _stanjeStrategy.OdrediStanje(RokZavrsetka, BrojZadataka, BrojZavrsenihZadataka, Stanje);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Greska prilikom azuriranja stanja projekta.", ex);
            }
        }
    }
}
