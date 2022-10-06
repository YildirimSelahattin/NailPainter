using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternMachineManager : MonoBehaviour
{
    Material patternMaterial;
    Vector3 standartPosition;
    [SerializeField] GameObject patternSign;
    public int patternIndex;
    public static bool nail1iscollerd = false;
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
        if (other.transform.tag.Contains("Nail"))
        {
            string currentTag = other.transform.tag;
            Material[] matArrays = other.gameObject.GetComponent<MeshRenderer>().materials;
            matArrays[ColorManager.NAIL_PATTERN_INDEX] = patternMaterial;
            nail1iscollerd = true;
            other.gameObject.GetComponent<MeshRenderer>().materials = matArrays;
            GameManager.Instance.currentPatternIndexArray[currentTag[currentTag.Length - 1] - '0'] = patternIndex;
            transform.DOLocalMoveY(standartPosition.y, 0.1f);
            Debug.Log("cikti");
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag.Contains("Nail"))
        {
            transform.DOLocalMoveY(other.transform.position.y, 0.1f);
            Debug.Log("girdi");
        }
    }
}
