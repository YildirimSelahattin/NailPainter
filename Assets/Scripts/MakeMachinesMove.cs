using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MakeMachinesMove : MonoBehaviour
{
    // THÝS SCRÝPT MAKES NAÝL ART MACHÝNES MOVE WHEN THEÝR COLLÝDER TRÝGGERS HAND RÝGGED GAME OBJECT
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("ColorMachine"))//When HandRigged Gets Triggered Because ColorMachine
        {
            other.transform.DORotate(new Vector3(-30, 0, 0),1);
            Debug.Log("colormachinemove");
        }
    }
}
