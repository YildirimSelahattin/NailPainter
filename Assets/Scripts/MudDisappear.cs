using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class MudDisappear : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Image>().DOFade(0,Random.Range(0.5f,1f)).OnComplete(()=>ResetFadeAndDisable());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ResetFadeAndDisable()
    {
        gameObject.SetActive(false);
        Color color = gameObject.GetComponent<Image>().color;
        color.a = 1;
        gameObject.GetComponent<Image>().color = color;
    }
}
