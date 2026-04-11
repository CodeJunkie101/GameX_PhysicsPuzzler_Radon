using UnityEngine;

public class Tutorialtagsremoval : MonoBehaviour
{
    private bool WASD,Mouse;
    public GameObject WASDToolTip, tip;
    void Update()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            WASD = true;
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Mouse = true;
        }
        if (WASD && Mouse)
        {
            WASDToolTip.SetActive(false);
            tip.SetActive(false);
        }
    }
}
