using UnityEngine;

public class PlayerAttacks : MonoBehaviour
{
    [SerializeField] private Transform firePoint;
    [SerializeField] private float timeCoolDown;
    [SerializeField] private GameObject[] projectles;

    private PlayerMovement playerMovement;
    private float timeCounter;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        timeCounter = 0;
    }

    private void Update()
    {
        if (!playerMovement.IsOnWall() && Input.GetMouseButtonDown(0) && timeCounter < 0)
        {
            Fire();
            timeCounter = timeCoolDown;
        }
        timeCounter -= Time.deltaTime;
    }

    private void Fire()
    {
        GameObject bullet = projectles[FindBullet()];
        bullet.SetActive(true);
        bullet.transform.position = firePoint.position;
        bullet.GetComponent<BulletController>().SetDirection((int)Mathf.Sign(transform.localScale.x));
    }

    private int FindBullet()
    {
        for (int i = 0; i < projectles.Length; i++)
        {
            if (!projectles[i].activeInHierarchy)
                return i;
        }
        return 0;
    }
}