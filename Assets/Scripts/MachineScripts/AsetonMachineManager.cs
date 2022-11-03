using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsetonMachineManager : MonoBehaviour
{
    [SerializeField] Material[] matArray;
    string currentTag;

    private void OnTriggerExit(Collider other)//if one finger pass the brush
    {
        if (other.transform.tag.Contains("Nail"))
        {
            currentTag = other.transform.tag;
            GameManager.Instance.currentColorIndexArray[currentTag[currentTag.Length - 1] - '0'] = 0;
            GameManager.Instance.currentPatternIndexArray[currentTag[currentTag.Length - 1] - '0'] = 0;
            GameManager.Instance.currentDiamondIndexArray[currentTag[currentTag.Length - 1] - '0'] = 0;

            other.gameObject.GetComponent<MeshRenderer>().materials = matArray;
        }
    }
}
