using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class ImageShaker : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float strenghtOfShake = 5;
    [SerializeField] float waitForSeconds;
    [SerializeField] float shakeDuration = 1;
    void Start()
    {
        StartCoroutine(Shake());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public IEnumerator Shake()
    {
        yield return new WaitForSeconds(waitForSeconds);
        //transform.DOShakeRotation(shakeDuration, strenghtOfShake, 2, 30);
        transform.DOShakePosition(shakeDuration, strenghtOfShake, 4,30).OnComplete(()=>StartCoroutine(Shake()));

    }
}
