using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamenProjekt
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine("Skriv stof her:");
            string molecule = Console.ReadLine();
            Console.WriteLine(molecule);
            
            Console.ReadKey();            
        }
    }
}
