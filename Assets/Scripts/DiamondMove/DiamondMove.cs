using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DiamondMove : MonoBehaviour
{
    void Start()
    {
        transform.DOLocalMove(new Vector3(0, 0, 0), 1.5f).OnComplete(() => HandTriggerManager.Instance.IncreaseMoneyAndDestroy(gameObject)) ;
        transform.DOScale(new Vector3(.8f, .8f, .8f), 1.5f);
    }
  
}
