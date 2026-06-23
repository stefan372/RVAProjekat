using System;

namespace RVAProj.Patterns.Observers
{
    public class StatistikaObserver : IObserver
    {
        private int _brojProjekata = 0;
        private int _brojMetrika = 0;

        public void Update(string poruka)
        {
            if (poruka.Contains("Projekat dodat"))
            {
                _brojProjekata++;
            }
            else if (poruka.Contains("Metrika dodata"))
            {
                _brojMetrika++;
            }

            Console.WriteLine($"[STATISTIKA] Projekata: {_brojProjekata}, Metrika: {_brojMetrika}");
        }
    }
}
