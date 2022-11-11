using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HandTriggerManager : MonoBehaviour
{
    [SerializeField] Transform diamondReachPointReference;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        //if hand hit a diamond
        if (other.transform.CompareTag("Diamond"))
        {
            MoveMoney(other);
        }

        if (other.transform.CompareTag("EndGame"))
        {
            //disable path following mode
            gameObject.transform.parent.gameObject.GetComponent<PlayerController>().enabled = false;
            other.gameObject.SetActive(false);
            UIManager.Instance.ShowEndScreen();
        }
    }

    private void IncreaseMoneyAndDestroy(GameObject diamond)
    {
        UIManager.Instance.NumberOfDiamonds++;
        Destroy(diamond);
    }

    private void MoveMoney(Collider other)
    {
        other.transform.parent = this.transform;
        other.transform.DOLocalMove(diamondReachPointReference.localPosition, 1).OnComplete(() => IncreaseMoneyAndDestroy(other.gameObject));
        Vector3 originalScale = transform.localScale;
        other.transform.DOScale(0.02f/4, 1);
    }


}
