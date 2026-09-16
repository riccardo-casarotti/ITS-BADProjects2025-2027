using System;
using System.Collections.Generic;
using System.Text;

namespace Atleti
{
    internal interface IAtletaUniversale : IAtleta, ITennista, INuotatore
    {
        public string Mangio();
        public string Bevo();
    }
}
