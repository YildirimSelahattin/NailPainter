using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MovingPolishTriggerManager : MonoBehaviour
{
    [SerializeField] GameObject brushParticle;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("colorable"))
        {
            Instantiate(brushParticle, other.transform.position + 2 * Vector3.up, other.transform.rotation);
            Material[] matArray = other.gameObject.GetComponent<MeshRenderer>().materials;
            matArray = matArray.SkipLast(1).ToArray();
            other.gameObject.GetComponent<MeshRenderer>().materials = matArray;
        }
    }
}
