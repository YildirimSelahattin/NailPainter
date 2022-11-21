using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.ComponentModel.Design;

public class MovingPolishManager : MonoBehaviour
{
    [SerializeField] Vector3 targetCameraPosition;
    [SerializeField] Vector3 targetCameraRotation;
    // Start is called before the first frame update
    [SerializeField] float speed;
    bool startMovement = false;
    [SerializeField] GameObject camera;
    Vector3 startPos;
    Vector3 endPos;
    [SerializeField] GameObject colorableObjectsParent;
    float journeyLength;
    float startTime;    // Keep a note of the time the movement started.

    // Start is called before the first frame update
    void Start()
    {
        transform.position = colorableObjectsParent.transform.GetChild(0).transform.position + 3 * Vector3.forward;
        startPos = transform.position;
        startTime = Time.time;
        int index = ((int)GameManager.Instance.matchRate / 5) - 1;
        endPos = colorableObjectsParent.transform.GetChild(index).transform.position;
        journeyLength = Vector3.Distance(startPos, endPos);

        camera.transform.DOLocalMove(targetCameraPosition, 1f).OnComplete(() => transform.DOLocalMove(endPos, 10).OnComplete(() => CloseBrushAndOpenRewarPanel()));
        camera.transform.DOLocalRotate(targetCameraRotation, 1f);
        transform.DOLocalRotate(new Vector3(transform.localRotation.x, transform.localRotation.y, -90f), 0.1f);
    }

    private void CloseBrushAndOpenRewarPanel()
    {
        GameManager.Instance.DisableMovingPolish();
        UIManager.Instance.OpenRewardPanel();
    }
}
