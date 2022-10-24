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
            Debug.Log("niye olmuyor");
        }

        if (other.transform.CompareTag("EndGame"))
        {
       
            UIManager.Instance.ShowEndScreen();
            
        }
    }

    private void IncreaseMoneyAndDestroy(GameObject diamond)
    {
        Destroy(diamond);
    }

    private void MoveMoney(Collider other)
    {
        other.transform.parent = this.transform;

        other.transform.DOLocalMove(diamondReachPointReference.localPosition, 1f).OnComplete(() => IncreaseMoneyAndDestroy(other.gameObject));
        other.transform.DOScale(0.1f, 1);
    }


}
