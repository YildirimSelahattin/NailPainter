using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MovingPolishTriggerManager : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("colorable"))
        {
            Debug.Log("hey");
            Material[] matArray = other.gameObject.GetComponent<MeshRenderer>().materials;
            matArray= matArray.SkipLast(1).ToArray();
            other.gameObject.GetComponent<MeshRenderer>().materials = matArray;
        }
    }
}
