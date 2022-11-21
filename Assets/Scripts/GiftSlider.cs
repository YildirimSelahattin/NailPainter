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
    float[] degreeBounds = new float[] { 0.42f, 0.14f, 0.50f };
    [SerializeField]GameObject[] outlineArray;
    [SerializeField] TextMeshProUGUI multiplierText;
    [SerializeField] TextMeshProUGUI currentMoneyText;
    [SerializeField] GameObject noThanksButton;
    public int multiplier;
    int prevMultiplier;
    void Start()
    {
        //currentMoneyText.text = GameManager.Instance.moneyColletectedThisSession.ToString();
        rectTransform = gameObject.GetComponent<RectTransform>();
        
        DoRotateLoopMove();
        //StartCoroutine(OpenNoThanksAfterDelay(2));
    }

    // Update is called once per frame
    void Update()
    {
         int multiplier = GetMultiplier();
         multiplierText.text = multiplier.ToString();
         
    }
    public void DoRotateLoopMove()
    {
       rectTransform.DOLocalRotate(new Vector3(0,0,60), 2f).OnComplete(() => rectTransform.DOLocalRotate(new Vector3(0,0,-60), 2f).OnComplete(()=>DoRotateLoopMove()));
       //rectTransform.DOLocalRotate(new Vector3(0,0,14),2f).OnComplete(()=>rectTransform.DOLocalRotate( new Vector3(0,0,-14),2f));
    }

    public void OnGetButtonClicked()
    {
        int multiplier  = GetMultiplier();
        //stopRotation;
        transform.DOKill();
        GameManager.Instance.moneyColletectedThisSession*=multiplier;
    }

    public int GetMultiplier()
    {
        float sliderRotateAmount = Mathf.Abs(rectTransform.localRotation.z);
        
        if (sliderRotateAmount > 0.42f)
        {
            return   3;
        }

        if (sliderRotateAmount > 0.14f)
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
