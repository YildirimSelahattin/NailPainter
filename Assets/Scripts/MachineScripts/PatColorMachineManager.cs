using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatColorMachineManager : MonoBehaviour
{
    Material patternColorMaterial;
    Vector3 standartPosition;
    [SerializeField] int patternColorIndex;
    // Start is called before the first frame update
    void Start()
    {
        //Machine colors itself first
        standartPosition = transform.position;
        patternColorMaterial = ColorManager.Instance.GetPatternColorMaterialByIndex(patternColorIndex);
        Material[] matArrays = gameObject.GetComponent<MeshRenderer>().materials;
        matArrays[0] = patternColorMaterial;
        gameObject.GetComponent<MeshRenderer>().materials = matArrays;
    }

    // when it trigggers with  nail, it colors it accordng to the machines color and writes the index of the color 
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag.Contains("Nail"))
        {
            string currentTag = other.transform.tag;
            Material[] matArrays = other.gameObject.GetComponent<MeshRenderer>().materials;
            matArrays[ColorManager.NAIL_PATTERN_COLOR_INDEX] = patternColorMaterial;
            other.gameObject.GetComponent<MeshRenderer>().materials = matArrays;
            GameManager.Instance.currentPatternColorIndexArray[currentTag[currentTag.Length - 1] - '0'] = patternColorIndex;
            Debug.Log("cikti");
        }
    }
}
