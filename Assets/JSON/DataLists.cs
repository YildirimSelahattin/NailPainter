using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameDataManager;

[System.Serializable]
public class DataLists 
{
    [System.Serializable]
    public class GeneralDataStructure
    {
        public string name;
        public int price;
        public int power;
        public int isbuyed;
    }
    public GeneralDataStructure[] table;
    public GeneralDataStructure[] chair;
    public GeneralDataStructure[] sofa;
    public Room room;

    // Start is called before the first frame update

}
