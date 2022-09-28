using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingMachineManager : MonoBehaviour
{
    [SerializeField]public int whichFingerIndex;
    [SerializeField] GameObject handParent;
    GameObject nailParent;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Nail"))
        {
            transform.parent = handParent.transform;
            Vector3 targetPos=handParent.transform.GetChild(1).gameObject.transform.GetChild(whichFingerIndex).transform.localPosition;
            targetPos.z -= 2;
            transform.DOLocalMove(targetPos, 1f);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
