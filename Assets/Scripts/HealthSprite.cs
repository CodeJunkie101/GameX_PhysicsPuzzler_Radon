using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthSprite : MonoBehaviour
{
    private SpriteRenderer sr;
    public int selfhealth;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerHealth.CurrentPlayerHealth < selfhealth)
        {
            sr.color = Color.red;
        }
    }
}
