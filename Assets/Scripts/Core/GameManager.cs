using DG.Tweening;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Player properties")]
    [SerializeField] private PlayerMovement playerMovement;

    [SerializeField] private Rigidbody2D playerRigidbody;
    [SerializeField] private PlayerAttacks playerAttacks;

    [Header("UI Component")]
    [SerializeField] private GameObject menu;

    [SerializeField] private GameObject pauseButton;

    private void Start()
    {
        this.RegisterListener(EventId.UnLockPlayer, (param) => UnLockPlayer());
        this.RegisterListener(EventId.LockPlayer, (param) => LockPlayer());
        this.RegisterListener(EventId.Continue, (param) => Continue());
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

    /// <summary>
    /// Used to continue the game
    /// </summary>
    private void Continue()
    {
        UnLockPlayer();
        pauseButton.transform.DOScale(1, 0.2f).SetEase(Ease.OutBack).SetDelay(0.1f).OnComplete(() => DOTween.Kill(pauseButton.transform));
    }

    /// <summary>
    /// Used to pause the game
    /// </summary>
    public void Pause()
    {
        LockPlayer();
        menu.SetActive(true);
        pauseButton.transform.DOScale(0, 0.2f).SetEase(Ease.InBack).OnComplete(() => DOTween.Kill(pauseButton.transform));
        this.PostEvent(EventId.InitMenu);
    }
}