using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoBehaviour
{
    [SerializeField] Texture[] colorsArray;
    [SerializeField] Texture[] patternsArray;
    [SerializeField] Texture[] patternsColorArray;
    public static ColorManager Instance;
    Shader  transParentShader;
    int colorIndex;

    public static int NAIL_COLOR_INDEX=0;
    public static int NAIL_PATTERN_INDEX = 1;
    public static int NAIL_PATTERN_COLOR_INDEX = 2;

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
        Material myNewPatternMaterial = new Material(Shader.Find("Legacy Shaders/Transparent/Cutout/Diffuse"));
        //Set Texture on the mater
        //Set Texture on the material
        myNewPatternMaterial.SetTexture("_MainTex", patternsArray[patternIndex]);
        myNewPatternMaterial.SetTextureOffset("_MainTex", new Vector2(0.5f, 0.5f));      
        myNewPatternMaterial.DOTiling(new Vector2(2f, 2f), 0.1f);
        return myNewPatternMaterial;
    }

    public Material GetPatternColorMaterialByIndex(int patternColorIndex)
    {
        Material myNewPatternColorMaterial = new Material(Shader.Find("Legacy Shaders/Transparent/Cutout/Diffuse"));
        //Set Texture on the mater
        //Set Texture on the material
        myNewPatternColorMaterial.SetTexture("_MainTex", patternsColorArray[patternColorIndex]);
        myNewPatternColorMaterial.SetTextureOffset("_MainTex", new Vector2(0.5f, 0.5f));
        myNewPatternColorMaterial.DOTiling(new Vector2(2f, 2f), 0.1f);
        return myNewPatternColorMaterial;
    }
}