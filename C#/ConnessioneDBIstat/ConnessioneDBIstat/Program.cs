using Microsoft.Data.SqlClient;
using System.Data;

namespace ConnessioneDatabase
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Connessione al database SQL Server");

            //Creare la stringa di connessione al database 
            //classi di una lkibreria -- ADO.NET
            // nome progetto gestisci paccheyti nu get

            SqlConnectionStringBuilder connectionString = new SqlConnectionStringBuilder();
            connectionString.DataSource = @"localhost\SQLEXPRESS"; //dbms connessione
            connectionString.UserID = "bad";
            connectionString.Password = "Its-2026";
            connectionString.InitialCatalog = "Istat"; //il nome del nostro database 
            connectionString.TrustServerCertificate = true; //accetta il certificato digitale di sql server

            //connect 
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = connectionString.ConnectionString;//ci passo l'oggetto che ho creato con la connection string 
            connection.Open();
            Console.WriteLine($"Accesso al database {connectionString.InitialCatalog} avvenuto con successo!!! ");

            //command --> ESECUZIONE DI UNA QUERY 
            SqlCommand command = new SqlCommand();

            string sql = "select rg.Denominazione as 'Ripartizione Geografica', r.Denominazione as 'Regione', p.Denominazione as Provincia, c.Denominazione as Comune " +
     "from RipartizioneGeografica rg " + // Spazio aggiunto qui alla fine
     "inner join Regione r on rg.Id = r.IdRipartizione " +
     "inner join Provincia p on r.Id = p.IdRegione " +
     "inner join Comune c ON p.Id = c.IdProvincia " +
     "where r.Denominazione = 'Piemonte'";


            command.CommandText = sql;
            command.CommandType = System.Data.CommandType.Text; // o scrivo e poi uso fix usa system data 
            command.Connection = connection;

            Console.WriteLine("Operazione Command è avvenuta con successo!");

            //oggetto che mi consente di leggere il risultato dopo che ho fatto queste operazionei 

            SqlDataReader reader = command.ExecuteReader(); // solo lettura. 

            Console.WriteLine("Estrazioni dati da tabella RipartizioneGeografica");
            while (reader.Read())
            {
                Console.Write($"Ripartizione Geografica = {reader.GetString("Ripartizione Geografica")};");
                Console.Write($"Regione= {reader.GetString("Regione")};");
                Console.Write($"Provincia= {reader.GetString("Provincia")};");
                Console.Write($"Provincia= {reader.GetString("Comune")};");
                
            }
            reader.Close();
            command.Dispose(); //distrugge oggetto
            connection.Close(); // chiusura della connessione col database


        }
    }
}
