using System;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public int startHealth;
    private int _currentHealth;

    private void Start()
    {
        _currentHealth = startHealth;
    }

    public void GetHit()
    {
        _currentHealth--;
        if (_currentHealth <= 0) DestroyBrick();
    }

    private void DestroyBrick()
    {
        gameObject.SetActive(false);
    }
}
