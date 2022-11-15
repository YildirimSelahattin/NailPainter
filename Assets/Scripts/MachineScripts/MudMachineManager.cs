using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MudMachineManager : MonoBehaviour
{
    [SerializeField] Material MudMaterial;
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

        if (other.transform.CompareTag("Hand"))
        {
            Material[] matArray = other.gameObject.GetComponent<MeshRenderer>().materials;
            matArray[1] = MudMaterial;
            other.gameObject.GetComponent<MeshRenderer>().materials = matArray;
            GameManager.Instance.isCleaned = false;
        }
    }
}
