using UnityEngine;

public class LightBallController : MonoBehaviour
{
    public GameObject player;
    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Hit Something");
        if (collision.gameObject.tag == "BlackWall")
        {
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag != "Mirror")
        {
            Instantiate(player, gameObject.transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
