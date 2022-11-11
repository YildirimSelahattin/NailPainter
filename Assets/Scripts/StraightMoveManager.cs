using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightMoveManager : MonoBehaviour
{
    public bool stopForwardMovement = false;
    [SerializeField]float forwardMoveSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (stopForwardMovement == false)
        {
            transform.position -= Vector3.forward * forwardMoveSpeed * Time.deltaTime;//regular go forward
        }
    }
}
