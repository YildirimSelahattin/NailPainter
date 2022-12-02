using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineController : MonoBehaviour
{
    public static CinemachineController Instance;
    public List<CinemachineVirtualCamera> cameras = new List<CinemachineVirtualCamera>();
    public CinemachineVirtualCamera ActiveCam = null;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else { }
    }

    public bool isActiveCam(CinemachineVirtualCamera camera)
    {
        return camera == ActiveCam;
    }

    public void SwitchCam(CinemachineVirtualCamera camera)
    {
        camera.Priority = 10;
        ActiveCam = camera;

        foreach (CinemachineVirtualCamera c in cameras)
            if (c != camera && c.Priority != 0)
            {
                c.Priority = 0;
            }
    }

    public void Register(CinemachineVirtualCamera camera)
    {
        cameras.Add(camera);
    }
    public void UnRegister(CinemachineVirtualCamera camera)
    {
        cameras.Remove(camera);
    }
}
