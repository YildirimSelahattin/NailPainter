using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MakeMachinesMove : MonoBehaviour
{
    // TH�S SCR�PT MAKES NA�L ART MACH�NES MOVE WHEN THE�R COLL�DER TR�GGERS HAND R�GGED GAME OBJECT
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("ColorMachine"))//When HandRigged Gets Triggered Because ColorMachine
        {
            other.transform.DORotate(new Vector3(-30, 0, 0),1);
            Debug.Log("colormachinemove");
        }
    }
}
