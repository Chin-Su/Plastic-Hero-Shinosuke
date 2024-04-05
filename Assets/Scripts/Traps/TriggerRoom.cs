using UnityEngine;

public class TriggerRoom : MonoBehaviour
{
    [SerializeField] private GameObject[] doors;
    [SerializeField] private GameObject enemy;

    private bool check;

    private void Update()
    {
        if (!enemy.activeInHierarchy)
        {
            OpenDoors();
            check = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!check && enemy.activeInHierarchy)
            {
                check = true;
                ClosedDoors();
            }
        }
    }

    private void ClosedDoors()
    {
        foreach (var door in doors)
            door.GetComponent<Animator>().SetTrigger("isClosed");
    }

    private void OpenDoors()
    {
        foreach (var door in doors)
            door.GetComponent<Animator>().SetTrigger("isOpened");
    }
}