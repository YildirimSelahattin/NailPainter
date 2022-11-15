using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.ComponentModel.Design;

public class MovingPolishManager : MonoBehaviour
{
    [SerializeField]Vector3 targetCameraPosition;
    [SerializeField]Vector3 targetCameraRotation;
    // Start is called before the first frame update
    [SerializeField] float speed;
    bool startMovement = false;
    [SerializeField] GameObject camera;
    Vector3 startPos;
    Vector3 endPos;
    [SerializeField]GameObject colorableObjectsParent;
    float journeyLength;
    float startTime;    // Keep a note of the time the movement started.
    // Start is called before the first frame update
    void Start()
    {
        transform.position = colorableObjectsParent.transform.GetChild(0).transform.position + 3 * Vector3.forward;
        startPos = transform.position;
        startTime = Time.time;
        int index = (int)GameManager.Instance.matchRate / 2;
        endPos = colorableObjectsParent.transform.GetChild(10).transform.position;
        journeyLength = Vector3.Distance(startPos, endPos);
        camera.transform.DOLocalMove(targetCameraPosition, 1f).OnComplete(()=>transform.DOLocalMoveZ(endPos.z,10).OnComplete(() => CloseBrushAndOpenRewarPanel()));
        camera.transform.DOLocalRotate(targetCameraRotation, 1f);
      
    }

    // Update is called once per frame
    void Update()
    {

        /*,        if (startMovement == true)
        {


            // Distance moved equals elapsed time times speed..
            float distCovered = (Time.time - startTime) * speed;

            // Fraction of journey completed equals current distance divided by total distance.
            float fractionOfJourney = distCovered / journeyLength;

            // Set our position as a fraction of the distance between the markers.
            transform.position = Vector3.Lerp(startPos, endPos, fractionOfJourney);
        }*/
    }
    private void CloseBrushAndOpenRewarPanel()
    {
        GameManager.Instance.DisableMovingPolish();
        UIManager.Instance.OpenRewardPanel();

    }
}
