using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamenProjekt
{
    class Alkyl
    {
        private static string smileAlkyl;
        private static int parentIndex;

        public int GetParentIndex { get => parentIndex; }
        public string GetSmileChain { get => smileAlkyl; }

        private string smileChain;
        private string name;
        private bool isSideChain;
        private int parentIndex;
        private List<Alkyl> sideChainList;
        private List<Anomalies> anomalyList;


        public bool GetIsSide { get => isSideChain; }
        public int GetParentIndex { get => parentIndex; }
        public string GetSmileChain { get => smileChain; }
        public string GetName { get => name; }
        public List<Alkyl> GetSideList { get => sideChainList; }
        public List<Anomalies> GetAnomalyList { get => anomalyList; }
        public void AddSideChain(Alkyl sideChain)
        {
            sideChainList.Add(sideChain);
            sideChainList.Sort(Alkane.comparisonAlkyl);
        }
        public void AddAnomaly(Anomalies anomaly)
        {
            anomalyList.Add(anomaly);
            anomalyList.Sort(Alkane.comparisonAnomaly);
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
            anomalyList = new List<Anomalies>();
            isSideChain = false;
        }

        public Alkyl(string chainName, int indexOnParent)
        {
            smileChain = chainName;
            name = AlkaneName(chainName, true);
            sideChainList = new List<Alkyl>();
            anomalyList = new List<Anomalies>();
            parentIndex = indexOnParent;
            isSideChain = true;
        }



    }
}
