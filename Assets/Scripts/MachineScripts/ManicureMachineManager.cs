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
    GameObject handParent;
    [SerializeField] GameObject nailParticle;
    GameObject newNailParent;
    public static int nailTypeAfterManicure = 2;
    public static ManicureMachineManager Instance;
    bool usedOnce = false;
    float realPos;
    // Start is called before the first frame update
    void Start()
    {
        if(Instance == null)
        {
            Instance = this;
        }

        realPos = transform.position.x;

        MoveManicureMachine();
        handParent= GameObject.FindGameObjectWithTag("PlayerBase");
        newNailParent = handParent.transform.GetChild(GameManager.Instance.currentLevel.nailTypeAfterManicure).gameObject;
    }

    // Update is called once per frame
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag.Contains("Nail"))
        {  
            string currentTag = other.transform.tag;
            int index = currentTag[currentTag.Length - 1] - '0';
            other.gameObject.SetActive(false);
            newNailParent.transform.GetChild(index).gameObject.SetActive(true);
            GameManager.Instance.currentNailType = nailTypeAfterManicure;
            GameManager.Instance.isManicured = true;
        }
    }
    private void OnTriggerEnter(Collider other)// when nail enters the collider, play nail particle on it
    {
        if (other.transform.tag.Contains("Nail"))
        {
            GameObject particles = Instantiate(nailParticle, other.transform.position,other.transform.rotation);
            particles.transform.parent = handParent.transform;

            GameManager.Instance.isManicured = true;
        }
    }

    private void MoveManicureMachine()
    {
        transform.DOLocalMoveX(realPos+2,1f).OnComplete(()=>transform.DOLocalMoveX(realPos-2,1f).OnComplete(()=>MoveManicureMachine()));
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
