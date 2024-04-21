using Cinemachine;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraController : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] private float minPosX;

    [SerializeField] private float maxPosX;
    [SerializeField] private float minPosY;
    [SerializeField] private float maxPosY;
    [SerializeField] private float aheadDistance;
    [SerializeField] private float cameraSpeed;

    [Header("Target")]
    [SerializeField] private Transform player;

    private void Start()
    {
        this.RegisterListener(EventId.ChangeTimeMoveCam, (param) => { ChangeTimeMoveCam((float)param); });
    }

    private float lookAhead;

    private void Update()
    {
        // Calculate pos x and pos y of camera
        float xPos = Mathf.Clamp(player.position.x + lookAhead, minPosX, maxPosX);
        float yPos = Mathf.Clamp(player.position.y + 1.0f, minPosY, maxPosY);

        // Move camera
        transform.position = new Vector3(xPos, yPos, transform.position.z);
        lookAhead = Mathf.Lerp(lookAhead, (aheadDistance * Mathf.Sign(player.localScale.x)), Time.deltaTime * cameraSpeed);
    }

    private void ChangeTimeMoveCam(float timeMove)
    {
        Camera.main.GetComponent<CinemachineBrain>().m_DefaultBlend.m_Time = timeMove;
    }
}