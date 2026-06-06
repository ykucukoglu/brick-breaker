using UnityEngine;

public class FXManager : MonoBehaviour
{
    public ParticleSystem brickDestroyPS;
    public ParticleSystem ballImpactPS;
    public ParticleSystem coinCollectPS;
    public CameraShake cameraShake;
    public float cameraShakeDuration;
    public float cameraShakeMagnitude;
    public void PlayBrickDestroyed(Vector3 position)
    {
        var newPS = Instantiate(brickDestroyPS);
        newPS.transform.position = position;
        newPS.Play();
        cameraShake.Shake(cameraShakeMagnitude, cameraShakeDuration);
    }

    public void PlayBallImpactPS(Vector3 pos, Vector3 dir, Color color)
    {
        var newPS = Instantiate(ballImpactPS);
        newPS.transform.position = pos;
        newPS.transform.LookAt(pos + dir);
        var main = newPS.main;
        main.startColor = color;
        newPS.Play();
    }

    public void PlayCoinCollectPS(Vector3 pos)
    {
        var newPS = Instantiate(coinCollectPS);
        newPS.transform.position = pos;
        newPS.Play();
    }
}
