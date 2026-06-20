using System;
using System.Collections.Generic;

namespace RVAProj
{
    public class EvidencijaProjekata
    {
        private readonly List<SoftverskiProjekat> _projekti = new List<SoftverskiProjekat>();
        private readonly List<ProjektnaMetrika> _metrike = new List<ProjektnaMetrika>();

        public void DodajProjekat(SoftverskiProjekat projekat)
        {
            _projekti.Add(projekat);
        }

        public void DodajMetriku(ProjektnaMetrika metrika)
        {
            _metrike.Add(metrika);
        }

        public IReadOnlyList<SoftverskiProjekat> Projekti
        {
            get { return _projekti.AsReadOnly(); }
        }

        public IReadOnlyList<ProjektnaMetrika> Metrike
        {
            get { return _metrike.AsReadOnly(); }
        }
    }
}
