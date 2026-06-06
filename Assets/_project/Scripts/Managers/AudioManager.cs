using DG.Tweening;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource impactAS;
    public AudioSource explodeAS;
    public AudioSource positiveAS;
    public AudioSource failAS;
    public AudioSource coinCollectAS;
    public List<AudioSource> sounds;
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

    public void StartMusic(int levelNo)
    {
        foreach (var sound in sounds)
        {
            sound.Stop();
            sound.volume = .5f;
        }
        sounds[(levelNo - 1) % sounds.Count].Play();
    }

    public void StopMusic()
    {
        foreach (var sound in sounds)
        {
            sound.DOFade(0, 0.5f).OnComplete(() => sound.Stop());
        }
    }
}
