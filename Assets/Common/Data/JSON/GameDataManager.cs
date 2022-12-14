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
    public int upgradeAmountInSession = 0;
    public int currentRingIndex;
    public int currentBraceletIndex;
    public int playSound;
    public int playMusic;
    public AudioClip brushMachineMusic;
    public AudioClip asetonSound;
    public AudioClip washMachineSound;
    public AudioClip patternMachineSound;
    public AudioClip manicureMachineSound;
    public AudioClip diamondMachineSound;
    public AudioClip winSound;
    public AudioClip loseSound;
    public AudioClip diamondCollected;
    public AudioClip lastAnimationSound;
    public AudioClip upgradeSound;
    public AudioClip themeFinishedSound;
    public AudioClip mudMachineSound;
    public AudioClip hundredPercentSound;
    public AudioClip kittenBagShakeSound;
    public AudioClip openingBagSound;
    public AudioClip slotSound;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            currentRingIndex = PlayerPrefs.GetInt("CurrentRingIndexKey", 0);
            currentBraceletIndex = PlayerPrefs.GetInt("CurrentBraceletIndexKey", 0);
            playSound = PlayerPrefs.GetInt("PlaySoundKey", 1);
            playMusic = PlayerPrefs.GetInt("PlayMusicKey", 1);
            //if it is first time playing this game, delete and write default values to the json file 
            dir = Application.persistentDataPath + directory;

            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
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
        Debug.Log("save");
        WriteToJson();
        PlayerPrefs.SetInt("CurrentRingIndexKey", currentRingIndex);
        PlayerPrefs.SetInt("CurrentBraceletIndexKey", currentBraceletIndex);
        PlayerPrefs.SetInt("PlaySoundKey", playSound);
        PlayerPrefs.SetInt("PlayMusicKey", playMusic);
    }

    private void ReadFromJson()
    {
        string fullPath = dir + fileName;
        //if there is no file, use the default values 
        if (!File.Exists(fullPath))
        {
            Debug.Log("b�tyle bir dosya yok, olustur");
            File.WriteAllText(fullPath, JSONText.text);
            dataLists = JsonUtility.FromJson<DataLists>(JSONText.text);
        }
        //if there is file to read 
        else
        {
            Debug.Log("b�tyle bir dosya var");
            dataLists = JsonUtility.FromJson<DataLists>(File.ReadAllText(fullPath));
        }
    }

    private void WriteToJson()
    {
        string serializedData = JsonUtility.ToJson(dataLists);
        File.WriteAllText(dir + fileName, serializedData);
    }

    public IEnumerator AddUpgradeToStack()
    {
        yield return new WaitForEndOfFrame();
        UIManager.Instance.rewardPanel.gameObject.SetActive(false);
        UIManager.Instance.earnedRewardPanel.gameObject.SetActive(true);
        //add this change to stacked changes and next upgrade index, also control if upgrades are finished in that theme, increase theme and reset nextUpgrade variable
        dataLists.freeUpgradesLeft++;
        Debug.Log("gel");
    }

    public GameObject GetUpgradableObject()
    {
        int nextUpgradeIndex = (dataLists.room.nextUpgradeIndex + dataLists.freeUpgradesLeft) % 11;
        GameObject upgradableObject = objectPrefabList[nextUpgradeIndex * 5 + (dataLists.room.currentRoomIndexes[nextUpgradeIndex] + 1)];
        upgradableObject.transform.localScale = new Vector3(0.12f, 0.12f, 0.12f);
        if (nextUpgradeIndex > 1)
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
        GameObject ring = ringArray[dataLists.room.generalThemeIndex];
        foreach (Transform child in ring.transform)
        {
            child.gameObject.layer = LayerMask.NameToLayer("UI");
        }
        return ring;
    }

    public GameObject GetGiftBracelet()
    {
        //dataLists.room.generalThemeIndex
        GameObject bracelet = braceletArray[dataLists.room.generalThemeIndex];
        foreach (Transform child in bracelet.transform)
        {
            child.gameObject.layer = LayerMask.NameToLayer("UI");
        }
        return bracelet;
    }

    public void JSONSifirla()
    {
        dataLists = JsonUtility.FromJson<DataLists>(JSONText.text);
        File.Delete(dir + fileName);
        PlayerPrefs.DeleteAll();

    }
}