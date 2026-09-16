namespace FileTesto_Scrittura
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("FileTesto-Scrittura");

            string path = @"C:\file\frase.txt"; 

            Console.Write("Inserisci una frase: ");
            string frase = Console.ReadLine();

            //accesso al file di testo in modalità scrittura
            StreamWriter sw = new StreamWriter(path);
            
            sw.Write(frase);// scrivo file

            sw.Close();// salvo tutto e chiudo accesso 

            Console.WriteLine("Operazione eseguita con successo");
        }
    }
}
