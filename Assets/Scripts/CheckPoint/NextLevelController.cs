using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelController : MonoBehaviour
{
    [SerializeField] private string nextLevel;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            this.PostEvent(EventId.Winner);
            Timer.Schedule(this, () => { SceneManager.LoadScene(nextLevel); }, 3.0f);
            PlayerPrefs.SetString("level", nextLevel);
        }
    }
}