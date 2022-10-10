using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WashMachine : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        MoveWashMachine();
    }

    private void MoveWashMachine()
    {
        transform.DOLocalMoveX(1, 1f).OnComplete(() => transform.DOLocalMoveX(-1, 1f).OnComplete(() => MoveWashMachine()));
    }
}
