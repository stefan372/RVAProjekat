using System;

namespace RVAProj.Models
{
    public class ProjektnaMetrika
    {
        public Guid ProjekatId { get; set; }
        public DateTime RokZavrsetka { get; set; }
        public int BrojZadataka { get; set; }
        public int BrojZavrsenihZadataka { get; set; }
        public int BrojAngazovanihClanova { get; set; }
        public StanjeProjekta Stanje { get; set; }

        public void AzurirajStanje()
        {
            if (BrojZavrsenihZadataka >= BrojZadataka)
            {
                Stanje = StanjeProjekta.Zavrsen;
                return;
            }

            if (DateTime.Today > RokZavrsetka.Date)
            {
                Stanje = StanjeProjekta.Kasnjenje;
                return;
            }

            if (Stanje != StanjeProjekta.Obustavljen)
            {
                Stanje = StanjeProjekta.URazvoju;
            }
        }
    }
}
