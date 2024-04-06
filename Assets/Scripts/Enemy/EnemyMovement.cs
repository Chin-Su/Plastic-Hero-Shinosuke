using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    // Speed of object move
    [SerializeField] private float speed;

    // Set for object is move follow horizontal or vertical
    [SerializeField] private bool isHorizontal;

    // Variable to store min point to move and max point
    [SerializeField] private float minEdge;

    [SerializeField] private float maxEdge;

    // Variable to check object move to min position
    [SerializeField] private bool isMoveMin;

    [SerializeField] private bool useRigidbody;

    private new Rigidbody2D rigidbody;
    private float defaultSpeed;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();

        defaultSpeed = speed;
    }

    private void Update()
    {
        // Check and handle method move similar
        if (isHorizontal)
        {
            if (isMoveMin)
            {
                if (transform.localPosition.x > minEdge)
                {
                    if (useRigidbody)
                    {
                        rigidbody.velocity = new Vector2(-speed, rigidbody.velocity.y);
                        transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, 1);
                    }
                    else
                        transform.Translate(new Vector3(-Time.deltaTime * speed, 0, 0), Space.World);
                }
                else
                    isMoveMin = false;
            }
            else
            {
                if (transform.localPosition.x < maxEdge)
                {
                    if (useRigidbody)
                    {
                        rigidbody.velocity = new Vector2(speed, rigidbody.velocity.y);
                        transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, 1);
                    }
                    else
                        transform.Translate(new Vector3(Time.deltaTime * speed, 0, 0), Space.World);
                }
                else
                    isMoveMin = true;
            }
        }
        else
        {
            if (isMoveMin)
            {
                if (transform.localPosition.y > minEdge)
                {
                    transform.Translate(new Vector3(0, -Time.deltaTime * speed, 0), Space.World);
                }
                else
                    isMoveMin = false;
            }
            else
            {
                if (transform.localPosition.y < maxEdge)
                {
                    transform.Translate(new Vector3(0, Time.deltaTime * speed, 0), Space.World);
                }
                else
                    isMoveMin = true;
            }
        }
    }

    public void SetSpeed(float _speed)
    {
        speed = _speed;
    }

    public void DefaultSpeed()
    {
        speed = defaultSpeed;
    }
}