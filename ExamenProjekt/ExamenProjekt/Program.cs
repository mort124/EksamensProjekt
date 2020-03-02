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
                    string mainChain = MainChain(molecule);
                    int carbonCount = ChainCount(mainChain);
                    string alkane = AlkaneName(carbonCount);
                    Console.WriteLine(alkane);

                    Hashtable sideChainTable = SideChains(molecule);

                    foreach (int item in sideChainTable.Keys)
                    {
                        Console.Write(item + "-");
                        Console.WriteLine(AlkaneName(ChainCount(sideChainTable[item].ToString())));
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
            Hashtable sideChainTable = new Hashtable();
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

                    sideChainTable.Add(chainPos, sideChain);
                    i = sideEnd;
                }
            }
            return sideChainTable;
        }
        private static int ChainCount(string input)
        {

            int output = 0;//The amount of times there has been spottet a c in the string

            foreach (var item in input)
            {
                if (item == 'C' || item == 'c') //The molecule string is chekked for c's 
                {
                    output++; 
                }
            }
            return output;
        }

        private static string AlkaneName(int input)//Has a list of aklane names  
        {
            string output;

            int index = input-1; //The array is indexed in regrad to 0, there for the input-
            //value is subtrected with 1

            string[] AlkaneList = new string[]
            {
                "metan", "ethan", "propan", "butan", "pentan", "hexan","heptan","octan","nonan", "decan","undecan", "dodecan"
            };

            output = AlkaneList[index];

            return output;
        }

        private static string MainChain(string input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == '(')
                {
                    int sideStart = i;
                    int sideEnd = input.IndexOf(')', sideStart);

                    input = input.Remove(sideStart, sideEnd - sideStart + 1);
                }
            }
            return input;
        }

    }
}
