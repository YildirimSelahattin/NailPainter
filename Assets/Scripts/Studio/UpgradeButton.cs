using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class UpgradeButton : MonoBehaviour
{
    Vector3 originalPos;
    Material material;
    // Start is called before the first frame update
    void Start()
    {
        originalPos = transform.localPosition;
        material = GetComponent<MeshRenderer>().material;
        Move();
        Fade();
    }
    // Update is called once per frame
    void Update()
    {
        Debug.Log("after move" + transform.position);
    }
    public void Move()
    {
        transform.DOLocalMoveY(originalPos.y+3,1f).OnComplete(()=>transform.DOLocalMoveY(originalPos.y,1).OnComplete(()=>Move()));
    }
    public void Fade()
    {
        material.DOFade(0.5f, 0.5f).OnComplete(() => material.DOFade(1f, 0.5f).OnComplete(()=>Fade())); 
    }

}
