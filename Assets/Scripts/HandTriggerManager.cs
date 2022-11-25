using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HandTriggerManager : MonoBehaviour
{
    [SerializeField] Transform diamondReachPointReference;

    //Player(Hand) in trigger islevleri
    private void OnTriggerEnter(Collider other)
    {
        //if hand hit a diamond
        if (other.transform.CompareTag("Diamond"))
        {
            MoveMoney(other);
            if (GameDataManager.Instance.playSound == 1)
            {
                GameObject sound = new GameObject("sound");
                sound.AddComponent<AudioSource>().PlayOneShot(GameDataManager.Instance.diamondCollected);
                Destroy(sound, GameDataManager.Instance.diamondCollected.length); // Creates new object, add to it audio source, play sound, destroy this object after playing is done
            }
        }

        if (other.transform.CompareTag("EndGame"))
        {
            //disable path following mode
            gameObject.transform.parent.gameObject.GetComponent<PlayerController>().enabled = false;
            //other.gameObject.SetActive(false);
            GameDataManager.Instance.upgradeAmountInSession = 0;
            UIManager.Instance.ShowEndScreen();
        }
    }

    //Elmas toplaninca sayiyi artıran fonk.
    private void IncreaseMoneyAndDestroy(GameObject diamond)
    {
        UIManager.Instance.currentLevelDiamond++;
        UIManager.Instance.NumberOfDiamonds++;
        Destroy(diamond);
    }

    //Toplanan elmaslarin ekranın sag üstüne gitmesini saglayan fonk.
    private void MoveMoney(Collider other)
    {
        other.transform.parent = this.transform;
        other.transform.DOLocalMove(diamondReachPointReference.localPosition, 1).OnComplete(() => IncreaseMoneyAndDestroy(other.gameObject));
        Vector3 originalScale = transform.localScale;
        other.transform.DOScale(0.02f / 4, 1);
    }
}
