
using System.Linq.Expressions;

namespace FileCSV_Lettura
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("File CSV-Scrittura");

            Persona p = new Persona();
            string path = @"..\..\..\File\Persone.csv";
            string txt;

            try
            {
                using (StreamReader sw = new StreamReader(path))
                {
                    txt = sw.ReadToEnd(); // non devo fare il closing etc.. perchè uso using 
                }



                string[] righe = txt.Split('\n');

                for (int i = 0; i < righe.Length; i++)
                {
                    string[] dati = righe[i].Split(";");
                    p = new Persona();
                    p.Nome = dati[0];
                    p.Cognome = dati[1];
                    p.LuogoNascita = dati[2];
                    p.DataDiNascita = DateTime.Parse(dati[3]);

                    switch (dati[4])
                    {
                        case "F": p.Sesso = TipoSesso.F; break;
                        case "M": p.Sesso = TipoSesso.M; break;
                    }
                    Console.WriteLine("Operazione avvenuta con successo");
                    Console.WriteLine($" \n{p}");
                }


                
            }
            catch (FileNotFoundException e)
            {

                Console.WriteLine($"{e.Message}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Si è verigicato un errore non previsto \n {e.Message}");
            }


        }
    }
}
