using System.Collections;
using UnityEngine;

public class Slime : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private float timeBetweenBothFire;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] projectiles;
    [SerializeField] private int amountFire;

    private new Rigidbody2D rigidbody;
    private float timeCounter;
    private EnemyMovement enemyMovement;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        enemyMovement = GetComponent<EnemyMovement>();
    }

    private void Update()
    {
        if (timeCounter < 0)
        {
            StartCoroutine(nameof(Attack));
        }
        timeCounter -= Time.deltaTime;
    }

    private IEnumerator Attack()
    {
        timeCounter = timeBetweenBothFire + attackCooldown;
        enemyMovement.SetSpeed(0);
        for (int i = 0; i < amountFire; i++)
        {
            int index = FindFire();
            projectiles[index].transform.position = firePoint.position;
            projectiles[index].GetComponent<BulletController>().SetDirection((int)Mathf.Sign(transform.localScale.x));
            projectiles[index].SetActive(true);
            yield return new WaitForSeconds(attackCooldown / amountFire);
        }
        enemyMovement.DefaultSpeed();
    }

    private int FindFire()
    {
        for (int i = 0; i < projectiles.Length; ++i)
        {
            if (!projectiles[i].activeInHierarchy)
                return i;
        }
        return 0;
    }
}