using System;
using RVAProj.Components;
using RVAProj.Models;

namespace RVAProj.Patterns.Commands
{
    public class DodajProjekatCommand : ICommand
    {
        private readonly Komponenta1 _komponenta;
        private readonly SoftverskiProjekat _projekat;

        public DodajProjekatCommand(Komponenta1 komponenta, SoftverskiProjekat projekat)
        {
            if (komponenta == null)
            {
                throw new ArgumentNullException(nameof(komponenta), "Komponenta ne moze biti null.");
            }

            if (projekat == null)
            {
                throw new ArgumentNullException(nameof(projekat), "Projekat ne moze biti null.");
            }

            _komponenta = komponenta;
            _projekat = projekat;
        }

        public void Execute()
        {
            _komponenta.DodajProjekat(_projekat);
        }
    }
}
