using System;
using System.Collections.Generic;
using System.Text;

namespace ExamenProjekt
{
    class Alkane //hej
    {
        private static string smileChain;
        private static Alkyl a;
        private static int CompareAlkyl(Alkyl a1, Alkyl a2)
        {
            string name1 = a1.GetName;
            string name2 = a2.GetName;
            return name1.CompareTo(name2);
        }
        private static int CompareAnomaly(Anomalies a1, Anomalies a2)
        {
            string name1 = a1.GetName;
            string name2 = a2.GetName;
            return name1.CompareTo(name2);
        }

        public static Comparison<Alkyl> comparisonAlkyl = new Comparison<Alkyl>(CompareAlkyl);
        public static Comparison<Anomalies> comparisonAnomaly = new Comparison<Anomalies>(CompareAnomaly);
        public string GetSmileChain { get => smileChain; }


        public static void ExtractSideChains(Alkyl input)
        {
            int chainPos = 0;
            Alkyl sideChain;

            for (int i = 0; i < smileChain.Length; i++)
            {
                if (smileChain[i] == 'c' || smileChain[i] == 'C')
                {
                    chainPos++;//Counts the number of c-atoms up to the start of the side chain
                }
                else if (smileChain[i] == '(')
                {
                    int sideStart = i;
                    int sideEnd = smileChain.IndexOf(')', sideStart);//creates a zero based index from the start parren to the end parren

                    if (sideEnd - sideStart - 1 > 0)
                    {
                        sideChain = new Alkyl(smileChain.Substring(sideStart + 1, sideEnd - sideStart - 1), chainPos);/*creates a string in the interval 
                    between sideStart and sideEnd. Adding and subtracting 1 to account for the parentheses 
                    */ input.AddSideChain(sideChain);
                    }


                    i = sideEnd;
                }

            }
        }
        public static void ExtrackAnomalies(Alkyl input)
        {
            int index = 0;
            foreach (var item in input.GetSmileChain)
            {
                if(item != 'c' && item != 'C')
                {
                    Anomalies anomaly = new Anomalies(item, index);
                    input.AddAnomaly(anomaly);
                }
                else
                {
                    index++;
                }
            }
        }

        public void PrintChains()
        {
            System.Console.WriteLine(a.GetName);

            foreach (Alkyl item in a.GetSideList)
            {
                System.Console.WriteLine(item.GetName);
            }
            foreach (var item in a.GetAnomalyList)
            {
                System.Console.WriteLine(item.GetName);
            }
        }

        public string GenerateName()
        {
            List<string> chainNames = new List<string>();
            StringBuilder sb = new StringBuilder();
            foreach (var item in a.GetSideList)
            {
                if (!chainNames.Contains(item.GetName))
                {
                    chainNames.Add(item.GetName);
                }
            }
            foreach (var item in a.GetAnomalyList)
            {
                if (!chainNames.Contains(item.GetName))
                {
                    chainNames.Add(item.GetName);
                }
            }
            chainNames.Sort();
            foreach (var item in chainNames)
            {
                int chainAmount = 0;
                foreach (var sideChain in a.GetSideList)
                {
                    if (item == sideChain.GetName)
                    {
                        chainAmount++;
                        sb.Append(sideChain.GetParentIndex + ",");
                    }
                }
                foreach (var anomaly in a.GetAnomalyList)
                {
                    if (item == anomaly.GetName)
                    {
                        chainAmount++;
                        sb.Append(anomaly.GetParentIndex + ",");
                    }
                }
                sb.Append("\b-");
                sb.Append(NumToPre(chainAmount) + item + "-");
            }
            sb.Append("\b" + a.GetName + " ny\n");

            return sb.ToString();
        }

        public static string ExtractMain(string input)
        {
            string output = input;
            for (int i = 0; i < output.Length; i++)
            {
                if (output[i] == '(')
                {
                    int sideStart = i;
                    int sideEnd = smileChain.IndexOf(')', sideStart);

                    if (sideEnd - sideStart + 1 > 0)
                    {
                        output = output.Remove(sideStart, sideEnd - sideStart + 1);
                    }
                    i--;
                }
            }
            return output;
        }

        public override string ToString()
        {
            return smileChain;
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


        #region constructs
        public Alkane(string alkaneChain)
        {
            smileChain = alkaneChain;
            a = new Alkyl(ExtractMain(alkaneChain));
            ExtractSideChains(a);
            ExtrackAnomalies(a);
        }

        private static string CycloFinder(Alkyl input)
        {

            string smileChain = input.GetSmileChain;
            string output = "";
            int end = smileChain.Length - 1;
            bool accept1 = false;
            bool accept2 = false;

            if (smileChain.Length > 3)
            {
                accept2 = true;
            }

            if (accept2)
            {

                if (smileChain[1] == '1' && smileChain[end] == '1')
                {
                    accept1 = true;
                }
                if (smileChain[1] != '1' && smileChain[end] == '1')
                {
                    output = "Du mangler start tal";
                }
                if (smileChain[1] == '1' && smileChain[end] != '1')
                {
                    output = "Du mangler slut tal";
                }
            }
            //---------------------------------------------


            if (accept1)
            {
                if (smileChain == "C1CCCCC1")
                {
                    output = "Benzen";
                }
                else
                {
                    output = "cyclo" + AlkaneName(smileChain,input.GetIsSide);
                }
            }

            return output;
        }
        private static string AlkaneName(string smile, bool isAlkyl)//Has a list of aklane names  
        {
            string output;
            int chainLength = 0;

            foreach (var item in smile)
            {
                if (item == 'c' || item == 'C')
                {
                    chainLength++;
                }
            }

            int index = chainLength - 1; /*The array is indexed in regrad to 0, there for the input-
            value is subtracted with 1*/

            string[] AlkaneList = new string[]
            {
                "metan", "ethan", "propan", "butan", "pentan", "hexan","heptan","octan","nonan","decan","undecan","dodecan"
            };

            output = AlkaneList[index];

            if (isAlkyl)
            {
                output = output.Remove(output.Length - 2, 2);
                output += "yl";
            }

            return output;
        }

        private static List<string> CharacteristicGroups(List<string> input)
        {
            List<string> Groups = new List<string>();


            foreach (var item in input)
            {
                if (item == "O" && item + 1 == "H")
                {
                    Groups.Add("ol");
                }
                if (item == "C" && item + 1 == "O" && item + 2 == "O" && item + 3 == "H")
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

        private static string Bindings(string input)
        {
            string output = "";

            foreach (var item in input)
            {


            }


            return output;
        }

        #endregion
    }
}
