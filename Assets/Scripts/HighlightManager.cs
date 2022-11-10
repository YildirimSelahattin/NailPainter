using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightManager : MonoBehaviour
{
    [SerializeField] Material highlightMat;
    [SerializeField] Material TransparrentMat;


        private void OnTriggerStay (Collider other)//if one finger pass the brush
    {
        if (other.transform.tag.Contains("Nail"))
        {
            string currentTag = other.transform.tag;
            Material[] matArray = other.gameObject.GetComponent<MeshRenderer>().materials;
            matArray[3] = highlightMat;
            other.gameObject.GetComponent<MeshRenderer>().materials = matArray;
        }
    }

    private void OnTriggerExit (Collider other)//if one finger pass the brush
    {
        if (other.transform.tag.Contains("Nail"))
        {
            string currentTag = other.transform.tag;
            Material[] matArray = other.gameObject.GetComponent<MeshRenderer>().materials;
            matArray[3] = TransparrentMat;
            other.gameObject.GetComponent<MeshRenderer>().materials = matArray;
        }
    }
    
}