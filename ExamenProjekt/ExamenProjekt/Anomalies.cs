using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamenProjekt
{
    class Anomalies //hej
    {
        private string name;
        private char tooth;
        private int parentIndex;

        public char GetTooth { get => tooth; }
        public int GetParentIndex { get => parentIndex; }
        public string GetName { get => name; }

        #region
        public Anomalies (char smileValue, int indexOnParent)
        {
            tooth = smileValue;
            parentIndex = indexOnParent;
            name = Alkane.AnomalyName(smileValue);
        }
        #endregion
    }
}
