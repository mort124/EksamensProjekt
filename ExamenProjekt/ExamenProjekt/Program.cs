using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamenProjekt
{
    class Program //Hej
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Green;//!!!very important!!!

            bool viewed = false;
            bool run = true;
            while (run)
            {
                if (viewed == false)
                {

                    Console.WriteLine("\n For at skrive stoffet skal du huske disse regler:\n Du behøver ikke skrive CH men bare c  \n Dobbelt bindinger skrives med =" +
                    "\n Trippel bindinger skrives med # \n Sidekæder skrives inde i () \n Hvis dit stof er aromatisk skrives det med lille C " +
                    "\n HVis stoffer er en ring skrives det c1c*n1 f.eks: c1ccc1 ");
                    Console.WriteLine("Skriv stof her:"); //tager input fra brugeren

                    viewed = true;
                }

                string molecule = Console.ReadLine();

                if (molecule != "exit")//exit lukker programmet
                {
                    if (Validate(molecule))
                    {
                        string replacedM = molecule;
                        replacedM = replacedM.Replace("Br", "L").Replace("Cl", "R").Replace("OOH", "Y").Replace("OH","T").Replace("OO","E");

                        string mainChain = MainChain(replacedM);
                        Alkane alkane1 = new Alkane(replacedM);
                        alkane1.PrintChains();
                        Console.WriteLine("\n");
                        Console.WriteLine(alkane1.GenerateName());

                        int carbonCount = ChainCount(mainChain);
                        string alkane = AlkaneName(carbonCount);
                        string Cyclo = CycloFinder(replacedM);
                        //List<string> CharGroup = CharacteristicGroups(replacedM);


                        List<Tuple<string, int>> sideChainList = SideChains(molecule);

                        if (Cyclo == "")
                        {
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
                                Console.Write(NumToPre(chainAmount) + item + "-");
                            }
                            Console.WriteLine("\b" + alkane + " Gammel"+"\n");
                        }
                        else
                        {
                            Console.WriteLine(Cyclo);
                        }

                    }
                }
                else
                {
                    run = false;
                }
            }
        }

        private static string NumToPre(int amount) //converts number to standard prefix
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
                    output = "hepta";
                    break;
                case 8:
                    output = "octo";
                    break;
                case 9:
                    output = "nona";
                    break;
                case 10:
                    output = "deca";
                    break;
                case 11:
                    output = "undeca";
                    break;
                case 12:
                    output = "dodeca";
                    break;
            }
            return output;
        }

        private static List<Tuple<string, int>> SideChains(string input)
        {
            List<Tuple<string, int>> sideChainList = new List<Tuple<string, int>>();
            int chainPos = 0;
            string sideChain = "";

            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == 'c' || input[i] == 'C')
                {
                    chainPos++;//Counts the number of c-atoms up to the start of the side chain
                }
                else if (input[i] == '(')
                {
                    int sideStart = i;
                    int sideEnd = input.IndexOf(')', sideStart);//creates a zero based index from the start parren to the end parren

                    if (sideEnd - sideStart - 1 > 0)
                    {
                        sideChain = input.Substring(sideStart + 1, sideEnd - sideStart - 1);/*creates a string in the interval 
                    between sideStart and sideEnd. Adding and subtracting 1 to account for the parentheses 
                    */
                    }

                    string namedChain = AlkaneName(ChainCount(sideChain));
                    namedChain = namedChain.Remove(namedChain.Length - 2, 2);
                    namedChain += "yl";
                    Tuple<string, int> outputChain = new Tuple<string, int>(namedChain, chainPos);
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

                    if (sideEnd - sideStart + 1 > 0)
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

            return accept;
        }

        //private static List<Tuple<string, int>> Elements(string input)
        //{
        //    int chainPos = 0;
        //    List<Tuple<string, int>> ElementList = new List<Tuple<string, int>>();

        //    for (int i = 0; i < input.Length; i++)
        //    {
        //        if (input[i] == 'c' || input[i] == 'C')
        //        {
        //            chainPos++;//Counts the number of c-atoms up to the start of the side chain
        //        }
        //        else
        //        {
        //            string elementName;
        //            switch (input[i])
        //            {
        //                case 'L':
        //                    elementName =  "brom";
        //                    break;
        //                case 'K':
        //                    elementName =  "chlor";
        //                    break;
        //                case 'B':
        //                    elementName =  "bor";
        //                    break;
        //                case 'N':
        //                    elementName =  "nitrogen";
        //                    break;
        //                case 'O':
        //                    elementName =  "oxygen";
        //                    break;
        //                case 'P':
        //                    elementName =  "fosfor";
        //                    break;
        //                case 'S':
        //                    elementName =  "sulfur";
        //                    break;
        //                case 'F':
        //                    elementName =  "flour";
        //                    break;
        //                case 'I':
        //                    elementName =  "iod";
        //                    break;
        //                default:
        //                    elementName =  @"N\A";
        //                    break;
        //            }
        //            Tuple<string, int> element = new Tuple<string, int>(elementName, i);
        //            ElementList.Add(element);
        //        }
        //    }
        //    return ElementList;
        //}
        private static string CycloFinder(string input)
        {
            string output = "";
            int end = input.Length - 1;
            bool accept1 = false;
            bool accept2 = false;

            if (input.Length>3)
            {
                accept2 = true;
            }

            if (accept2)
            {

                if (input[1] == '1' && input[end] == '1')
                {
                    accept1 = true;
                }
                if (input[1] != '1' && input[end] == '1')
                {
                    output = "Du mangler start tal";
                }
                if (input[1] == '1' && input[end] != '1')
                {
                    output = "Du mangler slut tal";
                }
            }
            //---------------------------------------------
           

            if (accept1)
            {
                if (input =="C1CCCCC1")
                {
                    output = "Benzen";
                }
                else
                {
                    output = "cyclo" + AlkaneName(ChainCount(input));
                }
            }

            return output;
        }

        private static List<string> CharacteristicGroups(List<string> input)
        {
            List<string> Groups = new List<string>();


            foreach (var item in input)
            {
                if (item == "O" && item+1 == "H")
                {
                    Groups.Add("ol");
                }
                if (item == "C" && item+1 == "O" && item + 2 == "O" && item + 3 == "H")
                {
                    Groups.Add("Syre");
                }
                if (item == "N" && item + 1 == "H")
                {
                    Groups.Add("amin");
                }
            }
           
            return Groups;
        }

        private static string Bindings (string input)
        {
            string output="";

            foreach (var item in input)
            {


            }
           

            return output;
        }
    }
}
