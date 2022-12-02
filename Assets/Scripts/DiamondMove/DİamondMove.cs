using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DİamondMove : MonoBehaviour
{
    public Vector3 targetPos;

    // Start is called before the first frame update
    void Start()
    {
        targetPos = HandTriggerManager.Instance.targetPos;
    }

    // Update is called once per frame
    void Update()
    {
        targetPos = HandTriggerManager.Instance.GetIconPosition(transform.position);

        transform.DOMove(targetPos, Time.deltaTime * 100).OnComplete(() => HandTriggerManager.Instance.IncreaseMoneyAndDestroy(gameObject));
    }
}
