using UnityEngine;
using Cinemachine;
using System.Collections.Generic;

public class CameraManager : MonoBehaviour
{
    private static List<CinemachineVirtualCamera> cameras = new List<CinemachineVirtualCamera>();
    public static CinemachineVirtualCamera activeCamera = null;

    public static bool IsActiveCamera(CinemachineVirtualCamera camera)
    {
        return camera == activeCamera;
    }

    public static void SwitchCamera(CinemachineVirtualCamera newCamera)
    {
        newCamera.Priority = 10;
        activeCamera = newCamera;

        foreach (CinemachineVirtualCamera cam in cameras)
        {
            if (cam != newCamera)
            {
                cam.Priority = 0;
            }
        }
    }

    public static void Register(CinemachineVirtualCamera camera)
    {
        cameras.Add(camera);
    }

    public static void Unregister(CinemachineVirtualCamera camera)
    {
        cameras.Remove(camera);
    }
}