using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StudioUIManager : MonoBehaviour
{
    private float dist;
    private bool dragging = false;
    private Vector3 offset;
    private Transform toDrag;
    Camera m_MainCamera;
    Vector3 camOriginPos;
    void Start()
    {
        m_MainCamera = Camera.main;
        camOriginPos = m_MainCamera.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 v3;

        if (Input.touchCount != 1)
        {
            dragging = false;
            return;
        }

        Touch touch = Input.touches[0];
        Vector3 pos = touch.position;

        //Physics.Raycast(_rayPoint.transform.position, _rayPoint.transform.forward, out _raycastHit, range)

        if (touch.phase == TouchPhase.Began)
        {
            Ray ray = Camera.main.ScreenPointToRay(pos);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                m_MainCamera.transform.DOLocalMove(new Vector3(hit.transform.position.x, 0.6f, -5), 1);
                Debug.Log(hit.collider.name);

            }
            else
            {
                m_MainCamera.transform.DOLocalMove(camOriginPos,0.5f);
            }
        }
    }
}