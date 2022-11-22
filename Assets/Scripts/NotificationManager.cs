using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class NotificationManager : MonoBehaviour
{
    Vector3 startPos;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.localPosition;
        MoveLoop();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void MoveLoop()
    {
        transform.DOMoveZ(startPos.z - 1, 1f).OnComplete(() => transform.DOMoveZ(startPos.z,1f).OnComplete(()=>MoveLoop()));
    }
}
