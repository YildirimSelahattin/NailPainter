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
        yield return new WaitForSeconds(Random.Range(0.5f, 0.7f));
        gameObject.GetComponent<Image>().DOFade(0, Random.Range(1f, 2f)).OnComplete(() => ResetFadeAndDisable());
    }
}
