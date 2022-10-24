using DG.Tweening;
using UnityEngine;

public class ColorManager : MonoBehaviour
{
    [SerializeField] Texture[] colorsArray;
    [SerializeField] Texture[] patternsArray;
    //[SerializeField] Texture[] patternsColorArray;
    [SerializeField] GameObject[] diamondsArray;
    [SerializeField] Material baseMat;
    [SerializeField] Material machineBaseMat;
    public static ColorManager Instance;
    Shader transParentShader;
    int colorIndex;

    public static int NAIL_COLOR_INDEX = 0;
    public static int NAIL_PATTERN_INDEX = 0;
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
        Material myNewColorMaterial = new Material(Shader.Find("Universal Render Pipeline/Lit"));
        //Set Texture on the material
        myNewColorMaterial.SetTexture("_BaseMap", colorsArray[colorIndex]);

        //Find the Standard Shader
        return myNewColorMaterial;
    }

    public Material GetPatternMaterialByIndex(int patternIndex)
    {
        Material myNewPatternMaterial = new Material(baseMat);
        //Set Texture on the mater
        //Set Texture on the material

        myNewPatternMaterial.SetTexture("_BaseMap", patternsArray[patternIndex]);
        //myNewPatternMaterial.SetFloat("_Surface", 1);
        myNewPatternMaterial.SetTextureOffset("_BaseMap", new Vector2(0f, 0f));
        myNewPatternMaterial.DOTiling(new Vector2(1.48f, 8.8f), 0.1f);
        return myNewPatternMaterial;
    }

    public Material GetPatternColorMaterialByIndex(int patternColorIndex)//NOT IN USE
    {
        Material myNewPatternColorMaterial = new Material(baseMat);
        //Set Texture on the mater

        //Set Texture on the material
        //myNewPatternColorMaterial.SetTexture("_BaseMap", patternsColorArray[patternColorIndex]);
        //myNewPatternColorMaterial.SetFloat("_Surface", 1);
        myNewPatternColorMaterial.SetTextureOffset("_BaseMap", new Vector2(0f, 0f));
        myNewPatternColorMaterial.DOTiling(new Vector2(1.48f, 8.8f), 0.1f);
        return myNewPatternColorMaterial;
    }
    public GameObject GetDiamondObjectByIndex(int index)
    {
        return diamondsArray[index];
    }

    public Material GetMachineColorMaterialByTexture(Texture texture)
    {
        Material myMachineColorMaterial = new Material(machineBaseMat);
        //Set Texture on the mater
        //Set Texture on the material

        myMachineColorMaterial.SetTexture("_BaseMap", texture);
        //myNewPatternMaterial.SetFloat("_Surface", 1);
        return myMachineColorMaterial;
    }
   
}


//to color the machine itself

/*
public Material GetPatternMachineMaterialByIndex(int patternIndex)
{
    Material myNewPatternMaterial = new Material(Shader.Find("Transparent/Diffuse"));
    //Set Texture on the material
    myNewPatternMaterial.SetTexture("_MainTex", patternsArray[patternIndex]);
    return myNewPatternMaterial;
}

public Material GetPatternColorMachineMaterialByIndex(int patternColorIndex)
{
    Material myNewPatternColorMaterial = new Material(Shader.Find("Transparent/Diffuse"));
    //Set Texture on the material
    myNewPatternColorMaterial.SetTexture("_MainTex", patternsColorArray[patternColorIndex]);
    return myNewPatternColorMaterial;
}
}
*/
