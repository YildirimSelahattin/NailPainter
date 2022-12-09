using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    public static GiftUIManager Instance;
    // Start is called before the first frame update
    void Start()
    {
        if (Instance == null)
        {
            Instance=this;
        }
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
        tapToCollectWindow.SetActive(false);
        collectWindow.SetActive(true);
        if (GameDataManager.Instance.playSound == 1)
        {
            GameObject sound = new GameObject("sound");
            sound.AddComponent<AudioSource>().PlayOneShot(GameDataManager.Instance.openingBagSound);
            Destroy(sound, GameDataManager.Instance.openingBagSound.length); // Creates new object, add to it audio source, play sound, destroy this object after playing is done
        }
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

    public void OpenRingAndRingAfterSpotlightGrow()
    {
        braceletParent.transform.GetChild(GameDataManager.Instance.dataLists.room.generalThemeIndex - 1).gameObject.SetActive(true);
        ringSpawnParent.transform.GetChild(GameDataManager.Instance.dataLists.room.generalThemeIndex - 1).gameObject.SetActive(true);
    }
}
