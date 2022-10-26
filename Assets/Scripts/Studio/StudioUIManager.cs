using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class StudioUIManager : MonoBehaviour
{
    private float dist;
    private bool dragging = false;
    private Vector3 offset;
    private Transform toDrag;
    Camera m_MainCamera;
    Vector3 camOriginPos;
    [SerializeField] GameObject KomodinParrent;
    [SerializeField] GameObject MasaParrent;
    [SerializeField] GameObject PlatformParrent;
    [SerializeField] GameObject DuvarParrent;
    [SerializeField] GameObject ZeminParrent;

    void Start()
    {
        m_MainCamera = Camera.main;
        camOriginPos = m_MainCamera.transform.position;
    }

    public void PrevScene()
    {
        SceneManager.LoadScene(0);
    }

    public void onUpgradeTable()
    {
        Debug.Log("dasdfasdasdfadsf");
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
                if (hit.collider.tag == "ZeminUpgrade")
                {
                    ZeminParrent.transform.GetChild(0).gameObject.SetActive(false);
                    ZeminParrent.transform.GetChild(2).gameObject.SetActive(false);
                    ZeminParrent.transform.GetChild(1).gameObject.SetActive(true);
                }
                if (hit.collider.tag == "KomodinUpgrade")
                {
                    KomodinParrent.transform.GetChild(0).gameObject.SetActive(false);
                    KomodinParrent.transform.GetChild(2).gameObject.SetActive(false);
                    KomodinParrent.transform.GetChild(1).gameObject.SetActive(true);
                }
                if (hit.collider.tag == "DuvarUpgrade")
                {
                    DuvarParrent.transform.GetChild(0).gameObject.SetActive(false);
                    DuvarParrent.transform.GetChild(2).gameObject.SetActive(false);
                    DuvarParrent.transform.GetChild(1).gameObject.SetActive(true);
                }
                if (hit.collider.tag == "PlatformUpgrade")
                {
                    PlatformParrent.transform.GetChild(0).gameObject.SetActive(false);
                    PlatformParrent.transform.GetChild(2).gameObject.SetActive(false);
                    PlatformParrent.transform.GetChild(1).gameObject.SetActive(true);
                }
                if (hit.collider.tag == "MasaUpgrade")
                {
                    MasaParrent.transform.GetChild(0).gameObject.SetActive(false);
                    MasaParrent.transform.GetChild(2).gameObject.SetActive(false);
                    MasaParrent.transform.GetChild(1).gameObject.SetActive(true);
                }
            }
            else
            {
                //m_MainCamera.transform.DOLocalMove(camOriginPos,1f);
            }
        }


    }
}