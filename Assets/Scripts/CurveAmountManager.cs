using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CurveAmountManager : MonoBehaviour
{
    [SerializeField] Material ground;
    public List<Material> materialArray;
    float bendX = 0;
    float bendY = 0;
    float targetBendX;
    float targetBendY;
    bool startTurn = false;
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
            targetBendX = 0.002f;
            targetBendY = bendY;
        }
        //Turn right
        if (other.gameObject.CompareTag("turnLeft"))
        {
            targetBendX = -0.002f;
            targetBendY = bendY;
        }
        if (other.gameObject.CompareTag("turnDown") )
        {
            targetBendX = bendX;
            targetBendY = -0.002f;
        }
        if (other.gameObject.CompareTag("turnUp"))
        {
            targetBendX = bendX;
            targetBendY = 0.002f;
        }
        startTurn = true;

      
    }
    private void Update()
    {
        if(startTurn == true)
        {
            if (targetBendX == bendX && targetBendY == bendY)
            {
                startTurn = false;
            }
            bendX=Mathf.Lerp(bendX, targetBendX, 0.1f);
            bendY=Mathf.Lerp(bendY, targetBendY, 0.1f);
            TweakCurvesForAllMats();
            
        }
    }
    public void TweakCurvesForAllMats()
    {
        foreach(Material m in materialArray)
        {
            if (m != null)
            {
                m.SetFloat(Shader.PropertyToID("CurveY"), bendY * 3);
                m.SetFloat(Shader.PropertyToID("CurveX"), bendX * 3);
            }
        }
        ground.SetFloat(Shader.PropertyToID("CurveX"), bendX);
        ground.SetFloat(Shader.PropertyToID("CurveY"), bendY);
    }
    private void OnDisable()
    {
        foreach (Material m in materialArray)
        {
            if (m != null)
            {
                m.SetFloat(Shader.PropertyToID("CurveY"), 0);
                m.SetFloat(Shader.PropertyToID("CurveX"), 0);
            }
        }
        ground.SetFloat(Shader.PropertyToID("CurveX"),0);
        ground.SetFloat(Shader.PropertyToID("CurveY"),0);
    }
}
