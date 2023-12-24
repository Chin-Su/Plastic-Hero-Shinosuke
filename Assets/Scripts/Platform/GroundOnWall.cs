using UnityEngine;

public class GroundOnWall : MonoBehaviour
{
    [SerializeField] private string groundLayer;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.GetContact(0).normal.y > 0)
            gameObject.layer = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        gameObject.layer = LayerMask.NameToLayer(groundLayer);
    }
}