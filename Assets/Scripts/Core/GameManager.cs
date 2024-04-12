using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Player properties")]
    [SerializeField] private PlayerMovement playerMovement;

    [SerializeField] private Rigidbody2D playerRigidbody;
    [SerializeField] private PlayerAttacks playerAttacks;

    [Header("UI Component")]
    [SerializeField] private GameObject menu;

    [SerializeField] private GameObject pauseButton;
    [SerializeField] private Text textHeart;
    [SerializeField] private GameObject cardImage;
    [SerializeField] private GameObject buttonNewGame;

    [Header("Sound")]
    public AudioClip buttonClickSound;

    private static GameManager instance;
    private int gameOver = 1;
    private int card;

    public static GameManager Instance
    {
        get => instance;
        private set { }
    }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        this.RegisterListener(EventId.UnLockPlayer, (param) => UnLockPlayer());
        this.RegisterListener(EventId.LockPlayer, (param) => LockPlayer());
        this.RegisterListener(EventId.Continue, (param) => Continue());

        if (cardImage)
            cardImage.SetActive(false);

        if (buttonNewGame)
            buttonNewGame.SetActive(PlayerPrefs.GetInt("inProgress", 0) == 1);
    }

    /// <summary>
    /// Used to lock, not allow player move and attack
    /// </summary>
    public void LockPlayer()
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
        if (!IsGameOver)
            UnLockPlayer();
        pauseButton.transform.DOScale(1, 0.2f).SetEase(Ease.OutBack).SetDelay(0.1f).OnComplete(() => DOTween.Kill(pauseButton.transform));
    }

    /// <summary>
    /// Used to pause the game
    /// </summary>
    public void Pause()
    {
        SoundManager.Instance.Play(buttonClickSound);
        LockPlayer();
        menu.SetActive(true);
        pauseButton.transform.DOScale(0, 0.2f).SetEase(Ease.InBack).OnComplete(() => DOTween.Kill(pauseButton.transform));
        this.PostEvent(EventId.InitMenu);
    }

    /*** Test ***/

    public void NewGame()
    {
        PlayerPrefs.SetString("level", "Level_1");
        Play();
    }

    public void Play()
    {
        SoundManager.Instance.Play(buttonClickSound);
        Timer.Schedule(this, () =>
        {
            SceneManager.LoadScene(PlayerPrefs.GetString("level", "Level_1"));
            PlayerPrefs.SetInt("inProgress", 1);
        }, buttonClickSound.length);
    }

    public void Exit()
    {
        SoundManager.Instance.Play(buttonClickSound);
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void EndGame()
    {
    }

    /******************************************/
    // ===== Declare properties in here ===== //
    /******************************************/

    /// <summary>
    /// Used to check game is over
    /// </summary>
    public bool IsGameOver
    {
        get => gameOver <= 0;
        private set { }
    }

    /// <summary>
    /// Used to get step respawn of player
    /// </summary>
    public int GameOver
    {
        get => gameOver;
        set
        {
            gameOver = value;
            textHeart.text = "x" + Mathf.Clamp(gameOver, 0, 1);
        }
    }

    public void SetCard()
    {
        card += 1;
        cardImage.SetActive(true);
    }

    public int GetCard()
    { return card; }
}