using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class PopArtAppearAnimator : MonoBehaviour
{
    // Start is called before the first frame update


    private void OnEnable()
    {
        Vector3 standartScale = transform.localScale;
        transform.DOScale(new Vector3(0.1f, 0.1f, 0.1f), 0).OnComplete(()=>transform.DOScale(standartScale,1f).OnComplete(()=>gameObject.SetActive(false)));
    }

 
}
