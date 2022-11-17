using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateFurniture : MonoBehaviour
{
    // Update is called once per frame
    //Reward Panelde Furniture'larin dönmesi icin 
    void Update()
    {
        transform.Rotate(0, 50 * Time.deltaTime, 0);
    }
}
