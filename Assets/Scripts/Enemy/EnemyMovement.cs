using DG.Tweening;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Vector3 startPosition;
    [SerializeField] private Vector3 endPosition;
    [SerializeField] private float timeSpeed;
    [SerializeField] private float timeDelay;
    [SerializeField] private bool canContainPlayer;

    private Transform origin;

    private void Start()
    {
        Move(startPosition, endPosition);
    }

    /// <summary>
    /// Used to move this transform to end position and change end by start when it move done
    /// </summary>
    /// <param name="startPos">Position start move</param>
    /// <param name="endPos">Position end move</param>
    private void Move(Vector3 startPos, Vector3 endPos)
    {
        transform.DOMove(endPos, timeSpeed).SetDelay(timeDelay).OnComplete(() => Move(endPos, startPos));
    }

    /// <summary>
    /// Use to check if player in here set parent for player is this
    /// </summary>
    /// <param name="collision">Collider of object collision</param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (canContainPlayer && collision.gameObject.CompareTag("Player"))
        {
            origin = collision.gameObject.transform.parent;

            if (Mathf.Abs(collision.GetContact(0).normal.x) != 1)
                collision.gameObject.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // Reassign parent for player when player not in this
        if (canContainPlayer && collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.SetParent(origin);
        }
    }

    private void OnDisable()
    {
        DOTween.Kill(transform);
    }

    private void OnDestroy()
    {
        DOTween.Kill(transform);
    }
}