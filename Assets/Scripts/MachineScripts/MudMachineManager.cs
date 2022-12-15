using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MudMachineManager : MonoBehaviour
{
    GameObject UIMudParent;
    [SerializeField] Material MudMaterial;
    [SerializeField] ParticleSystem mudParticle;
    private void Start()
    {
        UIMudParent = UIManager.Instance.mudParent;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Hand"))
        {
            Material[] matArray = other.gameObject.GetComponent<MeshRenderer>().materials;
            matArray[1] = MudMaterial;
            other.gameObject.GetComponent<MeshRenderer>().materials = matArray;
            GameManager.Instance.isCleaned = false;

            if (GameDataManager.Instance.playSound == 1)
            {
                GameObject sound = new GameObject("sound");
                sound.AddComponent<AudioSource>().PlayOneShot(GameDataManager.Instance.mudMachineSound);
                Destroy(sound, GameDataManager.Instance.mudMachineSound.length); // Creates new object, add to it audio source, play sound, destroy this object after playing is done
            }
            foreach(Transform child in UIMudParent.transform)
            {
                child.gameObject.SetActive(true);
            }
            //mud particle
            Instantiate(mudParticle, new Vector3(other.transform.position.x, other.transform.position.y + 1, other.transform.position.z + 5), other.transform.rotation);
        }
        UIManager.Instance.CreateBadPopUp();
    }
}
