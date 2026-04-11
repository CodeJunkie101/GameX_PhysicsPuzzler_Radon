using UnityEngine;
using UnityEngine.SceneManagement;

public class LightBallController : MonoBehaviour
{
    public GameObject player;
    private Rigidbody2D rb;
    public static bool exist;
    audioManager audioManager;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<audioManager>();
    }
    void Start()
    {
        exist = true;
        rb = gameObject.GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        //Instantiate(trail, transform.position, Quaternion.identity);
        if (rb.linearVelocity == Vector2.zero)
        {
            Instantiate(player, gameObject.transform.position, Quaternion.identity);
            exist = false;
            Destroy(gameObject);
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "BlackWall")
        {
            Instantiate(player , LevelController.spawn , Quaternion.identity);
            exist = false;
            PlayerHealth.CurrentPlayerHealth--;
            audioManager.PlaySFX(audioManager.hurt);
            Destroy(gameObject);
            
        }
        
        else if (collision.gameObject.tag != "Mirror")
        {
            Instantiate(player, gameObject.transform.position, Quaternion.identity);
            audioManager.PlaySFX(audioManager.btp);
            exist = false;
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.tag == "Socket")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
