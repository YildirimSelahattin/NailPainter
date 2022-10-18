using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CurveAmountManager : MonoBehaviour
{
    [SerializeField] GameObject ground;
    [SerializeField]Material[] allMats;
    float bendX = 0.002f;
    float bendY = 0.002f;

    void OnTriggerEnter(Collider other)
    {
        //Turn right
        if (other.gameObject.CompareTag ("turnRight"))
        {
            bendX=  0.002f;
            TweakCurvesForAllMats();
        }
        //Turn right
        if (other.gameObject.CompareTag("turnLeft"))
        {
            bendX = -0.002f;
            TweakCurvesForAllMats();
        }
        if (other.gameObject.CompareTag("turnDown") )
        {
            bendY = -0.002f;
            TweakCurvesForAllMats();
        }
        if (other.gameObject.CompareTag("turnUp"))
        {
            bendY = 0.002f;
            TweakCurvesForAllMats();
        }


      
    }

    public void TweakCurvesForAllMats()
    {
        foreach(Material m in allMats)
        {
            m.SetFloat(Shader.PropertyToID("CurveY"), bendY);
            m.SetFloat(Shader.PropertyToID("CurveX"), bendX);
        }
    }
}
