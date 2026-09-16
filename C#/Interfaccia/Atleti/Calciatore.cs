using System;
using System.Collections.Generic;
using System.Text;

namespace Atleti
{
    internal class Calciatore : Atleta, ICloneable, IComparable<Calciatore>
    {
        //proprietà
        public int GoalSegnati { get; set; }
        public int PartiteGiocate{ get; set; }
        
        //metodo
        public object Clone()
        {
            if (PartiteGiocate == 0)
                throw new NotImplementedException("Oggetto non clonato! Partite giocato = 0");
            return MemberwiseClone(); // funzione standard che mi copia l' oggetto.
        }

        //compare to -> 
        /*
         return 1 se this.MediaGoalSegnati > other.MediaGoalSegnati
        return -1 se this.MediaGoalSegnati < other.MediaGoalSegnati
        return 0 in tutti gli altri casi
         */
        public int CompareTo(Calciatore? other)
        {
            if (other == null)
                return 0;
            if (this.MediaGoalSegnati() > other.MediaGoalSegnati())
                return 1;
            else if (this.MediaGoalSegnati() < other.MediaGoalSegnati())
                return 1;
            return 0;
        }

        public override bool Equals(object? obj)
        {
            return obj is Calciatore calciatore &&
                   Nome == calciatore.Nome &&
                   Cognome == calciatore.Cognome &&
                   NumeroPettorina == calciatore.NumeroPettorina &&
                   Disciplina == calciatore.Disciplina &&
                   GoalSegnati == calciatore.GoalSegnati &&
                   PartiteGiocate == calciatore.PartiteGiocate;
        }// il metodo equals esiste sempre, l ofaccio dalla lampadina 

        

        public double MediaGoalSegnati()
        {
            return (double)GoalSegnati  / PartiteGiocate; // ho fatto un casting 
        }
        
        public override string ToString()
        {
            return base.ToString() + $", Partite giocate = {PartiteGiocate}" +
                 $", Goal segnati = {GoalSegnati}" +
                 $", {MediaGoalSegnati()}";
        }

    }
}
