using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GiftUIManager : MonoBehaviour
{
    [SerializeField] GameObject finishedThemeDialog;
    [SerializeField] GameObject tapToCollectWindow;
    [SerializeField] GameObject collectWindow;
    [SerializeField] GameObject braceletParent;
    [SerializeField] GameObject ringSpawnParent;
    [SerializeField] GameObject rewardPanelBG;
    [SerializeField] Sprite[] finishedRoomSprites;

    // Start is called before the first frame update
    void Start()
    {
        //set Image 
        finishedThemeDialog.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = finishedRoomSprites[GameDataManager.Instance.dataLists.room.generalThemeIndex-1];
    }

    public void OnContinueButtonClicked()
    {
        finishedThemeDialog.SetActive(false);
        rewardPanelBG.SetActive(true);
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
        rewardPanelBG.SetActive(false);
        StudioUIManager.Instance.OnCloseThemeFinishedPanelBtnClicked();
    }

    private void OnDisable()
    {
        braceletParent.transform.GetChild(GameDataManager.Instance.dataLists.room.generalThemeIndex - 1).gameObject.SetActive(false);

        ringSpawnParent.transform.GetChild(GameDataManager.Instance.dataLists.room.generalThemeIndex - 1).gameObject.SetActive(false);
        finishedThemeDialog.SetActive(true);
        tapToCollectWindow.SetActive(false);
        collectWindow.SetActive(false);

    }
}
