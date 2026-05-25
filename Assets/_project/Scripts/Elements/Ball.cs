using System;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float speed;
    private Vector3 _direction;
    private LevelManager _levelManager;
    private FXManager _fxManager;
    private AudioManager _audioManager;
    private Rigidbody2D _rb;

    public float playerCollisionXOffetMulplier;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
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
        _rb.linearVelocity = _direction.normalized * speed;
        //transform.position += _direction.normalized * speed * Time.fixedDeltaTime;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall")) Bounce(collision.contacts[0].normal, collision.contacts[0].point, Color.white);
        if (collision.gameObject.CompareTag("Brick"))
        {
            Bounce(collision.contacts[0].normal, collision.contacts[0].point, Color.yellow);
            collision.gameObject.GetComponent<Brick>().GetHit();
            _audioManager.PlayPositiveAS();
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            var offset = (transform.position - collision.transform.position).x * playerCollisionXOffetMulplier;
            Bounce(collision.contacts[0].normal, collision.contacts[0].point, new Color(0, .5f, 1), offset);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if ((collision.contacts[0].point.x < transform.position.x && _direction.x < 0)
                || (collision.contacts[0].point.x > transform.position.x && _direction.x > 0))
            {
                _direction.x *= -1;
            }
        }

        if ((collision.contacts[0].point.y < transform.position.y && _direction.y < 0)
             || (collision.contacts[0].point.y > transform.position.y && _direction.y > 0))
        {
            _direction.y *= -1;
        }
    }

    void Bounce(Vector3 n, Vector3 contactPos, Color color, float offset = 0f)
    {
        var newDir = Vector3.Reflect(_direction, n);
        if (offset != 0) newDir.x = offset;
        _direction = newDir;

        if (_direction.y < .2f && _direction.y > 0) _direction.y = .2f;
        else if (_direction.y > -.2f && _direction.y < 0) _direction.y = -.2f;

        _fxManager.PlayBallImpactPS(contactPos, n, color);
        _audioManager.PlayImpactAS();
    }
}
