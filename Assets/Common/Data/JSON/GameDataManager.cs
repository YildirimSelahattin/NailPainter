using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
//THE ONLY DATA READER , READS FROM JSONTEXT
public class GameDataManager : MonoBehaviour
{
    public GameObject[] objectPrefabList;
    public static GameDataManager Instance;
    public DataLists dataLists;
    // Start is called before the first frame update
    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            ReadFromJson();
            DontDestroyOnLoad(this.gameObject);
        }
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnDisable()
    {
        WriteToJson();
    }
    private void ReadFromJson()
    {
        dataLists = JsonUtility.FromJson<DataLists>(File.ReadAllText(Application.dataPath + "/Common/Data/JSON/JSONText.txt"));
    }

    private void WriteToJson()
    {
        string serializedData = JsonUtility.ToJson(dataLists);
        File.WriteAllText(Application.dataPath + "/Common/Data/JSON/JSONText.txt", serializedData);
    }

    public GameObject GetNextUpgrade()

    { 
        //increase current room index and next upgrade index, also control if upgrades are finished in that theme, increase theme and reset nextUpgrade variable
        dataLists.room.currentRoomIndexes[dataLists.room.nextUpgradeIndex]+=1;
        dataLists.room.nextUpgradeIndex += 1;
        if (dataLists.room.nextUpgradeIndex==11)
        {
            dataLists.room.nextUpgradeIndex = 0;
            dataLists.room.generalThemeIndex += 1;
        }
        return objectPrefabList[(dataLists.room.nextUpgradeIndex+1)*dataLists.room.currentRoomIndexes[dataLists.room.nextUpgradeIndex]];

    }
}
