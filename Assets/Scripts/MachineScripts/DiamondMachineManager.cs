using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class DiamondMachineManager : MonoBehaviour
{
    GameObject diamondParent;
    [SerializeField] ParticleSystem diamondParticles;

    private void Start()
    {
        diamondParent = GameObject.FindGameObjectWithTag("DiamondParrent");
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag.Contains("Nail"))
        {
            string currentTag = other.transform.tag;
            int index = currentTag[currentTag.Length - 1] - '0';
            if (GameManager.Instance.currentLevel.nailDiamondArray[index] != 0)
            {
                diamondParent.transform.GetChild(index).gameObject.SetActive(true);
                diamondParent.transform.GetChild(index).gameObject.GetComponent<MeshRenderer>().material = ColorManager.Instance.GetDiamondMaterialByIndex(GameManager.Instance.currentLevel.nailDiamondArray[index]);
                UIManager.Instance.CreateCelebrationPopUp();
                if (GameDataManager.Instance.playSound == 1)
                {
                    GameObject sound = new GameObject("sound");
                    sound.AddComponent<AudioSource>().PlayOneShot(GameDataManager.Instance.diamondMachineSound);
                    Destroy(sound, GameDataManager.Instance.diamondMachineSound.length); // Creates new object, add to it audio source, play sound, destroy this object after playing is done
                }
            }
            GameManager.Instance.currentDiamondIndexArray[index] = GameManager.Instance.currentLevel.nailDiamondArray[index];
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag.Contains("Nail"))
        {
                if (GameDataManager.Instance.playSound == 1)
                {
                    GameObject sound = new GameObject("sound");
                    sound.AddComponent<AudioSource>().PlayOneShot(GameDataManager.Instance.diamondMachineSound);
                    Destroy(sound, GameDataManager.Instance.diamondMachineSound.length); // Creates new object, add to it audio source, play sound, destroy this object after playing is done
                }
        }
    }
}