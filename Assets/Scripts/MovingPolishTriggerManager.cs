using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MovingPolishTriggerManager : MonoBehaviour
{
    [SerializeField] GameObject brushParticle;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("colorable"))
        {
            Instantiate(brushParticle, other.transform.position+2*Vector3.up, other.transform.rotation, other.transform.parent);
            Material[] matArray = other.gameObject.GetComponent<MeshRenderer>().materials;
            matArray= matArray.SkipLast(1).ToArray();
            other.gameObject.GetComponent<MeshRenderer>().materials = matArray;

        }
    }
}
