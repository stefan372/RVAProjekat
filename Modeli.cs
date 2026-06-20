using System;

namespace RVAProj
{
    public enum StanjeProjekta
    {
        URazvoju,
        Kasnjenje,
        Zavrsen,
        Obustavljen
    }

    public class SoftverskiProjekat
    {
        public Guid Id { get; set; }
        public string Naziv { get; set; }
        public string Klijent { get; set; }
        public int BrojClanovaTima { get; set; }
        public string Opis { get; set; }
    }

    public class ProjektnaMetrika
    {
        public Guid ProjekatId { get; set; }
        public DateTime RokZavrsetka { get; set; }
        public int BrojZadataka { get; set; }
        public int BrojZavrsenihZadataka { get; set; }
        public int BrojAngazovanihClanova { get; set; }
        public StanjeProjekta Stanje { get; set; }
    }
}
