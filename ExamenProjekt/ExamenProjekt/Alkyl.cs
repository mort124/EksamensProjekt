using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamenProjekt
{
    class Alkyl
    {

        private string smileChain;
        private string name;
        private bool isSideChain;
        private int parentIndex;
        private List<Alkyl> sideChainList;
        private static int CompareAlkyl(Alkyl a1, Alkyl a2)
        {
            string name1 = a1.name;
            string name2 = a2.name;
            return name1.CompareTo(name2);
        }
        Comparison<Alkyl> comparison = new Comparison<Alkyl>(CompareAlkyl);

        public bool GetIsSide { get => isSideChain; }
        public int GetParentIndex { get => parentIndex; }
        public string GetSmileChain { get => smileChain; }
        public string GetName { get => name; }
        public List<Alkyl> GetSideList { get => sideChainList; }
        public void AddSideChain(Alkyl sideChain)
        {
            sideChainList.Add(sideChain);
            sideChainList.Sort(comparison);
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

        public override string ToString()
        {
            return name;
        }

        public Alkyl(string chainName)
        {

            smileChain = chainName;
            name = AlkaneName(chainName, false);
            sideChainList = new List<Alkyl>();
            isSideChain = false;
        }

        public Alkyl(string chainName, int indexOnParent)
        {
            smileChain = chainName;
            name = AlkaneName(chainName, true);
            sideChainList = new List<Alkyl>();
            parentIndex = indexOnParent;
            isSideChain = true;
        }



    }
}
