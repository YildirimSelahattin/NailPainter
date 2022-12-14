using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class PopArtAppearAnimator : MonoBehaviour
{
    //Dogru boyanan tırnaktan sonra ekranda beliren yazıların animasyonu
    private void OnEnable()
    {
        if (GameDataManager.Instance.playSound == 1)
        {
            Handheld.Vibrate();
        }
        Vector3 standartScale = transform.localScale;
        transform.DOScale(new Vector3(0.1f, 0.1f, 0.1f), 0).OnComplete(() => transform.DOScale(standartScale, .7f).OnComplete(() => gameObject.SetActive(false)));
    }
}
