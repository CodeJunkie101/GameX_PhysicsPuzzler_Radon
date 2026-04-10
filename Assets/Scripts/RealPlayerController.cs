using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;

public class RealPlayerController : MonoBehaviour
{
    public float moveSpeed = 8f;
    public float runSpeed = 8f;
    public float jumpForce = 10f;
    
    private Vector2 initial, final;
    public static Vector2 moveLight;
    Vector2 cursorpos;
    public GameObject lightball;
    public GameObject HeldMirror;
    public float LightBallSpeed = 10f;
    bool hasMirror;
    bool isGrounded;
    bool canMove;
    bool isLight;
    private Rigidbody2D r;


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
    }

    void ability()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && !isLight)
        {
            initial= Camera.main.ScreenToWorldPoint(Input.mousePosition);
            canMove = false;
        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            final = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            isLight = true;
            moveLight = initial - final;
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            GameObject movinglight = Instantiate(lightball, transform.position, quaternion.identity);
            Debug.Log(moveLight.normalized + " || " + moveLight);
            movinglight.GetComponent<Rigidbody2D>().linearVelocity = moveLight.normalized * LightBallSpeed;
            Destroy(gameObject);
        }
    }
    void LeftRightMovement()
    {
        if (canMove)
        {
            float move = Input.GetAxisRaw("Horizontal");
            if(Input.GetKey(KeyCode.LeftShift) && isGrounded) r.linearVelocity = new Vector2(move * runSpeed, r.linearVelocityY);
            else r.linearVelocity = new Vector2(move * moveSpeed, r.linearVelocity.y);
        }
    }
    public void jump()
    {
        if(Input.GetKey(KeyCode.Space) && isGrounded && canMove) 
        {
            r.linearVelocity = new Vector2(r.linearVelocity.x, jumpForce);
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground")) isGrounded = true;
        if(collision.gameObject.CompareTag("Spike"))
        {
            Debug.Log("Player hit spikes , dead");
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
            hasMirror = true;
            Debug.Log("i have mirror");

        }
        if(collision.gameObject.CompareTag("MirrorHolder"))
        {
            
    }
}
}
