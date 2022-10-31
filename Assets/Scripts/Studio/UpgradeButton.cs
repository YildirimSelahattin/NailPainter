using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class UpgradeButton : MonoBehaviour
{
    Vector3 originalPos;

    // Start is called before the first frame update
    void Start()
    {
        originalPos = transform.position;
        Move();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void Move()
    {
        transform.DOLocalMoveY(originalPos.y+1,1f).OnComplete(()=>transform.DOLocalMoveY(originalPos.y,1).OnComplete(()=>Move()));

    }
}
