using System;
using System.Collections.Generic;

namespace RVAProj.Patterns.Commands
{
    public class CommandInvoker
    {
        private readonly List<ICommand> _komande = new List<ICommand>();

        public void DodajKomandu(ICommand komanda)
        {
            if (komanda == null)
            {
                throw new ArgumentNullException(nameof(komanda), "Komanda ne moze biti null.");
            }

            _komande.Add(komanda);
        }

        public void IzvrsiSve()
        {
            try
            {
                foreach (var komanda in _komande)
                {
                    komanda.Execute();
                }
                _komande.Clear();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Greska prilikom izvrsavanja komandi.", ex);
            }
        }
    }
}
