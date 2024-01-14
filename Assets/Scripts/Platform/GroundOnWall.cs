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
        Debug.LogError(collision.gameObject.name + " : " + collision.contacts[1].normal.ToString());
        if (collision.GetContact(0).normal.y > 0)
            gameObject.layer = 0;
        foreach (var i in boxColliders)
            if (i.isTrigger)
                i.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        gameObject.layer = LayerMask.NameToLayer(groundLayer);
        foreach (var i in boxColliders)
            if (i.isTrigger)
                i.enabled = false;
    }
}