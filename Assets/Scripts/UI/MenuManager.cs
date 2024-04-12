using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject content;

    private void Awake()
    {
        this.RegisterListener(EventId.InitMenu, (param) => Init());
        Debug.Log("Call init menu in awake!");
    }

    private void Start()
    {
        this.RegisterListener(EventId.InitMenu, (param) => Init());
        Debug.Log("Call init menu in start!");
    }

    /// <summary>
    /// Used to set content for menu
    /// </summary>
    private void Init()
    {
        content.transform.DOScale(1, 0.3f).SetEase(Ease.OutBack).OnComplete(() =>
        {
            DOTween.Kill(content.transform);
            Time.timeScale = 0f;
        });
    }

    /// <summary>
    /// Used to handle animation for continue button
    /// </summary>
    public void Continue()
    {
        SoundManager.Instance.Play(GameManager.Instance.buttonClickSound);
        Time.timeScale = 1;
        content.transform.DOScale(0, 0.3f).SetEase(Ease.InBack).OnComplete(() =>
        {
            DOTween.Kill(content.transform);
            gameObject.SetActive(false);
            this.PostEvent(EventId.Continue);
        });
    }

    /// <summary>
    /// Handle function back to home
    /// </summary>
    public void Home()
    {
        SoundManager.Instance.Play(GameManager.Instance.buttonClickSound);
        Time.timeScale = 1;
        content.transform.DOScale(0, 0.3f).SetEase(Ease.InBack).OnComplete(() =>
        {
            DOTween.Kill(content.transform);
            gameObject.SetActive(false);
            this.PostEvent(EventId.UnLockPlayer);
            SceneManager.LoadScene("Home");
        });
    }

    /// <summary>
    /// Handle function restart
    /// </summary>
    public void Restart()
    {
        SoundManager.Instance.Play(GameManager.Instance.buttonClickSound);
        Time.timeScale = 1;
        content.transform.DOScale(0, 0.3f).SetEase(Ease.InBack).OnComplete(() =>
        {
            DOTween.Kill(content.transform);
            gameObject.SetActive(false);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        });
    }
}