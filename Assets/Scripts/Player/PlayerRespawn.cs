using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    private Transform placeRespawn;
    public bool checkRespawn { get; private set; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("CheckPoint"))
        {
            checkRespawn = true;
            placeRespawn = collision.transform;
        }
    }

    public void Respawn()
    {
        this.PostEvent(EventId.Attacked);
        gameObject.transform.position = placeRespawn.position;
    }
}