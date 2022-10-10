using DG.Tweening;
using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class ManicureMachineManager : MonoBehaviour
{
    enum NailType
    {
        Badem,
        Oval,
        Kut,
    }
    [SerializeField]GameObject handParent;
    [SerializeField] GameObject nailParent;
    GameObject newNailParent;
    public int newNailIndex = 2;
    bool usedOnce = false;
    // Start is called before the first frame update
    void Start()
    {
        MoveManicureMachine();
        newNailParent = handParent.transform.GetChild(newNailIndex).gameObject;
    }

    // Update is called once per frame
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag.Contains("Nail"))
        {  
            string currentTag = other.transform.tag;
            int index = currentTag[currentTag.Length - 1] - '0';
            Debug.Log(index);
            nailParent.transform.GetChild(index).gameObject.SetActive(false);
            newNailParent.transform.GetChild(index).gameObject.SetActive(true);
        }
    }
    public void ChangeManicureAfterNailType(int index)
    {
        newNailIndex = index;
        newNailParent = handParent.transform.GetChild(newNailIndex).gameObject;
    }

    private void MoveManicureMachine()
    {
        transform.DOLocalMoveX(4,1f).OnComplete(()=>transform.DOLocalMoveX(-4,1f).OnComplete(()=>MoveManicureMachine()));
    }

    /*  private void MoveManicureMachine(int index)
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
        GameManager.Instance.isManicured = true; // to notify game manager that player get in manicure machine

    }*/
}
