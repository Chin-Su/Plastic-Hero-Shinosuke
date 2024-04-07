using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;
    private AudioSource audioSource;
    public AudioSource musicSound;

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

    private void Start()
    {
        ChangeMusic();
        ChangeSound();
    }

    public void Play(AudioClip audioClip)
    {
        audioSource.PlayOneShot(audioClip);
    }

    public void ChangeSound()
    {
        audioSource.volume = PlayerPrefs.GetFloat("sound", 1);

        Debug.Log("Sound: " + PlayerPrefs.GetFloat("sound", 1));
    }

    public void ChangeMusic()
    {
        musicSound.volume = PlayerPrefs.GetFloat("music", 1);
    }
}