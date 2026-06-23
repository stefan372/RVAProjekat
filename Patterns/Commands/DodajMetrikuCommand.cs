using System;
using RVAProj.Components;
using RVAProj.Models;

namespace RVAProj.Patterns.Commands
{
    public class DodajMetrikuCommand : ICommand
    {
        private readonly Komponenta1 _komponenta;
        private readonly ProjektnaMetrika _metrika;

        public DodajMetrikuCommand(Komponenta1 komponenta, ProjektnaMetrika metrika)
        {
            if (komponenta == null)
            {
                throw new ArgumentNullException(nameof(komponenta), "Komponenta ne moze biti null.");
            }

            if (metrika == null)
            {
                throw new ArgumentNullException(nameof(metrika), "Metrika ne moze biti null.");
            }

            _komponenta = komponenta;
            _metrika = metrika;
        }

        public void Execute()
        {
            _komponenta.DodajMetriku(_metrika);
        }
    }
}
