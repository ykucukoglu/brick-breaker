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

    private void FixedUpdate()
    {
        transform.position += _direction.normalized * speed * Time.fixedDeltaTime;

    }
}
