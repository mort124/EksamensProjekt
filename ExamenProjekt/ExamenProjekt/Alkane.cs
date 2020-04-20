using System.Collections.Generic;
using System.Text;

namespace ExamenProjekt
{
    class Alkane
    {
        private static string smileChain;
        private static bool isSideChain;
        private static int parentIndex;
        private static List<Alkane> sideChainList;

        public bool GetIsSide { get => isSideChain; }
        public int GetParentIndex { get => parentIndex; }
        public string GetSmileChain { get => smileChain; }
        public List<Alkane> GetSideList { get => sideChainList; }


        public static void ExtractSideChains()
        {
            int chainPos = 0;
            Alkane sideChain;

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
                        sideChain = new Alkane(smileChain.Substring(sideStart + 1, sideEnd - sideStart - 1), chainPos);/*creates a string in the interval 
                    between sideStart and sideEnd. Adding and subtracting 1 to account for the parentheses 
                    */
                        sideChainList.Add(sideChain);
                    }


                    i = sideEnd;
                }
            }
            sideChainList.Sort();
        }

        public static string ExtractMain()
        {
            string output = smileChain;
            for (int i = 0; i < smileChain.Length; i++)
            {
                if (smileChain[i] == '(')
                {
                    int sideStart = i;
                    int sideEnd = smileChain.IndexOf(')', sideStart);

                    if (sideEnd - sideStart + 1 > 0)
                    {
                        output = smileChain.Remove(sideStart, sideEnd - sideStart + 1);
                    }
                    i--;
                }
            }
            return output;
        }

        public override string ToString()
        {
            return AlkaneName(ExtractMain());
        }

            StringBuilder sb = new StringBuilder();
        public string GenerateName()
        {
            if (sideChainList.Count > 1)
            {
                foreach (var item in sideChainList)
                {
                    sb.Append(item.GenerateName());
                }
            }

            if (isSideChain)
            {
                sb.Append(parentIndex + "-" + AlkaneName(ExtractMain()));
            }

            else
            {
            sb.Append(AlkaneName(ExtractMain()));

            }
            return sb.ToString();
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

        private static string AlkaneName(string alkane)//Has a list of aklane names  
        {
            string output;
            int chainLength = 0;

            foreach (var item in alkane)
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

            if (isSideChain)
            {
                output = output.Remove(output.Length - 2, 2);
                output += "yl";
            }

            return output;
        }

        #region constructs
        public Alkane(string alkaneChain)
        {
            isSideChain = false;
            smileChain = alkaneChain;
            sideChainList = new List<Alkane>();
            ExtractSideChains();
        }
        private Alkane(string alkaneChain, int indexOnParent)
        {
            smileChain = alkaneChain;
            isSideChain = true;
            parentIndex = indexOnParent;
            sideChainList = new List<Alkane>();
            ExtractSideChains();
        }
        #endregion
    }
}
