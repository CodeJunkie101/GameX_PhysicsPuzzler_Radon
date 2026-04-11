using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    public static Vector3 spawn;

public Vector3 spawnPoint;
    public GameObject Player;
    void Start()
    {
        spawn = spawnPoint;
        Instantiate(Player , spawn , Quaternion.identity);
    }
    public void startGame()
    {
        SceneManager.LoadScene(1);
    }
}
