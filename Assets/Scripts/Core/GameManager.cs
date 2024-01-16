using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Player properties")]
    [SerializeField] private PlayerMovement playerMovement;

    [SerializeField] private Rigidbody2D playerRigidbody;
    [SerializeField] private PlayerAttacks playerAttacks;

    private void Start()
    {
        this.RegisterListener(EventId.UnLockPlayer, (param) => UnLockPlayer());
        this.RegisterListener(EventId.LockPlayer, (param) => LockPlayer());
    }

    /// <summary>
    /// Used to lock, not allow player move and attack
    /// </summary>
    private void LockPlayer()
    {
        playerMovement.enabled = false;
        playerRigidbody.velocity = Vector2.zero;
        playerAttacks.enabled = false;
    }

    /// <summary>
    /// Used to unlock, allow player attack and move
    /// </summary>
    private void UnLockPlayer()
    {
        playerMovement.enabled = true;
        playerAttacks.enabled = true;
    }
}