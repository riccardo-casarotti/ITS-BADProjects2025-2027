using System;
using System.Collections.Generic;
using System.Text;

namespace FileCSV_Scrittura
{
    internal class Persona
    {
        public string? Nome { get; set; } //il punto di domanda davanti al tipo vuol dire che può accettare anche il null
        public string Cognome{ get; set; }
        public DateTime DataDiNascita{ get; set; } //es. 25/09/2000
        public string LuogoNascita{ get; set; }
        public TipoSesso Sesso { get; set; }

        //obbligatorietà
        


        //metodi 
        public int Eta()
        {
            DateTime oggi = DateTime.Now;
            // es 15/06/2026

            int eta = oggi.Year - DataDiNascita.Year;

            if (oggi.Month < DataDiNascita.Month)
                eta--;//decremento -- era = eta -1 
            else if (oggi.Month == DataDiNascita.Month && oggi.Day < DataDiNascita.Day)
                eta--;
            return eta;

        }

        public  string? FormatCSV() //parametro di configurazione
        {
            //es. nome;cognome;datanascita;luogonascita;sesso
            return $"{Nome};" + $"{Cognome};" + $"{LuogoNascita};" + $"{DataDiNascita};" + $"{Sesso};";
        }
        public override string? ToString() //sto richiamando qualcosa di già esistente, o uso l'override perchè cambio robe già 
        {
            return $"Nome: {Nome}" +
                $"Cognome: {Cognome}" +
                $"LuogoNascita: {LuogoNascita}" +
                $"DataDiNascita: {DataDiNascita.ToShortDateString()}" + //to short serve per il formato della data
                $"Sesso: {Sesso}" +
                $"Eta': {Eta()}";
        }
    }
}
