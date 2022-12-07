using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using Unity.VisualScripting;

public class MudDisappear : MonoBehaviour
{
    // Start is called before the first frame update
    void OnEnable()
    {
        StartCoroutine(DelayAndStartFading());
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
    public IEnumerator DelayAndStartFading()
    {
        yield return new WaitForSeconds(Random.Range(0.5f, 1f));
        gameObject.GetComponent<Image>().DOFade(0, Random.Range(1f, 2f)).OnComplete(() => ResetFadeAndDisable());
    }
}
