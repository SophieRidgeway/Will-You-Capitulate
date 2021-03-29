using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int playerMaxHealth;

    private HealthBar healthBar;
    private int playerHealth;
    private bool death = false;
    private RigBuilder rigBuilder;
    private characterMovement characterMovement;
    private CharacterAiming characterAiming;
    private Animator animator;
    private int isDeadPar = Animator.StringToHash("IsDead");

    // Start is called before the first frame update
    void Awake()
    {
        healthBar = FindObjectOfType<HealthBar>();
        playerHealth = playerMaxHealth;
        healthBar.SetMaxHealth(playerHealth);
        StartCoroutine(RegenHealthTimer());
        Getters();
    }

    private void Getters()
    {
        rigBuilder = GetComponent<RigBuilder>();
        characterMovement = GetComponent<characterMovement>();
        characterAiming = GetComponent<CharacterAiming>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerHealth <= 0)
        {
            print("dead");
            death = true;
            animator.SetBool(isDeadPar, true);
            Disable();

        }
    }

    private void Disable()
    {
        rigBuilder.enabled = false;
        characterMovement.enabled = false;
        characterAiming.enabled = false;
    }

    IEnumerator RegenHealthTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(8f);
            RegenHealth();
        }
    }

    private void RegenHealth()
    {
        if (playerHealth != playerMaxHealth)
        {
            playerHealth = playerHealth + 1;
            healthBar.SetSliderValue(playerHealth);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            playerHealth = playerHealth - 1;
            healthBar.SetSliderValue(playerHealth);
        }
    }

    public bool DeathCheck()
    {
        return death;
    }

    public void TakeDamage(int damage)
    {
        playerHealth = playerHealth - damage;
        healthBar.SetSliderValue(playerHealth);
    }
}
