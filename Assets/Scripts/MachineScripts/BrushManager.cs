using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Windows;

public class BrushManager : MonoBehaviour
{
    public int colorIndex = 1;
    Material machineMaterial;
    Material paintMaterial;
    void Start()
    {
        paintMaterial = ColorManager.Instance.GetColorMaterialByIndex(colorIndex);
        machineMaterial=gameObject.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material ;
        CurveAmountManager.Instance.materialArray.Add(machineMaterial);
    }

    private void OnTriggerExit(Collider other)//if one finger pass the brush
    {
        if (other.transform.tag.Contains("Nail"))
        {
            string currentTag = other.transform.tag;
            Material[] matArray = other.gameObject.GetComponent<MeshRenderer>().materials;
            matArray[ColorManager.NAIL_COLOR_INDEX] = paintMaterial;
            other.gameObject.GetComponent<MeshRenderer>().materials = matArray;
            GameManager.Instance.currentColorIndexArray[currentTag[currentTag.Length - 1] - '0'] = colorIndex;
            Debug.Log("print!!");
            
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("PlayerBase"))//When HandRigged Gets Triggered Because ColorMachine
        {
            transform.DORotate(new Vector3(-30, 0, 0), 1);
            Debug.Log("colormachinemove");
        }
    }
}
