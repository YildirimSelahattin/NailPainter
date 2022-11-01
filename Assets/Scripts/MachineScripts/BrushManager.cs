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
    int brushColorMatArrayIndex = 1;
    [SerializeField] GameObject headPart;
    [SerializeField] GameObject brushPart;
    void Start()
    {
        brushMaterial = ColorManager.Instance.GetColorMaterialByIndex(colorIndex);
        //get the brushs material
        GameObject brush = gameObject.transform.GetChild(0).transform.GetChild(0).gameObject;
        Material[] matArray = brush.GetComponent<MeshRenderer>().materials;
        matArray[brushColorMatArrayIndex] = brushMaterial;
        brush.GetComponent<MeshRenderer>().materials = matArray;
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

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("PlayerBase"))//When HandRigged Gets Triggered Because ColorMachine
        {
            // TODO, 
            /*a
            transform.DORotate(new Vector3(-30, 0, 0), 1);
            Debug.Log("colormachinemove");
            */
        }
    }
}