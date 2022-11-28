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
        gameObject.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material = ColorManager.Instance.GetPatternSignMaterialByIndex(patternIndex);
        //standartPosition = transform.position;
        patternMaterial = ColorManager.Instance.GetPatternMaterialByIndex(patternIndex, false);
        thumbPatternMaterial = ColorManager.Instance.GetPatternMaterialByIndex(patternIndex, true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag.Contains("Nail"))
        {
            Material materialToAdd;
            string currentTag = other.transform.tag;
            int index = currentTag[currentTag.Length - 1] - '0';
            // this if else block is for tiling purposes
            if (index == 0)
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
            GameManager.Instance.currentPatternIndexArray[index] = patternIndex;
            if (GameDataManager.Instance.playSound == 1)
            {
                GameObject sound = new GameObject("sound");
                sound.AddComponent<AudioSource>().PlayOneShot(GameDataManager.Instance.patternMachineSound);
                Destroy(sound, GameDataManager.Instance.patternMachineSound.length);// Creates new object, add to it audio source, play sound, destroy this object after playing is done
            }
            if (patternIndex == GameManager.Instance.currentLevel.nailPatternArray[index])
            {
                UIManager.Instance.CreateCelebrationPopUp();
            }
        }
    }
}