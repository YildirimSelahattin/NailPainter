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
    [SerializeField]GameObject[] outlineArray;
    [SerializeField] TextMeshProUGUI multiplierText;
    [SerializeField] TextMeshProUGUI currentMoneyText;
    [SerializeField] GameObject noThanksButton;
    int multiplier;
    int prevMultiplier;
    void Start()
    {
        //currentMoneyText.text = GameManager.Instance.moneyColletectedThisSession.ToString();
        rectTransform = gameObject.GetComponent<RectTransform>();
        
        DoRotateLoopMove();
        StartCoroutine(OpenNoThanksAfterDelay(2));
    }

    // Update is called once per frame
    void Update()
    {
        /* int multiplier = GetMultiplier();
         multiplierText.text = multiplier.ToString();
         if (multiplier != prevMultiplier)
         {
             outlineArray[prevMultiplier]?.SetActive(false);
             if (multiplier == 5)
             {
                 outlineArray[multiplier].SetActive(true);
                 prevMultiplier = 5;
             }
             else
             {
                 if (rectTransform.localRotation.z > 0)
                 {
                     outlineArray[multiplier].SetActive(true);
                     prevMultiplier = multiplier;
                 }
                 else
                 {
                     outlineArray[multiplier + 4].SetActive(true);
                     prevMultiplier = multiplier+4;
                 }
             }
         }*/
        Debug.Log(rectTransform.localRotation.z);
    }
    public void DoRotateLoopMove()
    {
       rectTransform.DOLocalRotate(new Vector3(0,0,60), 2f).OnComplete(() => rectTransform.DOLocalRotate(new Vector3(0,0,-60), 2f).OnComplete(()=>DoRotateLoopMove()));
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
            return   2;
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
    private IEnumerator OpenNoThanksAfterDelay(int delay)
    {
        yield return new WaitForSeconds(delay);
        noThanksButton.SetActive(true);
    }
}
