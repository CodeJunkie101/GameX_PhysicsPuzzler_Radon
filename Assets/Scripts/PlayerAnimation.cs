using UnityEngine;
public class PlayerAnimation : MonoBehaviour
{
    Animator anim;
    RealPlayerController movement;
    Rigidbody2D rb;

    void Start()
    {
        anim = GetComponent<Animator>();
        movement = GetComponent<RealPlayerController>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float speed = Mathf.Abs(movement.move);
        anim.SetFloat("speed", speed);

        bool isJumping = Mathf.Abs(rb.linearVelocity.y) > 0.1f;
        anim.SetBool("IsJumping", isJumping);
    }
}