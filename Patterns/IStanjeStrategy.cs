using System;
using RVAProj.Models;

namespace RVAProj.Patterns
{
    public interface IStanjeStrategy
    {
        StanjeProjekta OdrediStanje(DateTime rokZavrsetka, int brojZadataka, int brojZavrsenihZadataka, StanjeProjekta trenutnoStanje);
    }
}
