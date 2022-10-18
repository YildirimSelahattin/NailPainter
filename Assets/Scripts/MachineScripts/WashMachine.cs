using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WashMachine : MonoBehaviour
{
    public Material transparentMat;
    private void Start()
    {
        //Add material list soap and sponge materials 
        CurveAmountManager.Instance.materialArray.Add(gameObject.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material);
        CurveAmountManager.Instance.materialArray.Add(gameObject.transform.GetChild(1).gameObject.GetComponent<MeshRenderer>().material);

    }
    private void OnTriggerExit(Collider other)//if one finger pass the brush
    {
        if (other.transform.tag.Contains("Hand"))
        {
            //dustDecal.gameObject.transform.DOLocalMoveZ(0f, 0.5f);
            string currentTag = other.transform.tag;
            Material[] matArray = other.gameObject.GetComponent<MeshRenderer>().materials;
            matArray[1] = transparentMat;
            other.gameObject.GetComponent<MeshRenderer>().materials = matArray; 
        }
    }
}
