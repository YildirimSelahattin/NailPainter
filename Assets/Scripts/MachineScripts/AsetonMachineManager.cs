using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsetonMachineManager : MonoBehaviour
{
    [SerializeField] Material[] matArray;

    private void OnTriggerExit(Collider other)//if one finger pass the brush
    {
        if (other.transform.tag.Contains("Nail"))
        {
            string currentTag = other.transform.tag;
            other.gameObject.GetComponent<MeshRenderer>().materials = matArray;
        }
    }
}
