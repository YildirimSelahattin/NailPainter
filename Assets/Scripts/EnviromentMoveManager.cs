using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class EnviromentMoveManager : MonoBehaviour
{
    public bool stopForwardMovement = true;
    public static EnviromentMoveManager Instance;
    [SerializeField] float forwardMoveSpeed = 8;
    // Start is called before the first frame update
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
            if (stopForwardMovement == false)
            {
            Debug.Log("basd");
                transform.position -= Vector3.forward * forwardMoveSpeed * Time.deltaTime;//regular go forward
            }
        
    }
   
}
