using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CurveAmountManager : MonoBehaviour
{
    [SerializeField] Material ground;
    public List<Material> materialArray;
    float bendX = 0;
    float bendY = 0;
    public static CurveAmountManager Instance;
    // Instantiate this before all other code 
    private void Awake()
    {

        if(Instance == null)
        {
            Instance = this;
        }
    }
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
        foreach(Material m in materialArray)
        {
            m.SetFloat(Shader.PropertyToID("CurveY"), bendY*3);
            m.SetFloat(Shader.PropertyToID("CurveX"), bendX*3);
        }
        ground.SetFloat(Shader.PropertyToID("CurveX"), bendX);
        ground.SetFloat(Shader.PropertyToID("CurveY"), bendX);
    }
}
