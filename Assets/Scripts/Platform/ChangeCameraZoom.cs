using Cinemachine;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCameraZoom : MonoBehaviour
{
    [SerializeField] private float timeMoveCam;
    [SerializeField] private List<CinemachineVirtualCamera> cameras;
    [SerializeField] private CinemachineVirtualCamera playerCamera;

    public void MoveCamera()
    {
        this.PostEvent(EventId.ChangeTimeMoveCam, timeMoveCam);
        this.PostEvent(EventId.LockPlayer);

        for (int i = 0; i < cameras.Count; i++)
        {
            Timer.Schedule(this, () =>
            {
                CameraManager.SwitchCamera(cameras[i]);
            }, i * timeMoveCam);
        }

        Timer.Schedule(this, () =>
        {
            this.PostEvent(EventId.UnLockPlayer);
            this.PostEvent(EventId.ChangeTimeMoveCam, 1.5f);
            CameraManager.SwitchCamera(playerCamera);
        }, timeMoveCam * cameras.Count);
    }
}