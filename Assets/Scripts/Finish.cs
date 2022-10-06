using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    float rate;
    [SerializeField] float WinRate = 60 ;
    [SerializeField] GameObject winDialog ;
    [SerializeField] GameObject loseDialog ;

    private void OnTriggerEnter(Collider other)//on player get to the finish level
    {
        PlayerController.Instance.stopForwardMovement = true;
        rate = GameManager.Instance.CompareTwoHands();
        Debug.Log(rate);

        if(rate> WinRate)
        {
            winDialog.gameObject.SetActive(true);
        }
        else
        {
            loseDialog.gameObject.SetActive(true);
        }
    }

    
}
