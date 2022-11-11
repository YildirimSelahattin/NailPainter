using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using static DataLists;
//THE ONLY DATA READER , READS FROM JSONTEXT
public class GameDataManager : MonoBehaviour
{
    public GameObject[] objectPrefabList;
    public GameObject[] ringArray;
    public GameObject[] braceletArray;
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
        Debug.Log("gel");
        if (dataLists.room.nextUpgradeIndex==11)
        {
            dataLists.room.nextUpgradeIndex = 0;
            dataLists.room.generalThemeIndex += 1;
            dataLists.showThemeFinishedPanel = 1;
        }
    }
    public GameObject GetUpgradableObject()
    {
        GameObject upgradableObject = objectPrefabList[(dataLists.room.nextUpgradeIndex * 5) + (dataLists.room.currentRoomIndexes[dataLists.room.nextUpgradeIndex] + 1)];
        upgradableObject.transform.localScale = new Vector3(0.12f,0.12f,0.12f);
        if(dataLists.room.nextUpgradeIndex > 2)
        {
            upgradableObject.transform.localScale = new Vector3(0.40f, 0.40f, 0.40f);
        }
        foreach(Transform child in upgradableObject.transform)
        {
            child.gameObject.layer = LayerMask.NameToLayer("UI");
        }
        return upgradableObject;
    }

    public GameObject GetGiftRing()
    {
        GameObject ring = ringArray[0];
        foreach (Transform child in ring.transform)
        {
            child.gameObject.layer = LayerMask.NameToLayer("UI");
        }
        return ring ;
    }

    public GameObject GetGiftBracelet()
    {
        //dataLists.room.generalThemeIndex
        GameObject bracelet = braceletArray[0];
        foreach (Transform child in bracelet.transform)
        {
            child.gameObject.layer = LayerMask.NameToLayer("UI");
        }
        return bracelet ;
    }
}