using UnityEngine;
using UnityEngine.SceneManagement;

public class EndManager : MonoBehaviour
{
    private void Start()
    {
        Timer.Schedule(this, () =>
        {
            SceneManager.LoadScene("Home");
        }, 5.0f);
    }
}