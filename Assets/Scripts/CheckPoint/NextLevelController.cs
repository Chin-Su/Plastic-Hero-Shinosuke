using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelController : MonoBehaviour
{
    [SerializeField] private string nextLevel;
    [SerializeField] private AudioClip win;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            this.PostEvent(EventId.Winner);

            if (nextLevel != "" && nextLevel != "End")
                PlayerPrefs.SetString("level", nextLevel);

            SoundManager.Instance.Play(win);
            Timer.Schedule(this, () => { SceneManager.LoadScene(nextLevel); }, win.length);
        }
    }
}