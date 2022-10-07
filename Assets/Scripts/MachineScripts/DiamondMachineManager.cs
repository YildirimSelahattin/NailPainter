using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class DiamondMachineManager : MonoBehaviour
{
    [SerializeField]public int[] diamondIndexArray = new int[5];
    GameObject nailParent;
    
    // Start is called before the first frame update
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag.Contains("Nail"))
        {
            string currentTag = other.transform.tag;
            int index = currentTag[currentTag.Length - 1] - '0';
            if (diamondIndexArray[index] > -1)
            {
                Debug.Log("sonpi�manl�k");
                GameObject diamondObject = ColorManager.Instance.GetDiamondObjectByIndex(diamondIndexArray[index]);
                GameObject curDiamondObject = Instantiate(diamondObject, other.transform.position,other.transform.rotation);
                curDiamondObject.transform.parent = other.transform;
            }
        }
    }
    
}
