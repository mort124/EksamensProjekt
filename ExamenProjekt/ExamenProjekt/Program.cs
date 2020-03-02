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
                    string alkane = AlkaneName(carbonCount);
                    Console.WriteLine(alkane);
                }
                else
                {
                    run = false;
                }
            }
        }
        private static string[] SideChains(string input)
        {
            string[] output;
            input.Split

            return output;
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

        private static string AlkaneName(int input)
        {
            string output;

            int index = input-1;

            string[] AlkaneList = new string[]
            {
                "metan", "ethan", "propan", "butan", "pentan", "hexan","heptan","octan","nonan", "decan","undecan", "dodecan"
            };

            output = AlkaneList[index];

            return output;
        }
    }
}
