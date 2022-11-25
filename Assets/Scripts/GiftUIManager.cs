using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiftUIManager : MonoBehaviour
{
    [SerializeField] GameObject finishedThemeDialog;
    [SerializeField] GameObject tapToCollectWindow;
    [SerializeField] GameObject collectWindow;
    [SerializeField] GameObject braceletParent;
    [SerializeField] GameObject ringSpawnParent;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OnContinueButtonClicked()
    {
        Debug.Log("sa");
        finishedThemeDialog.SetActive(false);
        tapToCollectWindow.SetActive(true);
    }
    public void OnTapToCollectButtonClicked()
    {
        braceletParent.transform.GetChild(GameDataManager.Instance.dataLists.room.generalThemeIndex - 1).gameObject.SetActive(true);
        ringSpawnParent.transform.GetChild(GameDataManager.Instance.dataLists.room.generalThemeIndex - 1).gameObject.SetActive(true);
        tapToCollectWindow.SetActive(false);
        collectWindow.SetActive(true);
    }
    public void OnCollectButtonClicked()
    {
        StudioUIManager.Instance.OnCloseThemeFinishedPanelBtnClicked();

    }
    private void OnDisable()
    {

        braceletParent.transform.GetChild(GameDataManager.Instance.dataLists.room.generalThemeIndex - 1).gameObject.SetActive(false);
        ringSpawnParent.transform.GetChild(GameDataManager.Instance.dataLists.room.generalThemeIndex - 1).gameObject.SetActive(false);
    }
}
