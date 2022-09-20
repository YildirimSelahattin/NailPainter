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
    [SerializeField] Texture[] myColor;
    [SerializeField] Texture[] myPattern;
    int colorIndex;
    public Material test;

    void Start()
    {
        //Find the Standard Shader
        Material myNewColorMaterial = new Material(Shader.Find("Standard"));
        //Set Texture on the material
        myNewColorMaterial.SetTexture("_MainTex", myColor[colorIndex]);
        //Find the Standard Shader
        Material myNewPatternMaterial = new Material(Shader.Find("Transparent/Diffuse"));
        //Set Texture on the material
        myNewPatternMaterial.SetTexture("_MainTex", myPattern[0]);

        //if(Collision)
        Brush.GetComponent<MeshRenderer>().material = myNewColorMaterial;
        Brush.GetComponent<MeshRenderer>().materials[0] = test;

        Nail1.GetComponent<MeshRenderer>().material = myNewColorMaterial;
        Nail2.GetComponent<MeshRenderer>().material = myNewColorMaterial;
        Nail3.GetComponent<MeshRenderer>().material = myNewColorMaterial;
        Nail4.GetComponent<MeshRenderer>().material = myNewColorMaterial;
        Nail5.GetComponent<MeshRenderer>().material = myNewColorMaterial;
    }

    // Update is called once per frame
    void Update()
    {
        Brush.GetComponent<MeshRenderer>().materials[1] = test;
    }
}
