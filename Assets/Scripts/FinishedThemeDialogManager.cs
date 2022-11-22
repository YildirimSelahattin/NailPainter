using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishedThemeDialogManager : MonoBehaviour
{
    [SerializeField] GameObject braceletParent;
    [SerializeField] GameObject ringSpawnParent;
    GameObject ring;
    GameObject bracelet;
    // Start is called before the first frame update

    void Start()
    {
        braceletParent.transform.GetChild(GameDataManager.Instance.dataLists.room.generalThemeIndex-1).gameObject.SetActive(true);
        ringSpawnParent.transform.GetChild(GameDataManager.Instance.dataLists.room.generalThemeIndex-1).gameObject.SetActive(true);


    }

    private void OnDisable()
    {
      
        braceletParent.transform.GetChild(GameDataManager.Instance.dataLists.room.generalThemeIndex - 1).gameObject.SetActive(false);
        ringSpawnParent.transform.GetChild(GameDataManager.Instance.dataLists.room.generalThemeIndex - 1).gameObject.SetActive(false);
    }
}
