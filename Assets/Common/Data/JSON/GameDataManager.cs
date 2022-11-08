using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using static DataLists;
//THE ONLY DATA READER , READS FROM JSONTEXT
public class GameDataManager : MonoBehaviour
{
    public GameObject[] objectPrefabList;
    public GeneralDataStructure[][] objectsByIndexArray;
    public static GameDataManager Instance;
    public DataLists dataLists;
    // Start is called before the first frame update
    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            ReadFromJson();
            objectsByIndexArray = new GeneralDataStructure[][] {
            GameDataManager.Instance.dataLists.wall,
            GameDataManager.Instance.dataLists.floor,
            GameDataManager.Instance.dataLists.platform,
            GameDataManager.Instance.dataLists.picture,
            GameDataManager.Instance.dataLists.tabure,
            GameDataManager.Instance.dataLists.komodin,
            GameDataManager.Instance.dataLists.sofa,
            GameDataManager.Instance.dataLists.flower,
            GameDataManager.Instance.dataLists.chair,
            GameDataManager.Instance.dataLists.table,
            GameDataManager.Instance.dataLists.mirror};
            DontDestroyOnLoad(this.gameObject);
        }
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnDisable()
    {
        //WriteToJson();
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

    public void AddUpgradeToStack()
    {
        //add this change to stacked changes and next upgrade index, also control if upgrades are finished in that theme, increase theme and reset nextUpgrade variable
        dataLists.stackedChangeParentIndexes.Add(dataLists.room.nextUpgradeIndex);
        dataLists.room.nextUpgradeIndex += 1;
        if (dataLists.room.nextUpgradeIndex==11)
        {
            dataLists.room.nextUpgradeIndex = 0;
            dataLists.room.generalThemeIndex += 1;
        }
    }
    public GameObject GetUpgradableObject()
    {
        return objectPrefabList[dataLists.room.nextUpgradeIndex * (dataLists.room.currentRoomIndexes[dataLists.room.nextUpgradeIndex] + 1)];
    }
}