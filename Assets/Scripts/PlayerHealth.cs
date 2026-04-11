using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public GameObject YouFail;
    public static int CurrentPlayerHealth = 3;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    void Update()
    {
        if (CurrentPlayerHealth == 0)
        {
            YouFail.SetActive(true);
        }
    }
    
    public void Retry()
    {
        SceneManager.LoadScene(1);
        CurrentPlayerHealth = 3;
        YouFail.SetActive(false);
    }
    public void Quit()
    {
        Application.Quit();
    }
}
