using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
//THE ONLY DATA READER , READS FROM JSONTEXT
public class GameDataManager : MonoBehaviour
{
    
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
}
