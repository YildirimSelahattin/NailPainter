using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SoupMove : MonoBehaviour
{
    //delete
    void Start()
    {
        MoveSoup();
    }

    private void MoveSoup()
    {
        transform.DOLocalMoveX(1, 1f).OnComplete(() => transform.DOLocalMoveX(-1, 1f).OnComplete(() => MoveSoup()));
    }
}
