using System;

namespace RVAProj.Models
{
    public class MetrikaZaPrikaz
    {
        private string _nazivProjekta;
        private int _brojZadataka;
        private int _brojZavrsenihZadataka;
        private int _brojAngazovanihClanova;

        public string NazivProjekta
        {
            get { return _nazivProjekta; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Naziv projekta ne moze biti prazan.");
                }
                _nazivProjekta = value;
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
    }
}
