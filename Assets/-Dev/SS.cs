using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SS : MonoBehaviour
{
    public string screenShotName = "throwObject";

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.O))
        {
            ScreenCapture.CaptureScreenshot(screenShotName+".png");
        }
    }
}