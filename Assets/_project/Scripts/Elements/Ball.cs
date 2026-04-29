using System;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float speed;
    private Vector3 _direction;
    private LevelManager _levelManager;
    private FXManager _fxManager;
    private AudioManager _audioManager;
    public void StartBall(LevelManager levelManager, Vector3 dir)
    {
        _levelManager = levelManager;
        _fxManager = _levelManager.gameDirector.fxManager;
        _audioManager = _levelManager.gameDirector.audioManager;
        _direction = dir;
        if (_levelManager.currentLevelNo > 35) speed++;
    }

    public void SetBallDireciton(Vector3 dir)
    {
        _direction = dir;
    }

    private void FixedUpdate()
    {
        transform.position += _direction.normalized * speed * Time.fixedDeltaTime;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall")) Bounce(collision.contacts[0].normal, collision.contacts[0].point);
        if (collision.gameObject.CompareTag("Brick"))
        {
            Bounce(collision.contacts[0].normal, collision.contacts[0].point);
            collision.gameObject.GetComponent<Brick>().GetHit();
            _audioManager.PlayPositiveAS();
        }
        if (collision.gameObject.CompareTag("Player")) Bounce(collision.contacts[0].normal, collision.contacts[0].point);
    }

    void Bounce(Vector3 n, Vector3 contactPos)
    {
        _direction = Vector3.Reflect(_direction, n);
        _fxManager.PlayBallImpactPS(contactPos, n);
        _audioManager.PlayImpactAS();
    }
}
