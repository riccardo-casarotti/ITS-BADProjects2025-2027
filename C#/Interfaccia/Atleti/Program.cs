using System.Security.Cryptography;

namespace Atleti
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Atleti");

            Atleta a1 = new Atleta
            {
                Nome = "Riccardo",
                Cognome = "Casarotti",
                Disciplina = "Salto Triplo",
                NumeroPettorina = 12
            };


            Console.WriteLine(a1);
            //Console.WriteLine(a1.Dorso());
            Console.WriteLine(a1.Corro());

            
            

            Calciatore c2 = new Calciatore
            {
                Nome = "Francesco",
                Cognome = "Totti",
                Disciplina = "Calcio",
                NumeroPettorina = 10,
                PartiteGiocate = 786,
                GoalSegnati = 307
            };
            Console.WriteLine(c2);
            
            Calciatore c3 = new Calciatore
                        {
                            Nome = "Samuel",
                            Cognome = "Eto'o",
                            Disciplina = "Calcio",
                            NumeroPettorina = 9,
                            PartiteGiocate = 846,
                            GoalSegnati = 427
                        };
                        Console.WriteLine(c3);
            Calciatore c4 = (Calciatore)c2.Clone();
            //Calciatore c5 = c2.Clone as Calciatore 

            if (c3.CompareTo(c2) == 1)
                Console.WriteLine($"Vincitore: {c2}\nPerdente{c3}");
            else if (c3.CompareTo(c2.) == -1)
                Console.WriteLine($"Vincitore: {c3}\nPerdente{c2}");

        }
    }
}
