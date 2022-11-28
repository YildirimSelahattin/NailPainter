using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WashMachine : MonoBehaviour
{
    public Material transparentMat;
    [SerializeField] Texture soapTexture;
    [SerializeField] Texture spongeTexture;
    [SerializeField] MeshRenderer soapPart;
    [SerializeField] MeshRenderer spongePart;

    private void OnTriggerExit(Collider other)//if one finger pass the brush
    {
        if (other.transform.tag.Contains("Hand"))
        {
            //dustDecal.gameObject.transform.DOLocalMoveZ(0f, 0.5f);
            Material[] matArray = other.gameObject.GetComponent<MeshRenderer>().materials;
            matArray[1] = transparentMat;
            other.gameObject.GetComponent<MeshRenderer>().materials = matArray;
            GameManager.Instance.isCleaned = true;
            if (GameDataManager.Instance.playSound == 1)
            {
                GameObject sound = new GameObject("sound");
                sound.AddComponent<AudioSource>().PlayOneShot(GameDataManager.Instance.washMachineSound);
                Destroy(sound, GameDataManager.Instance.washMachineSound.length); // Creates new object, add to it audio source, play sound, destroy this object after playing is done
            }
        }
    }
}
