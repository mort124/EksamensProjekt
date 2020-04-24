using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
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
                    Console.WriteLine("\n For at skrive stoffet skal du huske disse regler:\n Du behøver ikke skrive CH men bare c  \n" +
                    "\n Sidekæder skrives inde i () \n" + "\n Hvis stoffer er en ring skrives det c1c*n1 f.eks: c1ccc1 ");
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
                        string name = alkane1.GenerateName();
                        using (StreamWriter file = new StreamWriter(@"ouput.txt",true))
                        {
                            file.WriteLine(name.Replace(",\b","").Replace("-\b",""));
                        }
                        Console.WriteLine(name + "\n");
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



 
    }
}
