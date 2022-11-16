using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BraceletMachineManager : MonoBehaviour
{
    public int braceletIndex;
    [SerializeField] GameObject handParent;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Nail"))
        {
            transform.parent = handParent.transform;
            transform.DOLocalMove(new Vector3(0, 0, 2.5f), 1f);
            GameManager.Instance.currentBraceletIndexArray.Add(braceletIndex);
        }
    }
}
