using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetect : MonoBehaviour
{
    private PlayerController playerController;
    public SphereCollider sphCol;
    private bool isplayerDetect = false;
    public bool rush = false;

    private void Awake()
    {
        playerController = GameObject.FindGameObjectWithTag("PlayerBase").GetComponent<PlayerController>();       
    }

    private void Start()
    {
        transform.eulerAngles = new Vector3(0, 180, 0);
    }
}
