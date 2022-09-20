using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NailPolishManager : MonoBehaviour
{
    [SerializeField] Material[] Pattern;
    [SerializeField] GameObject Brush;
    [SerializeField] GameObject Nail1;
    [SerializeField] GameObject Nail2;
    [SerializeField] GameObject Nail3;
    [SerializeField] GameObject Nail4;
    [SerializeField] GameObject Nail5;
    [SerializeField] Texture[] myTexture;
    int colorIndex;

    void Start()
    {
        //Find the Standard Shader
        Material myNewMaterial = new Material(Shader.Find("Standard"));
        //Set Texture on the material
        myNewMaterial.SetTexture("_MainTex", myTexture[colorIndex]);

        //if(Collision)
        Brush.GetComponent<MeshRenderer>().material = myNewMaterial;
        Nail1.GetComponent<MeshRenderer>().material = myNewMaterial;
        Nail2.GetComponent<MeshRenderer>().material = myNewMaterial;
        Nail3.GetComponent<MeshRenderer>().material = myNewMaterial;
        Nail4.GetComponent<MeshRenderer>().material = myNewMaterial;
        Nail5.GetComponent<MeshRenderer>().material = myNewMaterial;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
