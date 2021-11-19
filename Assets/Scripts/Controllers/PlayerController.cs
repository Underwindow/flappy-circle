using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviourSingleton<PlayerController>, IPauseEventListener
{
    [SerializeField] private float movingSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private AudioClip jumpSound;

    private Rigidbody2D rb;
    private Vector2 lastVelocity;
    private Vector2 updateVelocity;

    public delegate void PlayerPassWallEvent();
    public event PlayerPassWallEvent OnPlayerPassWall;
    public delegate void PlayerHitWallEvent();
    public event PlayerHitWallEvent OnPlayerHitWall;

    public Vector2 PlayerPos => transform.position;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.simulated = true;

        OnPlayerPassWall += Score.Instance.Collect;
    }
    private void FixedUpdate()
    {
        updateVelocity = Vector2.right * movingSpeed;
        rb.velocity = Vector2.up * rb.velocity.y + updateVelocity;
    }

    private void OnEnable()
    {
        InputManager.Instance.OnStartTouch += Jump;
    }

    private void OnDisable()
    {
        InputManager.Instance.OnStartTouch -= Jump;
    }

    private void Jump(Vector2 position, float time)
    {
        rb.velocity = Vector2.right * rb.velocity.x + Vector2.up * jumpForce;
        AudioManager.Instance.PlayClip(jumpSound);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        lastVelocity = rb.velocity;
        OnPlayerPassWall -= Score.Instance.Collect;        
        enabled = false;
        rb.velocity = lastVelocity;

        OnPlayerHitWall?.Invoke();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        OnPlayerPassWall?.Invoke();
    }

    public void Pause()
    {
        lastVelocity = rb.velocity;
        rb.simulated = false;
    }

    public void Resume()
    {
        rb.simulated = true;
        rb.velocity = lastVelocity;
    }
}
