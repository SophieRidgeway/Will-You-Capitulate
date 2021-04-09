using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    [SerializeField] float shotDelay = 1f;
    [SerializeField] int damage = 1;
    [SerializeField] float hitRange = 100f;
    [SerializeField] LineRenderer line;

    private bool canShoot;
    private bool moving;
    private Animator animator;
    private int isShootingParam = Animator.StringToHash("IsShooting");
    private RaycastHit hit;
    private PlayerHealth playerHealth;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TimeBetweenShoot());
        animator = GetComponentInParent<Animator>();
        playerHealth = FindObjectOfType<PlayerHealth>();
    }

    IEnumerator TimeBetweenShoot()
    {
        while (true)
        {
            yield return new WaitForSeconds(shotDelay);
            AutoShoot();
        }
    }

    // Update is called once per frame
    void Update()
    {
        var ray = new Ray(gameObject.transform.position, gameObject.transform.forward);

        if (canShoot == true && moving == false && playerHealth.DeathCheck() == false)
        {
            if (Physics.Raycast(ray, out hit, hitRange))
            {
                line.SetPosition(0, gameObject.transform.position);
                animator.SetBool(isShootingParam, true);
                //Debug.DrawLine(gameObject.transform.position, hit.point, Color.red, 5f);
                Damage();
                LineManagement();
                StartCoroutine(LineLife());
            }
            canShoot = false;
        }
        else
        {
            animator.SetBool(isShootingParam, false);
        }
    }

    private void Damage()
    {
        PlayerHealth player = hit.transform.GetComponent<PlayerHealth>();
        if (player != null)
        {
            playerHealth.TakeDamage(damage);
        }
    }

    void AutoShoot()
    {
        canShoot = true;
    }

    public void MovingToDest(bool m)
    {
        moving = m;
    }

    private void LineManagement()
    {
        line.enabled = true;
        line.SetPosition(1, hit.point);
    }

    IEnumerator LineLife()
    {
        yield return new WaitForSeconds(0.05f);
        line.enabled = false;
    }
}
