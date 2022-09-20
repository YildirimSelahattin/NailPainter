using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrushManager : MonoBehaviour
{
    [SerializeField] int colorIndex = 1;
    Material brushMaterial;
    [SerializeField] GameObject headMaterial;

    void Start()
    {
        brushMaterial = ColorManager.Instance.GetColorMaterialByIndex(colorIndex);

        //Find the Standard Shader
        Material[] matArray = GetComponent<MeshRenderer>().materials;
        matArray[ColorManager.NAIL_COLOR_INDEX] = brushMaterial;
        GetComponent<MeshRenderer>().materials = matArray;

        Material[] matArrays = headMaterial.GetComponent<MeshRenderer>().materials;
        matArrays[ColorManager.NAIL_COLOR_INDEX] = brushMaterial;
        headMaterial.GetComponent<MeshRenderer>().materials = matArrays;
    }

    private void OnTriggerExit(Collider other)//if one finger pass the brush
    {
        if (other.transform.CompareTag("Nail"))
        {
            Material[] matArray = other.gameObject.GetComponent<MeshRenderer>().materials;
            matArray[ColorManager.NAIL_COLOR_INDEX] = brushMaterial;
            other.gameObject.GetComponent<MeshRenderer>().materials = matArray;
            Debug.Log("print!!");
        }
    }
}
