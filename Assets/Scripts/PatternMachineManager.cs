using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternMachineManager : MonoBehaviour
{
    Material patternMaterial;
    Vector3 standartPosition;
    [SerializeField] GameObject patternSign;
    [SerializeField] int patternIndex;
    // Start is called before the first frame update
    void Start()
    {
        standartPosition = transform.position;
        patternMaterial = ColorManager.Instance.GetPatternMaterialByIndex(patternIndex);
        Material[] matArrays = patternSign.GetComponent<MeshRenderer>().materials;
        matArrays[0] = patternMaterial;
        patternSign.GetComponent<MeshRenderer>().materials = matArrays;
    }
   
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("Nail"))
        {
            Material[] matArrays = other.gameObject.GetComponent<MeshRenderer>().materials;
            matArrays[ColorManager.NAIL_PATTERN_INDEX] = patternMaterial;
            other.gameObject.GetComponent<MeshRenderer>().materials = matArrays;
            transform.DOLocalMoveY(standartPosition.y, 0.1f);
            Debug.Log("cikti");
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Nail"))
        {
            transform.DOLocalMoveY(other.transform.position.y,0.1f);
            Debug.Log("girdi");
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
