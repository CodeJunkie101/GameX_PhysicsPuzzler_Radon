using UnityEngine;

public class RealPlayerController : MonoBehaviour
{
    public float moveSpeed = 8f;
    public bool boolFlipX;
    public float runSpeed = 8f;
    public float jumpForce = 10f;
    public float move;
    private Vector2 initial, final;
    public static Vector2 moveLight;
    public GameObject lightball;
    public GameObject HeldMirror;
    public float LightBallSpeed = 100f;
    bool isGrounded;
    bool canMove;
    static bool isLight;
    private Rigidbody2D r;
    public GameObject line;
    private GameObject arrowline;
    static Quaternion directionoflight;


    void Start()
    {
        canMove = true;
        isLight = false;
        r = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        LeftRightMovement();
        ability();
        jump();
        if (r.linearVelocityY == 0)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }

    void ability()
    {
        
        if (Input.GetKeyDown(KeyCode.Mouse0) && !isLight)
{
    initial = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    canMove = false;
}

if (Input.GetKey(KeyCode.Mouse0) && !isLight)
{
    Vector2 currentMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    Vector2 direction = (currentMousePos - initial).normalized;
    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
    Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    if (arrowline != null)
        Destroy(arrowline);
    Vector2 spawnPos = (Vector2)transform.position + direction * 0.5f;

    arrowline = Instantiate(line, spawnPos, rotation);

        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {

            final = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            isLight = true;
            moveLight = initial - final;
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            GameObject movinglight = Instantiate(lightball, transform.position, directionoflight);
            movinglight.GetComponent<Rigidbody2D>().linearVelocity = moveLight.normalized * LightBallSpeed;
            Destroy(arrowline);
            Destroy(gameObject);
        }
    }
    void LeftRightMovement()
    {
        if (canMove)
        {
             move = Input.GetAxisRaw("Horizontal");
             if (move == 1f) boolFlipX = false;
            if (move == -1f) boolFlipX = true;
            gameObject.GetComponent<SpriteRenderer>().flipX = boolFlipX;
            if(Input.GetKey(KeyCode.LeftShift) && isGrounded) r.linearVelocity = new Vector2(move * runSpeed, r.linearVelocityY);
            else r.linearVelocity = new Vector2(move * moveSpeed, r.linearVelocity.y);
        }
    }
    public void jump()
    {
        if(Input.GetKey(KeyCode.Space) && isGrounded && canMove) 
        {
            r.linearVelocity = new Vector2(r.linearVelocity.x, jumpForce);
            Debug.Log("jumped now");
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground")) isGrounded = true;
        if(collision.gameObject.CompareTag("Spike"))
        {
            
            Instantiate(gameObject , LevelController.spawn , Quaternion.identity);
            Destroy(gameObject);
        }
        
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            isGrounded = false;
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("MirrorObject"))
        {
            Destroy(collision.gameObject);
            GameObject temp = Instantiate(HeldMirror, gameObject.transform.position, Quaternion.identity);
            temp.transform.parent = gameObject.transform;
            temp.transform.position = gameObject.transform.position + new Vector3(0f, 1.25f, 0f);
            Debug.Log("i have mirror");

        }
        if(collision.gameObject.CompareTag("MirrorHolder"))
        {
        }
}
}
