using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurveAmountManager : MonoBehaviour
{
    [SerializeField] GameObject ground;
    Material groundMat;

    void Start()
    {
        groundMat = ground.GetComponent<MeshRenderer>().material;
    }

    void OnTriggerEnter(Collider collision)
    {
        //Turn right
        if (collision.gameObject.tag == "turnRight")
        {
            groundMat.SetFloat(Shader.PropertyToID("CurveX"), -0.002f);
        }
        //Turn right
        if (collision.gameObject.tag == "turnLeft")
        {
            groundMat.SetFloat(Shader.PropertyToID("CurveX"), -0.002f);
        }
        if (collision.gameObject.tag == "turnDown")
        {
            groundMat.SetFloat(Shader.PropertyToID("CurveY"), -0.002f);
        }
        if (collision.gameObject.tag == "turnUp")
        {
            groundMat.SetFloat(Shader.PropertyToID("CurveY"), -0.002f);
        }
    }
}
