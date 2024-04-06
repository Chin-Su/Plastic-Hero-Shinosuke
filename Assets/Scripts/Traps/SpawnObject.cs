using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    [SerializeField] private GameObject box;
    [SerializeField] private Transform spawnPosition;
    private bool isPlayerStay;

    private void Update()
    {
        if (!isPlayerStay && !box.activeInHierarchy)
        {
            box.SetActive(true);
            box.transform.position = spawnPosition.position;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerStay = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerStay = false;
        }
    }
}