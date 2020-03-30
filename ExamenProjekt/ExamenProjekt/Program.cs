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
                    if (Validate(molecule))
                    {
                        string replacedM = molecule;
                        replacedM = replacedM.Replace("Br", "L").Replace("Cl", "K");

                        string mainChain = MainChain(replacedM);
                        int carbonCount = ChainCount(mainChain);
                        string alkane = AlkaneName(carbonCount);
                        string elements = Convert.ToString(Elements(replacedM));
                        Console.WriteLine(alkane);

                        List<Tuple<string,int>> sideChainList = SideChains(molecule);


                        List<string> chainNames = new List<string>();
                        foreach (var item in sideChainList)
                        {
                            if (!chainNames.Contains(item.Item1))
                            {
                                chainNames.Add(item.Item1);
                            }
                        }
                        chainNames.Sort();
                        foreach (var item in chainNames)
                        {
                            int chainAmount = 0;
                            foreach (var sideChain in sideChainList)
                            {
                                if (item == sideChain.Item1)
                                {
                                    chainAmount++;
                                    Console.Write(sideChain.Item2 + ",");
                                }
                            }
                            Console.Write("\b-");
                            

                        }

                    }
                }
                else
                {
                    run = false;
                }
            }
        }

        private static string NumToPre(int amount)
        {
            string output = "";
            switch (amount)
            {
                case 1:
                    break;
                case 2:
                    output = "di";
                    break;
                case 3:
                    output = "tri";
                    break;
                case 4:
                    output = "tetra";
                    break;
                case 5:
                    output = "penta";
                    break;
                case 6:
                    output = "hexa";
                    break;
                case 7:
                    output = "septo";
                    break;
                case 8:
                    output = "octo";
                    break;
                case 9:
                    output = "di";
                    break;
                case 10:
                    output = "di";
                    break;
                case 11:
                    output = "di";
                    break;
                case 12:
                    output = "di";
                    break;
            }
            return output;
        }

        private static List<Tuple<string,int>> SideChains(string input)
        {
            List<Tuple<string,int>> sideChainList = new List<Tuple<string,int>>();
            int chainPos = 0;
            string sideChain = "";

            for (int i = 0; i < input.Length; i++)
            {
                if(input[i] == 'c'||input[i] == 'C')
                {
                    chainPos++;//Counts the number of c-atoms up to the start of the side chain
                }
                else if (input[i] == '(')
                {
                    int sideStart = i;
                    int sideEnd = input.IndexOf(')', sideStart);//creates a zero based index from the start parren to the end parren

                    if ( sideEnd - sideStart - 1>0)
                    {
                        sideChain = input.Substring(sideStart + 1, sideEnd - sideStart - 1);/*creates a string in the interval 
                    between sideStart and sideEnd. Adding and subtracting 1 to account for the parentheses 
                    */
                    }
                    
                        string namedChain = AlkaneName(ChainCount(sideChain));
                        namedChain = namedChain.Remove(namedChain.Length - 2, 2);
                        namedChain += "yl";
                        Tuple<string, int> outputChain = new Tuple<string,int>(namedChain,chainPos);
                        sideChainList.Add(outputChain);
                    

                    i = sideEnd;

                    }
            }
            return sideChainList;
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

            int index = input-1; /*The array is indexed in regrad to 0, there for the input-
            value is subtracted with 1*/

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

                    if (sideEnd - sideStart + 1>0)
                    {
                        input = input.Remove(sideStart, sideEnd - sideStart + 1);
                    }
                    i--;
                }
            }
            return input;
        }

        private static bool Validate(string input)
        {
            bool accept = false;

            int StartParren = 0;
            int EndParren = 0;

            foreach (var item in input)//tjækker parrenteser
            {
                if (item == '(')
                {
                    StartParren++;
                }

                if (item == ')')
                {
                    EndParren++;
                }

            }
            if (StartParren == EndParren)
            {
                accept = true;
            }
            else
            {
                Console.WriteLine("Ugyldigt indput: Du mangler pattenteser");
            }

            int StartparrenPos = 0;
            int EndparrenPos = 0;
            foreach (var item in input)//tjækker position af parrenteser
            {
                if (item != '(')
                {
                    StartparrenPos++;
                }
                if (item == '(')
                {
                    StartparrenPos++;
                    break;
                }
            }
            foreach (var item in input)
            {
                if (item != ')')
                {
                    EndparrenPos++;
                }
                if (item == ')')
                {
                    EndparrenPos++;
                    break;
                }
            }
            if (EndparrenPos < StartparrenPos)
            {
                accept = false;
                Console.WriteLine("Ugyldigt input: Dine parrenteser står forkert");
            }
            
            //yeet
            return accept;
        }

        private static List<string> Elements(string input)
        {

            List<string> elements = new List<string>();

            foreach (var item in input)
            {
                switch (item)
                {
                    case 'L':
                        elements.Add("Brom");
                        break;
                    case 'K':
                        elements.Add("Chlor");
                        break;
                    case 'B':
                        elements.Add("Bor");
                        break;
                    case 'N':
                        elements.Add("Nitrogen");
                        break;
                    case 'O':
                        elements.Add("Oxygen");
                        break;
                    case 'P':
                        elements.Add("Fosfor");
                        break;
                    case 'S':
                        elements.Add("Sulfur");
                        break;
                    case 'F':
                        elements.Add("Fluor");
                        break;
                    case 'I':
                        elements.Add("Iod");
                        break;
                    default:
                        break;
                }
            }

            foreach (string element in elements)
            {
                Console.WriteLine(element);
            }

            return elements;
        }
    }
}
