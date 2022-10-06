using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class BrushManager : MonoBehaviour
{
    public int colorIndex = 1;
    Material brushMaterial;
    [SerializeField] GameObject headPart;
    [SerializeField] GameObject brushPart;

    void Start()
    {
        brushMaterial = ColorManager.Instance.GetColorMaterialByIndex(colorIndex);

        //Find the Standard Shader
        Material[] matArray = brushPart.GetComponent<MeshRenderer>().materials;
        matArray[0] = brushMaterial;
        brushPart.GetComponent<MeshRenderer>().materials = matArray;

        Material[] matArrays = headPart.GetComponent<MeshRenderer>().materials;
        matArrays[0] = brushMaterial;
        headPart.GetComponent<MeshRenderer>().materials = matArrays;
    }

    private void OnTriggerExit(Collider other)//if one finger pass the brush
    {
        if (other.transform.tag.Contains("Nail"))
        {
            string currentTag = other.transform.tag;
            Material[] matArray = other.gameObject.GetComponent<MeshRenderer>().materials;
            matArray[ColorManager.NAIL_COLOR_INDEX] = brushMaterial;
            other.gameObject.GetComponent<MeshRenderer>().materials = matArray;
            GameManager.Instance.currentColorIndexArray[currentTag[currentTag.Length - 1] - '0'] = colorIndex;
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
