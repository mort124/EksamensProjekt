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


                    Console.WriteLine("Skriv stof her:"); //tager input fra brugeren
                string molecule = Console.ReadLine();

                if (molecule != "exit")//exit lukker programmet
                {
                    try
                    {
                        Alkane alkane = new Alkane(molecule);
                        Console.WriteLine(alkane.GenerateName());
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


 
    }
}
