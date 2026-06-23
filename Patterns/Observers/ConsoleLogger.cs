using System;

namespace RVAProj.Patterns.Observers
{
    public class ConsoleLogger : IObserver
    {
        public void Update(string poruka)
        {
            Console.WriteLine($"[LOG] {poruka}");
        }
    }
}
