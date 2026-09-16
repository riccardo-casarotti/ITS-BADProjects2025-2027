
namespace FileCSV_Scrittura
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("File CSV-Scrittura");

            Persona p = new Persona();

            Console.Write("Nome: ");
            p.Nome = Console.ReadLine();

            Console.Write("Cognome: ");
            p.Cognome = Console.ReadLine();

            Console.Write("Data di Nascita [gg/mm/aaaa]: ");
            p.DataDiNascita = DateTime.Parse(Console.ReadLine()); //[gg/mm/aaaa]

            Console.Write("Luogo di Nascita: ");
            p.LuogoNascita = Console.ReadLine();

            Console.Write("Sesso [0=F, 1=M]: ");
            p.Sesso = (TipoSesso)int.Parse(Console.ReadLine());

            string path = @"..\..\..\File\Persone.csv";

            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.Write(p.FormatCSV()); // non devo fare il closing etc.. perchè uso using 
            }

            Console.WriteLine("Operazione avvenuta con successo");
        }
    }
}
