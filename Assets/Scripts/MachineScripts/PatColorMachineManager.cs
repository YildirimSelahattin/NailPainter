using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatColorMachineManager : MonoBehaviour
{
    Material patternColorMaterial;
    Vector3 standartPosition;
    //[SerializeField] GameObject roller;
    [SerializeField] int patternColorIndex;
    // Start is called before the first frame update
    void Start()
    {
        standartPosition = transform.position;
        patternColorMaterial = ColorManager.Instance.GetPatternColorMaterialByIndex(patternColorIndex);
        Material[] matArrays = gameObject.GetComponent<MeshRenderer>().materials;
        matArrays[0] = patternColorMaterial;
        gameObject.GetComponent<MeshRenderer>().materials = matArrays;
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("Nail"))
        {
            Material[] matArrays = other.gameObject.GetComponent<MeshRenderer>().materials;
            matArrays[ColorManager.NAIL_PATTERN_COLOR_INDEX] = patternColorMaterial;

            other.gameObject.GetComponent<MeshRenderer>().materials = matArrays;
            Debug.Log("cikti");
        }
    }
}
