using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternMachineManager : MonoBehaviour
{
    Material patternMaterial;
    Material thumbPatternMaterial;
    Vector3 standartPosition;
    public int patternIndex;
    public static bool nail1iscollerd = false;
    // Start is called before the first frame update
    void Start()
    {
        //standartPosition = transform.position;
        patternMaterial = ColorManager.Instance.GetPatternMaterialByIndex(patternIndex,false);
        thumbPatternMaterial = ColorManager.Instance.GetPatternMaterialByIndex(patternIndex,true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag.Contains("Nail"))
        {
            Material materialToAdd;
            string currentTag = other.transform.tag;
            int index = currentTag[currentTag.Length - 1] - '0';
            if(index == 0)
            {
                materialToAdd = thumbPatternMaterial;
            }
            else
            {
                materialToAdd = patternMaterial;
            }
           
            Material[] matArrays = other.gameObject.GetComponent<MeshRenderer>().materials;
            matArrays[ColorManager.NAIL_PATTERN_INDEX] = materialToAdd;
            other.gameObject.GetComponent<MeshRenderer>().materials = matArrays;
            GameManager.Instance.currentPatternIndexArray[currentTag[currentTag.Length - 1] - '0'] = patternIndex;
            //transform.DOLocalMoveY(standartPosition.y, 0.1f);
            Debug.Log("cikti");
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag.Contains("Nail"))
        {
            //transform.DOLocalMoveY(other.transform.position.y, 0.1f);
            Debug.Log("girdi");
        }
    }
}
