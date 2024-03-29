using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class BrushManager : MonoBehaviour
{
    [SerializeField]GameObject brushParticle;
    public int colorIndex;
    Material brushMaterial;
    
    void Start()
    {
        
        brushMaterial = ColorManager.Instance.GetColorMaterialByIndex(colorIndex);
        
        //paint the brush
        gameObject.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material = brushMaterial;
    }

    private void OnTriggerExit(Collider other)//if one finger pass the brush
    {
        if (other.transform.tag.Contains("Nail"))
        {
            string currentTag = other.transform.tag;
            int index = currentTag[currentTag.Length - 1] - '0';
            Material[] matArray = other.gameObject.GetComponent<MeshRenderer>().materials;
            matArray[ColorManager.NAIL_COLOR_INDEX] = brushMaterial;
            other.gameObject.GetComponent<MeshRenderer>().materials = matArray;
            GameManager.Instance.currentColorIndexArray[index] = colorIndex;
            
            brushParticle.GetComponent<ParticleSystemRenderer>().material = new Material(brushMaterial);
            Instantiate(brushParticle, other.transform.position, other.transform.rotation,other.transform);
            if (colorIndex == GameManager.Instance.currentLevel.nailColorArray[index])
            {
                UIManager.Instance.CreateCelebrationPopUp();
            }
            if (GameDataManager.Instance.playSound == 1)
            {
                GameObject sound = new GameObject("sound");
                sound.AddComponent<AudioSource>().PlayOneShot(GameDataManager.Instance.brushMachineMusic);
                Destroy(sound, GameDataManager.Instance.brushMachineMusic.length); // Creates new object, add to it audio source, play sound, destroy this object after playing is done
            }

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("PlayerBase"))//When HandRigged Gets Triggered Because ColorMachine
        {
            // TODO, 
            /*a
            transform.DORotate(new Vector3(-30, 0, 0), 1);
            Debug.Log("colormachinemove");
            */
        }
    }
}