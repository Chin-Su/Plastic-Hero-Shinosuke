using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class HomeController : MonoBehaviour
{
    [SerializeField] private Slider sound;
    [SerializeField] private Slider music;
    [SerializeField] private GameObject contentSetting;
    [SerializeField] private GameObject settingDialog;

    private void Update()
    {
        if (settingDialog.activeInHierarchy)
        {
            PlayerPrefs.SetFloat("sound", sound.value);
            PlayerPrefs.SetFloat("music", music.value);
            SoundManager.Instance.ChangeSound();
            SoundManager.Instance.ChangeMusic();
        }
    }

    public void OpenSetting()
    {
        SoundManager.Instance.Play(GameManager.Instance.buttonClickSound);
        settingDialog.SetActive(true);
        contentSetting.transform.DOScale(1, 0.5f).SetEase(Ease.OutBack);
        sound.value = PlayerPrefs.GetFloat("sound");
        music.value = PlayerPrefs.GetFloat("music");
    }

    public void CloseSetting()
    {
        SoundManager.Instance.Play(GameManager.Instance.buttonClickSound);

        contentSetting.transform.DOScale(0, 0.5f).SetEase(Ease.InBack).OnComplete(() =>
        {
            settingDialog.SetActive(false);
        });
    }
}