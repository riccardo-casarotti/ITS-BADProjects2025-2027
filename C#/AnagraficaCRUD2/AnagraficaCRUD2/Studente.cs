
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnagraficaCRUD2
{
    internal class Studente
    {
        public int Matricola { get; set; }
        public string Nome { get; set; }
        public string Cognome { get; set; }
        public string Email { get; set; }
        public string Classe { get; set; }

        public override string ToString()
        {
            return $"{{{nameof(Matrice)}={Matrice.ToString()}, {nameof(Nome)}={Nome}, {nameof(Cognome)}={Cognome}, {nameof(Email)}={Email}, {nameof(Classe)}={Classe}}}";
        }
    }
}
