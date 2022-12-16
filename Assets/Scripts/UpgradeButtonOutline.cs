using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class UpgradeButtonOutline : MonoBehaviour
{
    [SerializeField]Material outlineMat;
    // Start is called before the first frame update
    void Start()
    {
        FadeInFadeOutLoop(outlineMat);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FadeInFadeOutLoop(Material mat)
    {
        mat.DOFade(0f / 255f, 1).OnComplete(() => mat.DOFade(200 / 255f, 1).OnComplete(() => FadeInFadeOutLoop(mat)));
    }
}
