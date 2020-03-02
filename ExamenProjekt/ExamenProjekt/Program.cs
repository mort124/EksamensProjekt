using System;
using System.Collections;
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

                    Hashtable sideChainTable = SideChains(molecule);

                    foreach (int item in sideChainTable.Keys)
                    {
                        Console.Write(item + " ");
                        Console.WriteLine(sideChainTable[item]);
                    }
                }
                else
                {
                    run = false;
                }
            }
        }
        private static Hashtable SideChains(string input)
        {
            string[] output;
            Hashtable sideChainList = new Hashtable();
            int chainPos = 0;

            for (int i = 0; i < input.Length; i++)
            {
                if(input[i] == 'c'||input[i] == 'C')
                {
                    chainPos++;
                }
                else if (input[i] == '(')
                {
                    int sideStart = i;
                    int sideEnd = input.IndexOf(')', sideStart);

                    string sideChain = input.Substring(sideStart + 1, sideEnd - sideStart-1);

                    sideChainList.Add(chainPos, sideChain);
                    i = sideEnd;
                }
            }
            return sideChainList;
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
