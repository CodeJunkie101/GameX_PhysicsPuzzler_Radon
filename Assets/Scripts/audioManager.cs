using UnityEngine;


public class audioManager : MonoBehaviour
{
    public AudioSource musicSource;
    public AudioSource SFXSource;

    public AudioClip hurt, btp, ptp, btnprs,backgrnd;
    
    private void Start()
    {
        Debug.Log("Start Music");
        musicSource.clip = backgrnd;
        musicSource.Play();
    }
    public void PlaySFX(AudioClip clip)
    {
        Debug.Log(clip);
        SFXSource.PlayOneShot(clip);
    }
}
