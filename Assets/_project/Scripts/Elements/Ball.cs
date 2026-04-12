using System;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float speed;
    private Vector3 _direction;

    public void StartBall(Vector3 dir)
    {
        _direction = dir;
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
        if (collision.gameObject.CompareTag("Wall")) Bounce(collision.contacts[0].normal);
        if (collision.gameObject.CompareTag("Brick"))
        {
            Bounce(collision.contacts[0].normal);
            collision.gameObject.GetComponent<Brick>().GetHit();
        }
        if (collision.gameObject.CompareTag("Player")) Bounce(collision.contacts[0].normal);
    }

    void Bounce(Vector3 n)
    {
        _direction = Vector3.Reflect(_direction, n);
    }
}
