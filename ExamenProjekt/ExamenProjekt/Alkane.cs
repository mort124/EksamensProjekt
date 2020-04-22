using System;
using System.Collections.Generic;
using System.Text;

namespace ExamenProjekt
{
    class Alkane
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


        public static void ExtractSideChains()
        {
            int chainPos = 0;
            Alkyl sideChain;
            Anomalies anomaly;

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
                        a.AddSideChain(sideChain);
                    }


                    i = sideEnd;
                }
                else
                {
                    anomaly = new Anomalies(smileChain[i], chainPos);
                    a.AddAnomaly(anomaly);
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
            ExtractSideChains();
        }

        #endregion
    }
}
