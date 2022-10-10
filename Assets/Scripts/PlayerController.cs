using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    public float forwardMoveSpeed;
    public float horizontalSpeed;
    public TextMeshProUGUI startText;
    public bool stopForwardMovement = true;
    public bool stopSideMovement = false;
    Vector3 cursor_pos;
    Vector3 start_pos;
    public GameObject EndLine;
    public bool firstTouch = false;
    public enum PLATFORM { PC, MOBILE };
    [SerializeField] PLATFORM platform = PLATFORM.PC;
    public static PlayerController Instance;
    // Start is called before the first frame update
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        if (platform == PLATFORM.PC)
        {
            cursor_pos = Camera.main.ScreenToViewportPoint(Input.mousePosition) * 900; // Instead of getting pixels, we are getting viewport coordinates which is resolution independent
        }
        else
        {
            if (Input.touchCount > 0) cursor_pos = Camera.main.ScreenToViewportPoint(Input.GetTouch(0).position) * 900; // Instead of getting pixels, we are getting viewport coordinates which is resolution independent
        }

        if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) || Input.GetMouseButtonDown(0))
        { // This is actions when finger/cursor hit screen
            if (stopSideMovement == false)
            {
                start_pos = cursor_pos;
                startText.gameObject.SetActive(false);
                firstTouch = true;
            }
        }
        if ((Input.touchCount > 0 && (Input.GetTouch(0).phase == TouchPhase.Stationary || Input.GetTouch(0).phase == TouchPhase.Moved)) || Input.GetMouseButton(0))
        { // This is actions when finger/cursor pressed on screen
            
            if (stopSideMovement == false)
            {
                HorizontalMove(cursor_pos);
            }
        }
        if (((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended) || Input.GetMouseButtonUp(0)))
        { // This is actions when finger/cursor get out from screen
        }

        if (stopForwardMovement == false && firstTouch == true)
        {
           
            transform.position += Vector3.forward * forwardMoveSpeed * Time.deltaTime;//regular go forward
        }

    }
    public void HorizontalMove(Vector3 cursor_pos)
    {
        Vector3 pos = transform.localPosition;
        pos.x = (cursor_pos-start_pos ).x / 80;
        if (pos.x >= 3.50F) { pos.x = 3.50F; }
        if (pos.x <= -3.50F) { pos.x = -3.50F; }
        transform.DOLocalMoveX(pos.x, Time.deltaTime);
        
    }
}