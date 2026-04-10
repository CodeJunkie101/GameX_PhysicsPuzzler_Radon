using UnityEngine;

public class WinSocket : MonoBehaviour
{
    public GameObject LevelController;
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "LightBall")
        {
            
            Debug.Log("Hit the Socket");
            LevelController.SetActive(true);
        }
    }
}
