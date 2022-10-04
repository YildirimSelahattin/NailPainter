using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    public GameManager gm;
    [SerializeField] private GameObject successPanel;

    private void OnTriggerEnter(Collider other)//on player get to the finish level
    {
        successPanel.SetActive(true);       
        PlayerController.Instance.stopForwardMovement = true;
    }
}
