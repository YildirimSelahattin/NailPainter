using DG.Tweening;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManicureMachineManager : MonoBehaviour
{
    [SerializeField]GameObject handParent;
    [SerializeField] GameObject nailParent;
    [SerializeField] GameObject newNailParent;
    bool usedOnce = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void OnTriggerExit(Collider other)
    {

        if (other.transform.CompareTag("Nail"))
        {
               if (usedOnce == false ) // parenting and move
                {
                    gameObject.transform.parent = handParent.transform;
                    MoveManicureMachine(0);
                    usedOnce = true;
                }
        }
    }
    private void MoveManicureMachine(int index)
    {
        if (index == 4)
        {
            gameObject.transform.DOLocalMove(nailParent.transform.GetChild(index).transform.localPosition, 0.5f).OnComplete(() => transform.parent = null);
        }
        else {
            gameObject.transform.DOLocalMove(nailParent.transform.GetChild(index).transform.localPosition, 0.5f).OnComplete(() => MoveManicureMachine(index + 1));
        }
        nailParent.transform.GetChild(index).gameObject.SetActive(false);
        newNailParent.transform.GetChild(index).gameObject.SetActive(true);

    }
}
