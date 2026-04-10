using Unity.Mathematics;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] public float moveSpeed = 8f;
    [SerializeField] public float runSpeed = 8f;
    [SerializeField]public float jumpForce = 6f;
    public Animator animator;
    Rigidbody2D r;
    bool isGrounded;
    bool canMove;
    bool isLight;
    float move;
    private Vector2 initial, final;
    public static Vector2 moveLight;
    Vector2 cursorpos;
    public GameObject lightball;
    public GameObject HeldMirror;
    private float LightBallSpeed = 10f;
    bool hasMirror;


    void Start()
    {
        canMove = true;
        isLight = false;
        r = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        ability();
        movement();
        jump();
        run();
        landing();

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
    void movement()
    {
        if (canMove){
        move = Input.GetAxis("Horizontal");
        r.linearVelocity = new Vector2(move * moveSpeed, r.linearVelocity.y);}
        animator.SetFloat("Speed" , Mathf.Abs(move));
    }
    void jump()
    {
        if(Input.GetKey(KeyCode.Space) && isGrounded && canMove)
        {
            r.linearVelocity = new Vector2(r.linearVelocity.x, jumpForce);
            animator.SetBool("isJumping" , true);
        }
    }
    public void landing()
    {
        animator.SetBool("isJumping" , false);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
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
     void run()
    {
        if(Input.GetKey(KeyCode.LeftShift) && isGrounded)
        {
            r.linearVelocity = new Vector2(move * runSpeed, r.linearVelocityY);
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
