using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class DiamondMachineManager : MonoBehaviour
{
    public static int[] diamondIndexArray = new int[5];
    int planeChildIndex = 1;
    GameObject nailParent;
    GameObject diamondParent;
    private void Start()
    {
        diamondParent = GameObject.FindGameObjectWithTag("DiamondParrent");
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag.Contains("Nail"))
        {
            string currentTag = other.transform.tag;
            int index = currentTag[currentTag.Length - 1] - '0';
            if (diamondIndexArray[index] > 0)
            {
                diamondParent.transform.GetChild(index).gameObject.SetActive(true);
                diamondParent.transform.GetChild(index).gameObject.GetComponent<MeshRenderer>().material = ColorManager.Instance.GetDiamondMaterialByIndex(diamondIndexArray[index]);
                GameManager.Instance.currentDiamondIndexArray[index] = diamondIndexArray[index];
            }
            //0.00011
        }
    }

}
//OLD DİAMOND MACHİNE CODE, ADDS 3D OBJECT
/*private void OnTriggerExit(Collider other)
{
    if (other.transform.tag.Contains("Nail"))
    {
        string currentTag = other.transform.tag;
        int index = currentTag[currentTag.Length - 1] - '0';
        if (diamondIndexArray[index] > -1)
        {
            GameObject diamondObject = ColorManager.Instance.GetDiamondObjectByIndex(diamondIndexArray[index]);
            Vector3 vect = other.transform.position;
            Debug.Log(vect);
            GameObject curDiamondObject = Instantiate(diamondObject, vect, other.transform.rotation);
            curDiamondObject.transform.parent = other.transform;
            GameManager.Instance.currentDiamondIndexArray[index] = diamondIndexArray[index];
        }
        //0.00011
    }
}*/