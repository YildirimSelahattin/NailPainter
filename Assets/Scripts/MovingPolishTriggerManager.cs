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
            Instantiate(brushParticle, new Vector3(other.transform.position.x, other.transform.position.y + 1, other.transform.position.z + 5), other.transform.rotation);
            Material[] matArray = other.gameObject.GetComponent<MeshRenderer>().materials;
            matArray = matArray.SkipLast(1).ToArray();
            other.gameObject.GetComponent<MeshRenderer>().materials = matArray;
        }
    }
}
