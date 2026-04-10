using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 8f;
    public float runSpeed = 8f;
    public float jumpForce = 10f;

    public Animator animator;

    private Vector2 initial, final;
    public static Vector2 moveLight;

    public GameObject lightball;
    public GameObject HeldMirror;
    public float LightBallSpeed = 10f;

    bool hasMirror;
    bool isGrounded;
    bool canMove;
    bool isLight;
    bool boolFlipX;

    private Rigidbody2D r;
    private SpriteRenderer sr;

    public Sprite Jumping;

    // 🔥 TRAJECTORY
    public GameObject dotPrefab;
    public int dotsCount = 50;
    public float timeStep = 0.03f;

    private List<GameObject> dots = new List<GameObject>();

    void Start()
    {
        canMove = true;
        isLight = false;

        r = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        // CREATE DOTS
        for (int i = 0; i < dotsCount; i++)
        {
            GameObject dot = Instantiate(dotPrefab, transform.position, Quaternion.identity);
            dot.SetActive(false);
            dots.Add(dot);
        }
    }

    void Update()
    {
        ability();
        movement();
        jump();
    }

    void ability()
    {
        // START DRAG
        if (Input.GetMouseButtonDown(0) && !isLight)
        {
            initial = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            canMove = false;
        }

        // DRAG → SHOW TRAJECTORY
        if (Input.GetMouseButton(0) && !isLight)
        {
            Vector2 current = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = initial - current;

            // clamp for better control
            direction = Vector2.ClampMagnitude(direction, 5f);

            ShowTrajectory(transform.position, direction * LightBallSpeed);
        }

        // RELEASE → SHOOT
        if (Input.GetMouseButtonUp(0))
        {
            final = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            isLight = true;

            Vector2 direction = initial - final;
            direction = Vector2.ClampMagnitude(direction, 5f);

            Vector2 launchVelocity = direction * LightBallSpeed;

            HideDots();

            sr.enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;

            GameObject movinglight = Instantiate(lightball, transform.position, Quaternion.identity);

            Rigidbody2D rbLight = movinglight.GetComponent<Rigidbody2D>();
            rbLight.linearVelocity = launchVelocity;

            Destroy(gameObject);
        }
    }

    void ShowTrajectory(Vector2 startPos, Vector2 velocity)
    {
        Vector2 pos = startPos;
        Vector2 vel = velocity;

        for (int i = 0; i < dots.Count; i++)
        {
            // step-by-step physics (MATCHES UNITY)
            vel += Physics2D.gravity * timeStep;
            pos += vel * timeStep;

            dots[i].transform.position = new Vector3(pos.x, pos.y, 0f);

            // Angry Birds style (optional but nice)
            float scale = Mathf.Lerp(0.3f, 0.05f, i / (float)dots.Count);
            dots[i].transform.localScale = new Vector3(scale, scale, 1f);

            SpriteRenderer dsr = dots[i].GetComponent<SpriteRenderer>();
            Color c = dsr.color;
            c.a = Mathf.Lerp(1f, 0.1f, i / (float)dots.Count);
            dsr.color = c;

            dots[i].SetActive(true);
        }
    }

    void HideDots()
    {
        foreach (var dot in dots)
        {
            dot.SetActive(false);
        }
    }

    void movement()
    {
        if (canMove)
        {
            float move = Input.GetAxisRaw("Horizontal");

            if (move == 1f) boolFlipX = false;
            if (move == -1f) boolFlipX = true;

            sr.flipX = boolFlipX;

            animator.SetFloat("Speed", Mathf.Abs(move));

            if (Input.GetKey(KeyCode.LeftShift) && isGrounded)
                r.linearVelocity = new Vector2(move * runSpeed, r.linearVelocity.y);
            else
                r.linearVelocity = new Vector2(move * moveSpeed, r.linearVelocity.y);
        }
    }

    public void jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && canMove)
        {
            r.linearVelocity = new Vector2(r.linearVelocity.x, jumpForce);
            sr.sprite = Jumping;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            isGrounded = true;

        if (collision.gameObject.CompareTag("Spike"))
            Debug.Log("Player hit spikes , dead");
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
            animator.SetFloat("Jump", 0f);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("MirrorObject"))
        {
            Destroy(collision.gameObject);

            GameObject temp = Instantiate(HeldMirror, transform.position, Quaternion.identity);
            temp.transform.parent = transform;
            temp.transform.position = transform.position + new Vector3(0f, 1.25f, 0f);

            hasMirror = true;
        }
    }
}