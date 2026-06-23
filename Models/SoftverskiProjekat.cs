using System;

namespace RVAProj.Models
{
    public class SoftverskiProjekat
    {
        private Guid _id;
        private string _naziv;
        private string _klijent;
        private int _brojClanovaTima;
        private string _opis;

        public Guid Id
        {
            get { return _id; }
            set
            {
                if (value == Guid.Empty)
                {
                    throw new ArgumentException("ID projekta ne moze biti prazan.");
                }
                _id = value;
            }
        }

        public string Naziv
        {
            get { return _naziv; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Naziv projekta ne moze biti prazan.");
                }
                _naziv = value;
            }
        }

        public string Klijent
        {
            get { return _klijent; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Klijent ne moze biti prazan.");
                }
                _klijent = value;
            }
        }

        public int BrojClanovaTima
        {
            get { return _brojClanovaTima; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Broj clanova tima mora biti veci od 0.");
                }
                _brojClanovaTima = value;
            }
        }

        public string Opis
        {
            get { return _opis; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Opis projekta ne moze biti prazan.");
                }
                _opis = value;
            }
        }
    }
}
