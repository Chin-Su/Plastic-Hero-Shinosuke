using Cinemachine;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ChangeCameraZoom : MonoBehaviour
{
    [SerializeField] private float timeMoveCam;
    [SerializeField] private List<CinemachineVirtualCamera> cameras;
    [SerializeField] private CinemachineVirtualCamera playerCamera;
    [SerializeField] private GameObject guide;

    public void MoveCamera()
    {
        if (guide)
            guide.SetActive(true);

        this.PostEvent(EventId.ChangeTimeMoveCam, timeMoveCam);
        this.PostEvent(EventId.LockPlayer);

        for (int i = 0; i < cameras.Count; i++)
        {
            int index = i;
            Timer.Schedule(this, () =>
            {
                CameraManager.SwitchCamera(cameras[index]);
            }, index * timeMoveCam + 1.0f);
        }

        Timer.Schedule(this, () =>
        {
            this.PostEvent(EventId.UnLockPlayer);
            this.PostEvent(EventId.ChangeTimeMoveCam, 1.5f);
            CameraManager.SwitchCamera(playerCamera);

            if (guide)
                guide.SetActive(false);
        }, timeMoveCam * cameras.Count + 1);
    }
}