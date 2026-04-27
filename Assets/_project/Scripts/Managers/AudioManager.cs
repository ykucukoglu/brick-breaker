using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource impactAS;
    public AudioSource explodeAS;
    public AudioSource positiveAS;
    public AudioSource failAS;
    public AudioSource coinCollectAS;

    public void PlayImpactAS()
    {
        impactAS.Play();
    }

    public void PlayExplodeAS()
    {
        explodeAS.Play();
    }

    public void PlayPositiveAS()
    {
        positiveAS.Play();
    }

    public void PlayFailAS()
    {
        failAS.Play();
    }

    public void PlayCoinCollectAS()
    {
        coinCollectAS.Play();
    }
}
