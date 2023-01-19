using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HandTriggerManager : MonoBehaviour
{
    [SerializeField] GameObject diamondPrefab;
    [SerializeField] Transform diamondImage;
    Vector3 diamondImageScaleReach;
    Vector3 standartScale;
    [SerializeField] Camera mainCamera;

    private bool isCollected = false;
    public static HandTriggerManager Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else { }
    }

    private void Start()
    {
        standartScale = diamondImage.localScale;
        diamondImageScaleReach = diamondImage.localScale * 1.1f;


    }

    //Player(Hand) in trigger islevleri
    private void OnTriggerEnter(Collider other)
    {
        //if hand hit a diamond
        if (other.transform.CompareTag("Diamond"))
        {
            other.gameObject.SetActive(false);
            Instantiate(diamondPrefab,diamondImage);

            if (GameDataManager.Instance.playSound == 1)
            {
                GameObject sound = new GameObject("sound");
                sound.AddComponent<AudioSource>().PlayOneShot(GameDataManager.Instance.diamondCollected);
                Destroy(sound, GameDataManager.Instance.diamondCollected.length); // Creates new object, add to it audio source, play sound, destroy this object after playing is done
            }
        }

        if (other.transform.CompareTag("EndGame"))
        {
            //color the current hand image according to current arrays
            ColorManager.Instance.ColorCurrentHand();
            //disable path following mode
            gameObject.transform.parent.gameObject.GetComponent<PlayerController>().enabled = false;
            //other.gameObject.SetActive(false);
            GameDataManager.Instance.upgradeAmountInSession = 0;
            UIManager.Instance.ShowEndScreen();
        }
    }

    //Elmas toplaninca sayiyi artıran fonk.
    public void IncreaseMoneyAndDestroy(GameObject diamond)
    {
        UIManager.Instance.currentLevelDiamond++;
        UIManager.Instance.NumberOfDiamonds++;
        diamondImage.DOScale(diamondImageScaleReach, 0.2f).OnComplete(() => diamondImage.DOScale(standartScale, 0.2f));
        Destroy(diamond);
    }

}
