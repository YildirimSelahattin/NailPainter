using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;
using TMPro;

public class GiftSlider : MonoBehaviour
{
    // Start is called before the first frame update
    RectTransform rectTransform;
    float[] degreeBounds = new float[] { 0.05f, 0.25f, 0.50f };
    [SerializeField] TextMeshProUGUI multiplierText;
    void Start()
    {
        rectTransform = gameObject.GetComponent<RectTransform>();
        
        DoRotateLoopMove();
    }

    // Update is called once per frame
    void Update()
    {
        multiplierText.text =  GetMultiplier().ToString();
    }
    public void DoRotateLoopMove()
    {
       rectTransform.DORotate(new Vector3(0,0,85), 2f).OnComplete(() => rectTransform.DORotate(new Vector3(0,0,-85), 2f).OnComplete(()=>DoRotateLoopMove()));
       //rectTransform.DOLocalRotate(new Vector3(0,0,14),2f).OnComplete(()=>rectTransform.DOLocalRotate( new Vector3(0,0,-14),2f));
    }

    public void OnGetButtonClicked()
    {
        int multiplier  = GetMultiplier();
        GameManager.Instance.moneyColletectedThisSession*=multiplier;
    }

    public int GetMultiplier()
    {
        float sliderRotateAmount = 66 / 100f - Mathf.Abs(rectTransform.localRotation.z);
        Debug.Log(sliderRotateAmount);
        if (sliderRotateAmount < 0.05f)
        {
            return 2;
        }

        else if (sliderRotateAmount < 0.25f)
        {
            return 3;
        }
        else if (sliderRotateAmount < 0.50f)
        {
            return 4;
        }
        else
        {
            return 5;
        }
        
    }
}
