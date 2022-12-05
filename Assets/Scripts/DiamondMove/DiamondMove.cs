using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DiamondMove : MonoBehaviour
{
    private bool collected = false;
    public Vector3 targetPos;

    // Update is called once per frame
    void Update()
    {
        if (collected == true)
        {
            targetPos = HandTriggerManager.Instance.GetIconPosition(transform.position);
            //transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * 5f);
            transform.DOMove(targetPos, Time.deltaTime * 100).OnComplete(() => HandTriggerManager.Instance.IncreaseMoneyAndDestroy(gameObject));
        }
    }

    public void SetCollected()
    {
        collected = true;
    }
}
