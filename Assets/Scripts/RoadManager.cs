using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadManager : MonoBehaviour
{
    [SerializeField] GameObject ground;
    Material groundMat;

    void Start()
    {
        groundMat = ground.GetComponent<MeshRenderer>().material;
    }

    void OnTriggerEnter(Collider collision)
    {
        //Check for a match with the specified name on any GameObject that collides with your GameObject
        if (collision.gameObject.tag == "turnRight")
        {
            groundMat.SetFloat(Shader.PropertyToID("CurveX"), -0.002f);
        }
    }
}
