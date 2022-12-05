using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DİamondMove : MonoBehaviour
{
    public Vector3 targetPos;
    public bool isHitted;
    public Image diamondImage;
    // Start is called before the first frame update
    void Start()
    {
        transform.DOLocalMove(new Vector3(0, 0, 0), 1.5f).OnComplete(()=>Destroy(gameObject));

    }

    // Update is called once per frame
}
