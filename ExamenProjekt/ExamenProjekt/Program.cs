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
            while (run)
            {
                Console.WriteLine("Skriv stof her:");
                string molecule = Console.ReadLine();
                if (molecule != "exit")
                {
                    int carbonCount = 0;
                    carbonCount = MainChain(molecule);
                    Console.WriteLine(carbonCount.ToString());
                }
                else
                {
                    run = false;
                }
            }
        }
        private static int MainChain(string input)
        {
            int output = 0;

                    foreach (var item in input)
                    {
                        if(item == 'C'|| item == 'c')
                        {
                            output++;
                        }
                    }
            return output;
        }

    }
}
