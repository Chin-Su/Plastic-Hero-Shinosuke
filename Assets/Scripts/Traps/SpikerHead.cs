using UnityEngine;

public class SpikerHead : EnemyMakeDamage
{
    [Header("Spkierhead Attributes")]
    [SerializeField] private float speed;

    [SerializeField] private float range;
    [SerializeField] private float checkDelay;
    [SerializeField] private LayerMask playerLayer;

    private Vector3[] directions = new Vector3[2];
    private float checkTimer;
    private Vector3 destination;

    private bool attacking;

    private void OnEnable()
    {
        Stop();
    }

    private void Update()
    {
        // Check if this object is attacking, move it
        if (attacking)
            transform.Translate(speed * Time.deltaTime * destination);
        else
        {
            // If not, check player
            checkTimer += Time.deltaTime;
            if (checkTimer > checkDelay)
                CheckForPlayer();
        }
    }

    private void CheckForPlayer()
    {
        CalculateDirections();

        for (int i = 0; i < directions.Length; i++)
        {
            Debug.DrawRay(transform.position, directions[i], Color.red);
            // Create rayc cast to find player follow fourth direction
            RaycastHit2D raycastHit = Physics2D.Raycast(transform.position, directions[i], range, playerLayer);

            // If find player, attacking
            if (raycastHit.collider != null && !attacking)
            {
                attacking = true;
                destination = directions[i];
                checkTimer = 0;
            }
        }
    }

    private void Stop()
    {
        destination = transform.position;
        attacking = false;
    }

    private void CalculateDirections()
    {
        directions[0] = transform.right * range; // Right direction
        directions[1] = -transform.right * range; // Left direction
        //directions[2] = transform.up * range; // Up direction
        //directions[3] = -transform.up * range; // Down direction
    }

    private new void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (!collision.CompareTag("Bullet"))
        {
            Stop();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
            Stop();
    }
}