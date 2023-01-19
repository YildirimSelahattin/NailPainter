using UnityEngine;
using UnityEngine.UI;

public class StartScreenRes : MonoBehaviour
{
    public Canvas _Canvas;

    void Start()
    {
        int _width = Display.main.systemWidth;
        int _width2 = Screen.width;

        Debug.Log("Width: " + _width);
        Debug.LogError("Width: " + _width2);

        if (_width > 1060)
        {
            //_Canvas.GetComponent<CanvasScaler>().scaleFactor = 1.2f;
            _Canvas.GetComponent<CanvasScaler>().referenceResolution = new Vector2(1540, 1080);
        }
    }
}
