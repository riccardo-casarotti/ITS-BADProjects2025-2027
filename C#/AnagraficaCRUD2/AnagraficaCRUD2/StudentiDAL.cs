using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace AnagraficaCRUD2
{
    internal class StudentiDAL
    {
        private SqlConnectionStringBuilder connectionString;

        public StudentiDAL()
        {
            SqlConnectionStringBuilder connectionString = new SqlConnectionStringBuilder();
            connectionString.DataSource = @"localhost\SQLEXPRESS"; //dbms connessione
            connectionString.UserID = "bad";
            connectionString.Password = "Its-2026";
            connectionString.InitialCatalog = "Anagrafica"; //il nome del nostro database 
            connectionString.TrustServerCertificate = true; //accetta il certificato digitale di sql server

        }

        public List<Studente> Elenco()
        {
            List<Studente> Lista = new List<Studente>();

            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = connectionString.ConnectionString;
                connection.Open();

                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandText = "Select * From Studenti";
                    command.CommandType = System.Data.CommandType.Text;
                    command.Connection = connection;

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Lista.Add(new Studente { Matricola = reader.GetInt32("Matricola"),
                                Nome = reader.GetString("Nome"),
                                Cognome = reader.GetString("Cognome"),
                                Email = reader.GetString("Email"),
                                Classe = reader.GetString("Classe"),

                            };
                        }
                    }
                }
                return Lista;
            };


            public Studente? Dettaglio(int Matricola)
        {
            Studente? studente = null; //studente non trovato 

            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = connectionString.ConnectionString;
                connection.Open();

                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandText = "Select * From Studenti";
                    command.CommandType = System.Data.CommandType.Text;
                    command.Connection = connection;

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            studente = new Studente
                            {
                                Matricola = reader.GetInt32("Matricola"),
                                Nome = reader.GetString("Nome"),
                                Cognome = reader.GetString("Cognome"),
                                Email = reader.GetString("Email"),
                                Classe = reader.GetString("Classe"),

                            };
                        }
                    }
                }


                return studente;
            }
        }


             public bool Nuovo(Studente studente)
        {
            int rows = 0; // nessuno studente inserito 

            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = connectionString.ConnectionString;
                connection.Open();

                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandText = "INSERT INTO Studente (Matricola, Nome, Cognome, Email, Classe) VALUES(@Matricola, @Nome, @Cognome, @Email, @Classe)\";";
                    command.CommandType = System.Data.CommandType.Text;
                    command.Connection = connection;

                    command.Parameters.Add("@Matricola",SqlDbType.Int).Value = studente.Matricola;
                    command.Parameters.Add("@Nome",SqlDbType.VarChar,50).Value = studente.Nome;
                    command.Parameters.Add("@Cognome", SqlDbType.VarChar,50).Value = studente.Cognome;
                    command.Parameters.Add("@Email", SqlDbType.VarChar,50).Value = studente.Email;
                    command.Parameters.Add("@Classe", SqlDbType.VarChar,50).Value = studente.Classe;

                    rows = command.ExecuteNonQuery();
                        
                    
                }


                
            }

            return rows == 1;

        }
    }
}
