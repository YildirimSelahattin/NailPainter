using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrushManager : MonoBehaviour
{
    [SerializeField] int colorIndex = 1;
    Material brushMaterial;
    [SerializeField] GameObject headPart;
    [SerializeField] GameObject brushPart;

    void Start()
    {
        brushMaterial = ColorManager.Instance.GetColorMaterialByIndex(colorIndex);

        //Find the Standard Shader
        Material[] matArray = brushPart.GetComponent<MeshRenderer>().materials;
        matArray[ColorManager.NAIL_COLOR_INDEX] = brushMaterial;
        brushPart.GetComponent<MeshRenderer>().materials = matArray;

        Material[] matArrays = headPart.GetComponent<MeshRenderer>().materials;
        matArrays[ColorManager.NAIL_COLOR_INDEX] = brushMaterial;
        headPart.GetComponent<MeshRenderer>().materials = matArrays;
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
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Hand"))//When HandRigged Gets Triggered Because ColorMachine
        {
            transform.DORotate(new Vector3(-30, 0, 0), 1);
            Debug.Log("colormachinemove");
        }
    }
}
