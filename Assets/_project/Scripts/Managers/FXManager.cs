using UnityEngine;

public class FXManager : MonoBehaviour
{
    public ParticleSystem brickDestroyPS;
    public ParticleSystem ballImpactPS;
    public void PlayBrickDestroyedParticles(Vector3 position)
    {
        var newPS = Instantiate(brickDestroyPS);
        newPS.transform.position = position;
        newPS.Play();
    }

    public void PlayBallImpactPS(Vector3 pos, Vector3 dir)
    {
        var newPS = Instantiate(ballImpactPS);
        newPS.transform.position = pos;
        newPS.transform.LookAt(pos + dir);
        newPS.Play();
    }
}
