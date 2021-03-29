using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTest : MonoBehaviour
{
    [SerializeField] int maxHealth = 5;

    private Material material;
    private bool isDead;

    private float currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        material = GetComponent<Renderer>().material;
        isDead = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentHealth <= 0)
        {
            material.color = Color.red;
            isDead = true;
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth = currentHealth - damage;
    }

    public bool DeathCheck()
    {
        return isDead;
    }
}
