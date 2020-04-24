using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ExamenProjekt
{
    class Alkane //hej
    {
        private static string smileChain;
        private static Alkyl a;

        private static int CompareAlkyl(Alkyl a1, Alkyl a2)//compares the sidechains to sort them alphabeticaly
        {
            string name1 = a1.GetName;
            string name2 = a2.GetName;
            return name1.CompareTo(name2);
        }
        private static int CompareAnomaly(Anomalies a1, Anomalies a2)//compares the anomalies to sort them alphabeticaly
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
                    */
                        input.AddSideChain(sideChain);
                    }


                    i = sideEnd;
                }

            }
            foreach (var item in input.GetSideList)
            {
                ExtrackAnomalies(item);
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

        public string GenerateName()//genereates the name 
        {
            List<string> chainNames = new List<string>();
            StringBuilder sb = new StringBuilder();
            foreach (var item in a.GetSideList)
            {
                if (!chainNames.Contains(item.GetName))//checks if the sidechain is allready on the list
                {
                    chainNames.Add(item.GetName);
                }
            }
            foreach (var item in a.GetAnomalyList)
            {
                if (!chainNames.Contains(item.GetName))//checks if the anomaly is allready on the list
                {
                    chainNames.Add(item.GetName);
                }
            }
            chainNames.Sort();//sorts the list alphabetically
            foreach (var item in chainNames)
            {
                int chainAmount = 0;
                foreach (var sideChain in a.GetSideList)
                {
                    if (item == sideChain.GetName)//finds out how many times the sidechain has occured and ads the sidechain to the string with a ","
                    {
                        chainAmount++;
                        sb.Append(sideChain.GetParentIndex + ",");//findes index that sidechain starts on
                    }
                }
                foreach (var anomaly in a.GetAnomalyList)
                {
                    if (item == anomaly.GetName)//finds out how many times the anomaly has occured and ads the anomaly to the string with a ","
                    {
                        chainAmount++;
                        sb.Append(anomaly.GetParentIndex + ",");//findes index that sidechain starts on
                    }
                }
                sb.Append("\b-");//there is to many "," and replaces the last one with "-"
                sb.Append(NumToPre(chainAmount) + item + "-");//Adds the sidechain or anomality with its position and a prefix to tell how many there is and a "-"
            }
            sb.Append("\b" + a.GetName + " ny\n");//Adds the mainchain name 


            return sb.ToString();
        }

        public static string ExtractMain(string input)
        {
            string output = input;
            for (int i = 0; i < output.Length; i++)
            {
                if (output[i] == '(')// findes the position to the start of the sidechain
                {
                    int sideStart = i;
                    int sideEnd = output.IndexOf(')', sideStart);// finds the position to the end of the sidechain

                    if (sideEnd - sideStart + 1 > 0)
                    {
                        output = output.Remove(sideStart, sideEnd - sideStart + 1);// removes the sidechain by removing all the signs within the length of 
                                                                                   // the sidechain and adds one to account for the last parrenthatheses

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
        public static string AlkaneName(string smile, bool isAlkyl)//Has a list of aklane names  
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

        public static string AnomalyName(char toothValue)
        {
            Hashtable anomalies = new Hashtable();
            anomalies.Add('K', "kalium");
            anomalies.Add('L', "Brom");
            anomalies.Add('R', "Chlor");
            anomalies.Add('B', "Bor");
            anomalies.Add('N', "Amin");
            anomalies.Add('P', "Fosfor");
            anomalies.Add('S', "Sulfur");
            anomalies.Add('F', "Flour");
            anomalies.Add('I', "Iod");
            anomalies.Add('O', "Oxygen");
            anomalies.Add('T', "Ol");
            anomalies.Add('Y', "Syre");
            string nameOut = anomalies[toothValue].ToString();
            return nameOut;
        }

        #region constructs
        public Alkane(string alkaneChain)
        {
            smileChain = alkaneChain;
            a = new Alkyl(ExtractMain(alkaneChain));
            ExtractSideChains(a);
            ExtrackAnomalies(a);

        }
        #endregion
    }
}
