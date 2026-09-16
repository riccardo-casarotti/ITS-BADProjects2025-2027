using System;
using System.Collections.Generic;
using System.Text;

namespace Atleti
{
    internal class Atleta: IAtleta, ITennista, INuotatore, IAtletaUniversale
    {
        //proprietà
        public required string Nome { get; set; }
        public required string Cognome { get; set; }
        public required int NumeroPettorina { get; set; }
        public required string Disciplina { get; set; }


        //metodo da interffaccia
        public string Bevo()
        {
            throw new NotImplementedException();
        }
        public string Corro()
        {
            return "Sto correndo...";
        }

        public string Dorso()
        {
            throw new NotImplementedException();
        }

        public string Dritto()
        {
            throw new NotImplementedException();
        }

        public string Mangio()
        {
            throw new NotImplementedException();
        }

        public string Rana()
        {
            throw new NotImplementedException();
        }

        public string Rovescio()
        {
            throw new NotImplementedException();
        }

        public string Salto()
        {
            return "Sto saltando...";
        }





        public override string ToString()
        {
            return $"{GetType().Name}: " +
                $" {nameof(Nome)}={Nome}" +
                $", {nameof(Cognome)}={Cognome}" +
                $", {nameof(NumeroPettorina)}={NumeroPettorina.ToString()}" +
                $", {nameof(Disciplina)}={Disciplina.ToString()}";
        }
    }
}
