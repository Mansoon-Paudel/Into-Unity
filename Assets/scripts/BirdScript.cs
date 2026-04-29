using UnityEngine;
 
public class BirdScript : MonoBehaviour
{
    public Rigidbody2D myRigidbody;
    public float flapStrength;
    public LogicScript logic;
    public bool birdIsAlive = true;
    public float outOfBoundsPadding = 0.1f;

    private Collider2D _birdCollider;
    private SpriteRenderer _birdSpriteRenderer;
    
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        _birdCollider = GetComponent<Collider2D>();
        _birdSpriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && birdIsAlive)
        {
            myRigidbody.linearVelocity = Vector2.up * flapStrength;
        }

        if (birdIsAlive)
        {
            CheckOutOfBounds();
        }
    }

    void CheckOutOfBounds()
    {
        Camera cam = Camera.main;
        if (cam == null)
        {
            return;
        }

        Bounds bounds;
        if (_birdCollider != null)
        {
            bounds = _birdCollider.bounds;
        }
        else if (_birdSpriteRenderer != null)
        {
            bounds = _birdSpriteRenderer.bounds;
        }
        else
        {
            bounds = new Bounds(transform.position, Vector3.zero);
        }

        Vector3 viewportCenter = cam.WorldToViewportPoint(bounds.center);
        Vector3 viewportMin = cam.WorldToViewportPoint(bounds.min);
        Vector3 viewportMax = cam.WorldToViewportPoint(bounds.max);

        bool touchedTop = viewportMax.y >= 1f;
        bool fullyOutLeft = viewportMax.x < -outOfBoundsPadding;
        bool fullyOutRight = viewportMin.x > 1f + outOfBoundsPadding;
        bool fullyOutBottom = viewportMax.y < -outOfBoundsPadding;
        bool behindCamera = viewportCenter.z < 0f;

        if (touchedTop || fullyOutLeft || fullyOutRight || fullyOutBottom || behindCamera)
        {
            Die();
        }
    }
 
    private void OnCollisionEnter2D(Collision2D _)
    {
        Die();
    }

    void Die()
    {
        if (!birdIsAlive)
        {
            return;
        }

        birdIsAlive = false;
        myRigidbody.linearVelocity = Vector2.zero;

        if (logic != null)
        {
            logic.gameOver();
        }
    }
}

