using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class ColorManager : MonoBehaviour
{
    [SerializeField] Sprite[] colorsArray;
    [SerializeField] Sprite[] patternsArray;
    //[SerializeField] Texture[] patternsColorArray;
    [SerializeField] Sprite[] diamondsArray;
    [SerializeField] GameObject[] TargetMaskArray;
    [SerializeField] GameObject[] CurrentMaskArray;
    [SerializeField] Material baseMat;
    [SerializeField] Material patternSignBaseMat;
    public static ColorManager Instance;
    [SerializeField] GameObject targetHand;
    Shader transParentShader;
    int colorIndex;

    public static int NAIL_COLOR_INDEX = 1;
    public static int NAIL_PATTERN_INDEX = 2;
    public static int NAIL_PATTERN_COLOR_INDEX = 3;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Update()
    {

    }
    
    public Material GetColorMaterialByIndex(int colorIndex)
    {
        //Find the Standard Shader
        Material myNewColorMaterial = new Material(Shader.Find("Universal Render Pipeline/Unlit"));
        //Set Texture on the material
        myNewColorMaterial.SetTexture("_BaseMap", colorsArray[colorIndex].texture);

        //Find the Standard Shader
        return myNewColorMaterial;
    }

    public Material GetDiamondMaterialByIndex(int diamondIndex)
    {
        Material myNewDiamondMaterial = new Material(baseMat);
        //Set Texture on the mater
        //Set Texture on the material
        //myNewPatternColorMaterial.SetTexture("_BaseMap", patternsColorArray[patternColorIndex]);
        //myNewPatternColorMaterial.SetFloat("_Surface", 1);
        myNewDiamondMaterial.SetTexture("_BaseMap", diamondsArray[diamondIndex].texture);
        return myNewDiamondMaterial;
    }

    public Material GetPatternSignMaterialByIndex(int index)
    {
        Material myNewPatternMaterial = new Material(patternSignBaseMat);
        myNewPatternMaterial.SetTexture("_MainTex", patternsArray[index].texture);
        return myNewPatternMaterial;
    }

    public Material GetPatternMaterialByIndex(int patternIndex, bool forThumb)
    {
        Material myNewPatternMaterial = new Material(baseMat);
        //Set Texture on the mater
        //Set Texture on the material

        myNewPatternMaterial.SetTexture("_BaseMap", patternsArray[patternIndex].texture);
        //myNewPatternMaterial.SetFloat("_Surface", 1);
        myNewPatternMaterial.SetTextureOffset("_BaseMap", new Vector2(-6.33f, -0.63f));

        if (forThumb == true)
        {
            myNewPatternMaterial.DOTiling(new Vector2(20f, 20f), 0.1f);
        }
        else
        {
            myNewPatternMaterial.DOTiling(new Vector2(20f, 20f), 0.1f);
        }
        return myNewPatternMaterial;
    }

    public void ColorTargetHand(int[] nailColorArray, int[] nailPatternArray, int[] nailDiamondArray)
    {
        //PAİNT EVERY NAİL 
        for (int index = 0; index < 5; index++)
        {
            //color nail
            TargetMaskArray[index].transform.GetChild(0).gameObject.GetComponent<Image>().sprite = ColorManager.Instance.colorsArray[nailColorArray[index]];
            //pattern nail(special if for thumb because tilling)
            TargetMaskArray[index].transform.GetChild(1).gameObject.GetComponent<Image>().sprite = ColorManager.Instance.patternsArray[nailPatternArray[index]];
            //diamond nail
            TargetMaskArray[index].transform.GetChild(2).gameObject.GetComponent<Image>().sprite = ColorManager.Instance.diamondsArray[nailDiamondArray[index]];
        }
    }

    public void ColorCurrentHand(int[] nailColorArray, int[] nailPatternArray, int[] nailDiamondArray)
    {
        for (int index = 0; index < 5; index++)
        {
            //color nail
            CurrentMaskArray[index].transform.GetChild(0).gameObject.GetComponent<Image>().sprite = ColorManager.Instance.colorsArray[GameManager.Instance.currentColorIndexArray[index]];
            //pattern nail(special if for thumb because tilling)
            CurrentMaskArray[index].transform.GetChild(1).gameObject.GetComponent<Image>().sprite = ColorManager.Instance.patternsArray[GameManager.Instance.currentPatternIndexArray[index]];
            //diamond nail
            CurrentMaskArray[index].transform.GetChild(2).gameObject.GetComponent<Image>().sprite = ColorManager.Instance.diamondsArray[GameManager.Instance.currentDiamondIndexArray[index]];
        }
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