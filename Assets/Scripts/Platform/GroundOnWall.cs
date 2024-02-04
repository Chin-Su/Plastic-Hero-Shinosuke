using UnityEngine;

public class GroundOnWall : MonoBehaviour
{
    [SerializeField] private string groundLayer;
    private BoxCollider2D[] boxColliders;

    private void Awake()
    {
        boxColliders = GetComponents<BoxCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if player collision with ground in bottom, change its layer not is ground
        if (collision.GetContact(0).normal.y > 0)
            gameObject.layer = 0;
        foreach (var i in boxColliders)
            if (i.isTrigger)
                i.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Reassign layer for this is ground
        gameObject.layer = LayerMask.NameToLayer(groundLayer);
        foreach (var i in boxColliders)
            if (i.isTrigger)
                i.enabled = false;
    }
}