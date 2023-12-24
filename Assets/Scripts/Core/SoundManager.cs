using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;
    private AudioSource audioSource;

    public static SoundManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject singletonObject = new GameObject("Sound Manager");
                singletonObject.AddComponent<SoundManager>();
            }

            return instance;
        }
        private set { }
    }

    private void Awake()
    {
        if (instance != null && instance.GetInstanceID() != this.GetInstanceID())
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            gameObject.AddComponent<AudioSource>();
            audioSource = GetComponent<AudioSource>();
        }
    }

    public void Play(AudioClip audioClip)
    {
        audioSource.PlayOneShot(audioClip);
    }
}