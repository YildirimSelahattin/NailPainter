using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoBehaviour
{
    [SerializeField] Material[] Pattern;
    [SerializeField] GameObject Brush;
    [SerializeField] Texture[] myColor;
    [SerializeField] Texture[] myPattern;
    [SerializeField] GameObject[] nailsArray;
    public static ColorManager Instance;
    int colorIndex;

    public static int NAIL_COLOR_INDEX=0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public Material GetColorMaterialByIndex(int colorIndex)
    {
        //Find the Standard Shader
        Material myNewColorMaterial = new Material(Shader.Find("Standard"));
        //Set Texture on the material
        myNewColorMaterial.SetTexture("_MainTex", myColor[colorIndex]);
        //Find the Standard Shader
        return myNewColorMaterial;
    }

    public Material GetPatternMaterialByIndex(int patternIndex)
    {
        Material myNewPatternMaterial = new Material(Shader.Find("Transparent/Diffuse"));
        //Set Texture on the material
        myNewPatternMaterial.SetTexture("_MainTex", myPattern[patternIndex]);
        return myNewPatternMaterial;
    }
}
