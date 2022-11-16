using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MudMachineManager : MonoBehaviour
{
    [SerializeField] Material MudMaterial;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Hand"))
        {
            Material[] matArray = other.gameObject.GetComponent<MeshRenderer>().materials;
            matArray[1] = MudMaterial;
            other.gameObject.GetComponent<MeshRenderer>().materials = matArray;
            GameManager.Instance.isCleaned = false;
        }
    }
}
