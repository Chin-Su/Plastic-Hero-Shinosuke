using UnityEngine;

public class PlayerAttacks : MonoBehaviour
{
    [SerializeField] private Transform firePoint;
    [SerializeField] private float timeCoolDown;
    [SerializeField] private AudioClip playerFire;

    [HideInInspector]
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
        // Check if player not move on wall and can attack
        if (!playerMovement.IsOnWall() && Input.GetKeyDown(KeyCode.Space) && timeCounter < 0)
        {
            Fire();
            timeCounter = timeCoolDown;
        }
        timeCounter -= Time.deltaTime;
    }

    /// <summary>
    /// Handle fire by active bullet and set its direction
    /// </summary>
    private void Fire()
    {
        SoundManager.Instance.Play(playerFire);
        this.PostEvent(EventId.Attacking);
        GameObject bullet = projectles[FindBullet()];
        bullet.SetActive(true);
        bullet.transform.position = firePoint.position;
        bullet.GetComponent<BulletController>().SetDirection((int)Mathf.Sign(transform.localScale.x));
    }

    /// <summary>
    /// Find index of bullet in list can fire
    /// </summary>
    /// <returns>Index of bullet is not active in hierachy</returns>
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