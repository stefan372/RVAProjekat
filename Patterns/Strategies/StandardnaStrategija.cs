using System;
using RVAProj.Models;

namespace RVAProj.Patterns.Strategies
{
    public class StandardnaStrategija : IStanjeStrategy
    {
        public StanjeProjekta OdrediStanje(DateTime rokZavrsetka, int brojZadataka, int brojZavrsenihZadataka, StanjeProjekta trenutnoStanje)
        {
            if (trenutnoStanje == StanjeProjekta.Obustavljen)
            {
                return trenutnoStanje;
            }

            if (brojZavrsenihZadataka >= brojZadataka)
            {
                return StanjeProjekta.Zavrsen;
            }

            if (DateTime.Today > rokZavrsetka.Date)
            {
                return StanjeProjekta.Kasnjenje;
            }

            return StanjeProjekta.URazvoju;
        }
    }
}
