using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ColorManager : MonoBehaviour
{
    [SerializeField] Texture[] colorsArray;
    [SerializeField] Texture[] patternsArray;
    //[SerializeField] Texture[] patternsColorArray;
    [SerializeField] Texture[] diamondsArray;
    [SerializeField] Material baseMat;
    public static ColorManager Instance;
    [SerializeField] GameObject targetHand;
    Shader transParentShader;
    int colorIndex;

    public static int NAIL_COLOR_INDEX = 0;
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
        Material myNewColorMaterial = new Material(Shader.Find("Universal Render Pipeline/Lit"));
        //Set Texture on the material
        myNewColorMaterial.SetTexture("_BaseMap", colorsArray[colorIndex]);
        
        //Find the Standard Shader
        return myNewColorMaterial;
    }

    public Material GetPatternMaterialByIndex(int patternIndex, bool forThumb)
    {
        Material myNewPatternMaterial = new Material(baseMat);
        //Set Texture on the mater
        //Set Texture on the material

        myNewPatternMaterial.SetTexture("_BaseMap", patternsArray[patternIndex]);
        //myNewPatternMaterial.SetFloat("_Surface", 1);
        myNewPatternMaterial.SetTextureOffset("_BaseMap", new Vector2(0f, 0f));
        if (forThumb == true)
        {
            myNewPatternMaterial.DOTiling(new Vector2(1.14f, 8.8f), 0.1f);
        }
        else
        {
            myNewPatternMaterial.DOTiling(new Vector2(1.48f, 8.8f), 0.1f);
        }
        return myNewPatternMaterial;
    }

    public Material GetDiamondMaterialByIndex(int diamondIndex)
    {
        Material myNewDiamondMaterial = new Material(baseMat);
        //Set Texture on the mater
        //Set Texture on the material
        //myNewPatternColorMaterial.SetTexture("_BaseMap", patternsColorArray[patternColorIndex]);
        //myNewPatternColorMaterial.SetFloat("_Surface", 1);
        myNewDiamondMaterial.SetTexture("_BaseMap", diamondsArray[diamondIndex]);
        return myNewDiamondMaterial;
    }

    public Material GetPatternMachineMaterialByTexture(int index)
    {
        Material myNewPatternMaterial = new Material(baseMat);
        //Set Texture on the mater
        //Set Texture on the material

        myNewPatternMaterial.SetTexture("_BaseMap", patternsArray[index]);
        //myNewPatternMaterial.SetFloat("_Surface", 1);
        myNewPatternMaterial.SetTextureOffset("_BaseMap", new Vector2(0.44f, 0f));
        myNewPatternMaterial.DOTiling(new Vector2(0.1f, 1.09f), 0.1f);
        return myNewPatternMaterial;
    }

    public void ColorTargetHand(int nailTypeIndex, int[] nailColorArray, int[] nailPatternArray, int[] nailDiamondArray)
    {
        //OPEN THE WANTED NAİL SHAPE
        GameObject nailParent = targetHand.transform.GetChild(nailTypeIndex).gameObject;
        GameObject diamondParent = targetHand.transform.GetChild(0).gameObject;
        nailParent.SetActive(true);
        diamondParent.SetActive(true);
        //PAİNT EVERY NAİL 
        for (int index = 0; index < 5; index++)
        {
            //Debug.Log(nailColorArray[index] + nailPatternArray[index] + nailDiamondArray[index]);
            //get the material array
            Material[] matArrayForNail = nailParent.transform.GetChild(index).gameObject.GetComponent<MeshRenderer>().materials;
            //color nail
            matArrayForNail[NAIL_COLOR_INDEX] = GetColorMaterialByIndex(nailColorArray[index]);
            //pattern nail(special if for thumb because tilling)
            if (index == 0)
            {
                matArrayForNail[NAIL_PATTERN_INDEX] = GetPatternMaterialByIndex(nailPatternArray[index], true);
            }
            else
            {
                matArrayForNail[NAIL_PATTERN_INDEX] = GetPatternMaterialByIndex(nailPatternArray[index], false);
            }
            //give material array back
            nailParent.transform.GetChild(index).gameObject.GetComponent<MeshRenderer>().materials = matArrayForNail;
            //diamond nail
            diamondParent.transform.GetChild(index).gameObject.GetComponent<MeshRenderer>().material = GetDiamondMaterialByIndex(nailDiamondArray[index]);
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