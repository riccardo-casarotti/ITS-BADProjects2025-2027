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
            connectionString.InitialCatalog = "Anagrafica"; //il nome del nostro database 
            connectionString.TrustServerCertificate = true; //accetta il certificato digitale di sql server

            //connect 
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = connectionString.ConnectionString;//ci passo l'oggetto che ho creato con la connection string 
            connection.Open();
            Console.WriteLine("Accesso al database Anagrafica avvenuto con successo!!! ");

            //command --> ESECUZIONE DI UNA QUERY 
            SqlCommand command = new SqlCommand();
            string sql = "Select * From Studente";
            command.CommandText = sql;
            command.CommandType = System.Data.CommandType.Text; // o scrivo e poi uso fix usa system data 
            command.Connection = connection;

            Console.WriteLine("Operazione Command è avvenuta con successo!");

            //oggetto che mi consente di leggere il risultato dopo che ho fatto queste operazionei 

            SqlDataReader reader = command.ExecuteReader(); // solo lettura. 

            Console.WriteLine("Estrazioni dati da tabella Studente");
            while (reader.Read())
            {
                Console.Write($"Matricola = {reader.GetInt32("Matricola")};");
                Console.Write($"Cognome = {reader.GetString("Cognome")};");
                Console.Write($"Nome = {reader.GetString("Nome")};");
                Console.Write($"Email = {reader.GetString("Email")};");
                Console.Write($"Classe = {reader.GetString("Classe")}\n");

            }
            reader.Close();
            command.Dispose(); //distrugge oggetto
            connection.Close(); // chiusura della connessione col database


        }
    }
}
