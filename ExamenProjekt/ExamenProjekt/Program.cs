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
            bool run = true;
            int carbonCount;
            while (run)
            {
                Console.WriteLine("Skriv stof her:");
                string molecule = Console.ReadLine();
                if (molecule != "exit")
                {
                    carbonCount = 0;
                    foreach (var item in molecule)
                    {
                        if(item == 'C'|| item == 'c')
                        {
                            carbonCount++;
                        }
                    }
                    Console.WriteLine(carbonCount.ToString());
                }
                else
                {
                    run = false;
                }
            }
        }
    }
}
