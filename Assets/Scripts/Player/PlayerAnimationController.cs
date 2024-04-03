using UnityEditor;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] private string walking;
    [SerializeField] private string jumping;
    [SerializeField] private string doubleJumping;
    [SerializeField] private string crashing;
    [SerializeField] private string flying;
    [SerializeField] private string joking;
    [SerializeField] private string climbingUp;
    [SerializeField] private string climbingDown;
    [SerializeField] private string climbing;
    [SerializeField] private string attacking;
    [SerializeField] private string die;
    [SerializeField] private string attacked;
    [SerializeField] private string winner;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        this.RegisterListener(EventId.Walking, (param) => Walking((float)param));
        this.RegisterListener(EventId.Jumping, (param) => Jumping());
        this.RegisterListener(EventId.DoubleJumping, (param) => DoubleJumping((bool)param));
        this.RegisterListener(EventId.Crashing, (param) => Crashing((bool)param));
        this.RegisterListener(EventId.Flying, (param) => Flying());
        this.RegisterListener(EventId.Joking, (param) => Joking());
        this.RegisterListener(EventId.ClimbingUp, (param) => ClimbingUp((float)param));
        this.RegisterListener(EventId.ClimbingDown, (param) => ClimbingDown((float)param));
        this.RegisterListener(EventId.Climbing, (param) => Climbing((bool)param));
        this.RegisterListener(EventId.Attacking, (param) => Attacking());
        this.RegisterListener(EventId.Die, (param) => Die());
        this.RegisterListener(EventId.Attacked, (param) => Attacked());
        this.RegisterListener(EventId.Winner, (param) => Winner());
    }

    private void Walking(float velocity)
    {
        animator.SetFloat(walking, velocity);
    }

    private void Jumping()
    {
        animator.SetTrigger(jumping);
    }

    private void DoubleJumping(bool doubleJump)
    {
        animator.SetBool(doubleJumping, doubleJump);
    }

    private void Crashing(bool crashed)
    {
        animator.SetBool(crashing, crashed);
    }

    private void Flying()
    { }

    private void Joking()
    {
        animator.SetTrigger(joking);
    }

    private void ClimbingUp(float velocity)
    {
        animator.SetFloat(climbingUp, velocity);
    }

    private void ClimbingDown(float velocity)
    {
        animator.SetFloat(climbingDown, velocity);
    }

    private void Climbing(bool climbing)
    {
        animator.SetBool(this.climbing, climbing);
    }

    private void Attacking()
    {
        animator.SetTrigger(attacking);
    }

    private void Die()
    {
        animator.SetTrigger(die);
        this.PostEvent(EventId.LockPlayer);
    }

    private void Attacked()
    {
        animator.SetTrigger(attacked);
    }

    private void Winner()
    {
        animator.SetTrigger(winner);
        this.PostEvent(EventId.LockPlayer);
    }
}