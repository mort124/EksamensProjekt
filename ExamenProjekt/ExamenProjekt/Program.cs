﻿using System;
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
            string thor = "neger";
            string anker = "gay";

            Console.WriteLine(thor);
            Console.WriteLine(anker);
            Console.WriteLine(GetAbe());

            Console.ReadKey();

            
        }
        public static string GetAbe()
        {
            string output = "Abe";
            return output;
        }
    }
}
