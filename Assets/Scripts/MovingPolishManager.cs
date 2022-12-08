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
        camera.transform.DOLocalRotate(targetCameraRotation, 1f);
        transform.DOLocalRotate(new Vector3(-30, 180, -90f), 0.1f);
        //206
        camera.transform.GetComponent<Camera>().farClipPlane = 220;
    }

    void Update()
    {
        if (UIManager.Instance.isTapped == true)
        {
            UIManager.Instance.bg.SetActive(false);
            UIManager.Instance.isTapped = false;
            camera.transform.DOLocalMove(targetCameraPosition, 1f).OnComplete(() => transform.DOLocalMove(new Vector3(endPos.x, endPos.y + 1, endPos.z), 10).OnComplete(() => CloseBrushAndOpenRewarPanel()));
        }
    }

    IEnumerator DelayOpenPanel(float second)
    {
        yield return new WaitForSeconds(second);
        UIManager.Instance.diamondMuliplier.SetActive(true);
        UIManager.Instance.bg.SetActive(true);
        UIManager.Instance.minimap.SetActive(false);
    }

    private void CloseBrushAndOpenRewarPanel()
    {
        GameManager.Instance.DisableMovingPolish();
        UIManager.Instance.AfterEndGame();
        StartCoroutine(DelayOpenPanel(1f));
    }
}
