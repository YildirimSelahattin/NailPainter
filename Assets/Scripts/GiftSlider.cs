using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class GiftSlider : MonoBehaviour
{
    // Start is called before the first frame update
    RectTransform rectTransform;
    
    void Start()
    {
        rectTransform = gameObject.GetComponent<RectTransform>();
        DoCircularLoopMove();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void DoCircularLoopMove()
    {
       rectTransform.DOShapeCircle(new Vector2(0, -100), 45, 10* Time.deltaTime).OnComplete(() => rectTransform.DOShapeCircle(new Vector2(0, -100),-45, 10*Time.deltaTime).OnComplete(()=>DoCircularLoopMove()));
    }
}
