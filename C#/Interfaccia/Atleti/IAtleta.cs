using System;
using System.Collections.Generic;
using System.Text;

namespace Atleti
{
    internal interface IAtleta
    {
        //nelle interfacce non si possono usare le graffe, perchè i metodi sono astratti
        public string Corro();
        public string Salto();
    }
}
