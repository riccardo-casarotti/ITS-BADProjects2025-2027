namespace FileTesto_Lettura
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("FileTesto_Lettura");

            string path = @"C:\file\frase.txt";

            //accesso al file di testo in modalità lettura
            StreamReader sr = new StreamReader(path); //accesso

            ////se non trova il file deve dare un exception
            //string frase = sr.ReadLine(); //operazione per una sola riga

            string frase = sr.ReadToEnd(); //legge tutto il contenuto del file, 

            //termina accesso al file 
            sr.Close(); //chiusura

            Console.WriteLine($"Lettura da file: {frase}");
        }
    }
}
