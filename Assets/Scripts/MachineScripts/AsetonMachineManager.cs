using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsetonMachineManager : MonoBehaviour
{
    GameObject UIAcidParent;
    [SerializeField] Material[] matArray;
    [SerializeField] Material[] matArrayForThumb;
    string currentTag;
    GameObject diamondParent;

    private void Start()
    {
        UIAcidParent = UIManager.Instance.acidParent;
    }
    private void OnTriggerExit(Collider other)//if one finger pass the brush
    {
        if (other.transform.tag.Contains("Nail"))
        {
            currentTag = other.transform.tag;
            GameManager.Instance.currentColorIndexArray[currentTag[currentTag.Length - 1] - '0'] = 0;
            GameManager.Instance.currentPatternIndexArray[currentTag[currentTag.Length - 1] - '0'] = 0;
            GameManager.Instance.currentDiamondIndexArray[currentTag[currentTag.Length - 1] - '0'] = 0;

            if(currentTag[currentTag.Length - 1] - '0' == 0)//for thumb
            {
                other.gameObject.GetComponent<MeshRenderer>().materials = matArrayForThumb;
            }
            else//else
            {
                other.gameObject.GetComponent<MeshRenderer>().materials = matArray;
            }
            
            if (GameDataManager.Instance.playSound == 1 )
            {
                GameObject sound = new GameObject("sound");
                sound.AddComponent<AudioSource>().PlayOneShot(GameDataManager.Instance.asetonSound);
                Destroy(sound, GameDataManager.Instance.asetonSound.length); // Creates new object, add to it audio source, play sound, destroy this object after playing is done
            }
            foreach (Transform child in UIAcidParent.transform)
            {
                child.gameObject.SetActive(true);
            }
        }
    }
}
