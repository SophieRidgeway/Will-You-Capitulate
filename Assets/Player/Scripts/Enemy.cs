using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int health = 20;

    public void TakeDamage(int damage)
    {
        health = health - damage;

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
