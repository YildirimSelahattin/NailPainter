using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingMachineManager : MonoBehaviour
{
    public int ringIndex;
    [SerializeField] public int whichFingerIndex;
    [SerializeField] GameObject handParent;
    GameObject nailParent;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag.Contains("Nail"))
        {
            transform.parent = handParent.transform;
            Vector3 targetPos = handParent.transform.GetChild(1).gameObject.transform.GetChild(whichFingerIndex).transform.localPosition;
            targetPos.z -= 2;
            transform.DOLocalMove(targetPos, 1f);
            GameManager.Instance.currentRingIndexArray.Add(ringIndex);
        }
    }
}
