using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WashMachine : MonoBehaviour
{
    public Material transparentMat;

    private void OnTriggerExit(Collider other)//if one finger pass the brush
    {
        if (other.transform.tag.Contains("Hand"))
        {
            string currentTag = other.transform.tag;
            Material[] matArray = other.gameObject.GetComponent<MeshRenderer>().materials;
            matArray[1] = transparentMat;
            other.gameObject.GetComponent<MeshRenderer>().materials = matArray; 
        }
    }
}
