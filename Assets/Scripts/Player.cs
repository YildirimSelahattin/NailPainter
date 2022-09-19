using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Transform Center;
    [SerializeField] private float speed;

    private void Awake()
    {
        Center = GameObject.FindGameObjectWithTag("PlayerBase").transform;
    }
}
