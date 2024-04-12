using UnityEngine;
using UnityEngine.UI;

public class GuideManager : MonoBehaviour
{
    [SerializeField] private GameObject guideObject;
    [SerializeField] private Text guideTextUI;
    [SerializeField] private string textGuide;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            guideObject.SetActive(true);
            guideTextUI.text = textGuide;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            guideObject.SetActive(false);
    }
}