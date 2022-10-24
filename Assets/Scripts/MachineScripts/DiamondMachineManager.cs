using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class DiamondMachineManager : MonoBehaviour
{
    public static int[] diamondIndexArray = new int[5];
    GameObject nailParent;

    private void OnTriggerExit(Collider other)
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
    }

}
