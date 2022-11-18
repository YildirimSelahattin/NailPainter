using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;
using static DataLists;
//THE ONLY DATA READER , READS FROM JSONTEXT
public class GameDataManager : MonoBehaviour
{
    [SerializeField] TextAsset JSONText;
    public GameObject[] objectPrefabList;
    public GameObject[] ringArray;
    public GameObject[] braceletArray;
    public GeneralDataStructure[][] objectsByIndexArray;
    public static GameDataManager Instance;
    public DataLists dataLists;
    public static string directory = "/SaveData/";
    public static string fileName = "JSONText";
    string dir;
    public int nextOffset;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //if it is first time playing this game, delete and write default values to the json file 
            dir = Application.persistentDataPath + directory;
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            if (PlayerPrefs.GetString("unity.player_session_count") == "1")
            {
                File.Delete(dir + fileName);
            }
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

    private void OnDisable()
    {
        WriteToJson();
    }

    private void ReadFromJson()
    {
        string fullPath = dir + fileName;
        //if there is no file, use the default values 
        if (!File.Exists(fullPath))
        {
            File.WriteAllText(fullPath, JSONText.text);
            dataLists = JsonUtility.FromJson<DataLists>(JSONText.text);
        }
        //if there is file to read 
        else
        {
            dataLists = JsonUtility.FromJson<DataLists>(File.ReadAllText(fullPath));
        }
    }

    private void WriteToJson()
    {
        string serializedData = JsonUtility.ToJson(dataLists);
        File.WriteAllText(dir + fileName, serializedData);
    }

    public void AddUpgradeToStack()
    {
        //add this change to stacked changes and next upgrade index, also control if upgrades are finished in that theme, increase theme and reset nextUpgrade variable
        dataLists.freeUpgradesLeft++;
        Debug.Log("gel");
    }

    public GameObject GetUpgradableObject()
    {
        int nextUpgradeIndex = (dataLists.room.nextUpgradeIndex + dataLists.freeUpgradesLeft) % 12;
        GameObject upgradableObject = objectPrefabList[nextUpgradeIndex * 5 + (dataLists.room.currentRoomIndexes[nextUpgradeIndex] + 1)];
        upgradableObject.transform.localScale = new Vector3(0.12f, 0.12f, 0.12f);
        if (nextUpgradeIndex > 2)
        {
            upgradableObject.transform.localScale = new Vector3(0.40f, 0.40f, 0.40f);
        }
        foreach (Transform child in upgradableObject.transform)
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
        return ring;
    }

    public GameObject GetGiftBracelet()
    {
        //dataLists.room.generalThemeIndex
        GameObject bracelet = braceletArray[0];
        foreach (Transform child in bracelet.transform)
        {
            child.gameObject.layer = LayerMask.NameToLayer("UI");
        }
        return bracelet;
    }

    public void JSONSifirla()
    {
        File.Delete(dir + fileName);
        dataLists = JsonUtility.FromJson<DataLists>(JSONText.text);
    }
}