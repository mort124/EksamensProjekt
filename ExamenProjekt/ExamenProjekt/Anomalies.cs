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

        public Anomalies (char smileValue, int indexOnParent)
        {
            tooth = smileValue;
            parentIndex = indexOnParent;
            name = AnomalyName(smileValue);
        }

        private static string AnomalyName(char toothValue)
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
            anomalies.Add('T',"Ol");
            anomalies.Add('E', "Ether");
            anomalies.Add('Y', "Syre");

            string nameOut = anomalies[toothValue].ToString();
            return nameOut;
        }
    }
}
