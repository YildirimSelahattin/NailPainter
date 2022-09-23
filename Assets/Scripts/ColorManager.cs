using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoBehaviour
{
    [SerializeField] Texture[] colorsArray;
    [SerializeField] Texture[] patternsArray;
    public static ColorManager Instance;
    int colorIndex;

    public static int NAIL_COLOR_INDEX=0;
    public static int NAIL_PATTERN_INDEX = 1;

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
        myNewColorMaterial.SetTexture("_MainTex", colorsArray[colorIndex]);
        //Find the Standard Shader
        return myNewColorMaterial;
    }

    public Material GetPatternMaterialByIndex(int patternIndex)
    {
        Material myNewPatternMaterial = new Material(Shader.Find("Transparent/Diffuse"));
        //Set Texture on the material
        myNewPatternMaterial.SetTexture("_MainTex", patternsArray[patternIndex]);
        return myNewPatternMaterial;
    }
}
