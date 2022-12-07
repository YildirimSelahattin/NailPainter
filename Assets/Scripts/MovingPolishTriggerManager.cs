using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class MovingPolishTriggerManager : MonoBehaviour
{
    [SerializeField] GameObject brushParticle;
    GameObject tempSound;
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("colorable"))
        {
            Instantiate(brushParticle, new Vector3(other.transform.position.x, other.transform.position.y + 1, other.transform.position.z + 5), other.transform.rotation);
            Material[] matArray = other.gameObject.GetComponent<MeshRenderer>().materials;
            matArray = matArray.SkipLast(1).ToArray();
            other.gameObject.GetComponent<MeshRenderer>().materials = matArray;
            if (GameDataManager.Instance.playSound == 1)
            {
                GameObject sound = new GameObject("sound");
                tempSound = sound;
                sound.AddComponent<AudioSource>().PlayOneShot(GameDataManager.Instance.lastAnimationSound);
                
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("colorable"))
        {
            if(tempSound.IsDestroyed()==false)
            {
                tempSound.AddComponent<AudioSource>().PlayOneShot(GameDataManager.Instance.lastAnimationSound);
            }
        }
    }
}
