using Microsoft.Data.SqlClient;

namespace AnagraficaCRUD2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Connessione al database SQL Server");

            ////Creare la stringa di connessione al database 
            ////classi di una lkibreria -- ADO.NET
            //// nome progetto gestisci paccheyti nu get

            //SqlConnectionStringBuilder connectionString = new SqlConnectionStringBuilder();
            //connectionString.DataSource = @"localhost\SQLEXPRESS"; //dbms connessione
            //connectionString.UserID = "bad";
            //connectionString.Password = "Its-2026";
            //connectionString.InitialCatalog = "Anagrafica"; //il nome del nostro database 
            //connectionString.TrustServerCertificate = true; //accetta il certificato digitale di sql server

            ////connect 
            //SqlConnection connection = new SqlConnection();
            //connection.ConnectionString = connectionString.ConnectionString;//ci passo l'oggetto che ho creato con la connection string 
            //connection.Open();
            //Console.WriteLine("Accesso al database Anagrafica avvenuto con successo!!! ");



            //Console.WriteLine("Operazione Command è avvenuta con successo!");

            ////oggetto che mi consente di leggere il risultato dopo che ho fatto queste operazionei 





            //string queryCreate = "INSERT INTO Studente (Matricola, Nome, Cognome, Email, Classe) VALUES(010122,'Riccardo', 'Casarotti', 'riccardo.casarotti123456@gmail.com', 'BAD')";
            //using (SqlCommand commandCreate = new SqlCommand(queryCreate, connection))
            //{
            //    // Aggiunta sicura dei parametri
            //    commandCreate.Parameters.AddWithValue("@Nome", "Riccardo");
            //    commandCreate.Parameters.AddWithValue("@Cognome", "Casarotti");
            //    commandCreate.Parameters.AddWithValue("@Email", "riccardo.casarotti123456@gmail.com");
            //    commandCreate.Parameters.AddWithValue("@Classe", "BAD");

            //    int righeInserite = commandCreate.ExecuteNonQuery();
            //    Console.WriteLine($"Righe inserite: {righeInserite}");

            //    if (righeInserite != 1)
            //        Console.WriteLine("Errore! Dati non inseriti");
            //    else Console.WriteLine("Operazione avvenuta Correttamente");
            //}



            ////command-- > ESECUZIONE DI UNA QUERY
            //SqlCommand command = new SqlCommand();
            //string sql = "select Nome, Cognome, Email,Classe from Studente\r\nwhere Matricola = 010122;";
            //command.CommandText = sql;
            //command.CommandType = System.Data.CommandType.Text; // o scrivo e poi uso fix usa system data 
            //command.Connection = connection;

            //Console.WriteLine("Estrazioni dati da tabella Studente");
            //SqlDataReader reader = command.ExecuteReader(); // solo lettura. 
            //while (reader.Read())
            //{
            //    Console.Write($"Matricola = {reader.GetInt32("Matricola")};");
            //    Console.Write($"Cognome = {reader.GetString("Cognome")};");
            //    Console.Write($"Nome = {reader.GetString("Nome")};");
            //    Console.Write($"Email = {reader.GetString("Email")};");
            //    Console.Write($"Classe = {reader.GetString("Classe")}\n");

            //}
            //reader.Close();
            //command.Dispose(); //distrugge oggetto
            //connection.Close(); // chiusura della connessione col database

            StudentiDAL dal = new StudentiDAL();
            Console.WriteLine($"Elenco Studenti:");
            Console.WriteLine(string.Join("/n,", dal.Elenco()));


            Console.WriteLine("Inserisci la Matricola Dello studente da cercare: ");
            int matricola = int.Parse(Console.ReadLine());

            Studente s = dal.Dettaglio(matricola);

            if (s != null)
                Console.WriteLine($"Scheda di dettaglio: {s}");
            else Console.WriteLine($"Studente con matricola {matricola} non trovato");


            //dati in input per inserire un nuovo studente 
            Studente studente = new Studente();

            Console.WriteLine("Inserisci un nuovo Studente");
            Console.Write("Matricola: ");
            studente.Matricola = int.Parse(Console.ReadLine());
            Console.Write("Nome: ");
            studente.Nome = Console.ReadLine();
            Console.Write("Cognome: ");
            studente.Cognome = Console.ReadLine();
            Console.Write("Email: ");
            studente.Email = Console.ReadLine();
            Console.Write("Classe: ");
            studente.Classe = Console.ReadLine();

            if (dal.Nuovo(studente))
                Console.WriteLine("Inserimento avvenuto con successo");
            else
                Console.WriteLine("Inserimento fallito");



        }
    }
}
