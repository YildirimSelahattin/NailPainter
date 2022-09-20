using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorMachineManager : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]int colorIndex = 1;
    Material brushMaterial;
    
    void Start()
    {
        brushMaterial=NailPolishManager.Instance.GetColorMaterialByIndex(colorIndex);
        //Find the Standard Shader
        Material[] matArray = GetComponent<MeshRenderer>().materials;
        matArray[NailPolishManager.NAIL_COLOR_INDEX] = brushMaterial;
        GetComponent<MeshRenderer>().materials = matArray;
    }

    private void OnTriggerExit(Collider other)//if one finger pass the brush
    {
        if (other.transform.CompareTag("Nail"))
        {
            Material[] matArray = other.gameObject.GetComponent<MeshRenderer>().materials;
            matArray[NailPolishManager.NAIL_COLOR_INDEX] = brushMaterial;
            other.gameObject.GetComponent<MeshRenderer>().materials = matArray;
            Debug.Log("print!!");
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
