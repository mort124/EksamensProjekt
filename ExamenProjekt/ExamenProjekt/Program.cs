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

            bool run = true;
                    Console.WriteLine("\n For at skrive stoffet skal du huske disse regler:\n Du behøver ikke skrive CH men bare c  \n Dobbelt bindinger skrives med =" +
                    "\n Trippel bindinger skrives med # \n Sidekæder skrives inde i () \n Hvis dit stof er aromatisk skrives det med lille C " +
                    "\n HVis stoffer er en ring skrives det c1c*n1 f.eks: c1ccc1 ");
            while (run)
            {


                    Console.WriteLine("Skriv stof her:"); //takes input from the user
                string molecule = Console.ReadLine();

                if (molecule != "exit")//exit shuts down the program
                {
                    try
                    {
                        string replacedM = molecule.Replace("Br", "L").Replace("Cl", "R").Replace("OOH", "Y").Replace("OH", "T").Replace("OO", "U");
                        string mainChain = replacedM;
                        Alkane alkane1 = new Alkane(replacedM);
                        Console.WriteLine("\n");
                        Console.WriteLine(alkane1.GenerateName());
                    }
                    catch
                    {
                        Console.WriteLine("Ugyldigt input, prøv igen");
                    }
                }
                else
                {
                    run = false;
                }
            }
        }


        private static bool Validate(string input)
        {
            bool accept = false;

            int StartParren = 0;
            int EndParren = 0;

            foreach (var item in input)//checks the amount of parentheses 
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
            foreach (var item in input)//checks the position of parentheses
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


 
    }
}
