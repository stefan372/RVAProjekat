using System;

namespace RVAProj.Models
{
    public class SoftverskiProjekat
    {
        public Guid Id { get; set; }
        public string Naziv { get; set; }
        public string Klijent { get; set; }
        public int BrojClanovaTima { get; set; }
        public string Opis { get; set; }
    }
}
