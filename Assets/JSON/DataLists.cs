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
    public GeneralDataStructure[] mirror;
    public GeneralDataStructure[] window;
    public GeneralDataStructure[] flower;
    public GeneralDataStructure[] wall;
    public GeneralDataStructure[] komodin;
    public GeneralDataStructure[] platform;
    public GeneralDataStructure[] picture;
    public GeneralDataStructure[] tabure;
    public GeneralDataStructure[] floor;
   
    public Room room;
    public List<int> stackedChangeParentIndexes;

    // Start is called before the first frame update

}
