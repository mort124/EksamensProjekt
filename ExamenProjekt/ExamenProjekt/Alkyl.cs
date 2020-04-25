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


        public override string ToString()
        {
            return name;
        }

        #region construct
        public Alkyl(string chainName) //Creates main chain Alkyl when no index on parent is provided
        {
            smileChain = chainName;
            name = Alkane.AlkaneName(chainName, false);
            sideChainList = new List<Alkyl>();
            anomalyList = new List<Anomalies>();
            isSideChain = false;
        }

        public Alkyl(string chainName, int indexOnParent) //creates side chain alkyl when index on parent is provided
        {
            smileChain = chainName;
            name = Alkane.AlkaneName(chainName, true);
            sideChainList = new List<Alkyl>();
            anomalyList = new List<Anomalies>();
            parentIndex = indexOnParent;
            isSideChain = true;
        }
        #endregion
    }
}
