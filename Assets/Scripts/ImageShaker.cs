using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class ImageShaker : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float strenghtOfShake = 5;
    void Start()
    {
        Shake();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Shake()
    {
        transform.DOShakeRotation(2, strenghtOfShake, 3, 30).OnComplete(()=>transform.DOShakePosition(2, strenghtOfShake, 3,30).OnComplete(()=>Shake()));

    }
}
