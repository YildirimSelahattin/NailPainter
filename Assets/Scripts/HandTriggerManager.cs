using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;

public class HandTriggerManager : MonoBehaviour
{
    [SerializeField] Transform diamondReachPointReference;
    [SerializeField] Transform diamondImage;
    Vector3 diamondImageScaleReach;
    Vector3 standartScale;
    [SerializeField] CinemachineVirtualCamera mainCamera;
    [SerializeField] CinemachineVirtualCamera EndGameCamera;
    Vector3 targetPos;

    private void Start()
    {
        standartScale = diamondImage.localScale;
        diamondImageScaleReach = diamondImage.localScale * 1.1f;
    }

    private void OnEnable()
    {
        CinemachineController.Instance.Register(mainCamera);
        CinemachineController.Instance.Register(EndGameCamera);
        CinemachineController.Instance.SwitchCam(mainCamera);
    }

    private void OnDisable()
    {
        CinemachineController.Instance.UnRegister(mainCamera);
        CinemachineController.Instance.UnRegister(EndGameCamera);
    }

    //Player(Hand) in trigger islevleri
    private void OnTriggerEnter(Collider other)
    {
        //if hand hit a diamond
        if (other.transform.CompareTag("Diamond"))
        {
            Vector3 targetPos = GetIconPosition(other.transform.position);

            other.transform.DOMove(targetPos, Time.deltaTime * 50).OnComplete(() => IncreaseMoneyAndDestroy(other.gameObject));

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
            CinemachineController.Instance.SwitchCam(EndGameCamera);
            UIManager.Instance.ShowEndScreen();
        }
    }

    //Elmas toplaninca sayiyi artıran fonk.
    private void IncreaseMoneyAndDestroy(GameObject diamond)
    {
        UIManager.Instance.currentLevelDiamond++;
        UIManager.Instance.NumberOfDiamonds++;
        diamondImage.DOScale(diamondImageScaleReach, 0.2f).OnComplete(() => diamondImage.DOScale(standartScale, 0.2f));
        //Destroy(diamond);
    }

    //Toplanan elmaslarin ekranın sag üstüne gitmesini saglayan fonk.
    public Vector3 GetIconPosition(Vector3 target)
    {
        Vector3 IconPos = UIManager.Instance.DiamondIcon.position;
        IconPos.z = (target - mainCamera.transform.position).z;
        Vector3 result = new Vector3(1,1,1);
        return result;
    }
}
