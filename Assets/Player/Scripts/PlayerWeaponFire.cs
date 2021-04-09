using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponFire : MonoBehaviour
{
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] ParticleSystem hitEffect;
    [SerializeField] LineRenderer line;
    [SerializeField] Transform bulletHole;
    [SerializeField] Camera camera;
    [SerializeField] int damage;

    private RaycastHit hit;
    public void Fire()
    {
        muzzleFlash.Emit(1);

        line.SetPosition(0, bulletHole.transform.position);
        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, 100f))
        {
            //Debug.DrawLine(camera.transform.position, hit.point, Color.red, 5.0f);
            HitEffect();
            LineManagement();
            StartCoroutine(LineLife());
            ShootDamage();
            Triffic();
            Pizza();

        }
        else
        {
            line.enabled = false;
        }
    }

    private void HitEffect()
    {
        hitEffect.transform.position = hit.point;
        hitEffect.transform.forward = hit.normal;
        hitEffect.Emit(12);
    }

    private void LineManagement()
    {
        line.enabled = true;
        line.SetPosition(1, hit.point);
    }

    private void ShootDamage()
    {
        EnemyColissionCheck enemy = hit.transform.GetComponent<EnemyColissionCheck>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }
    }

    private void Triffic()
    {
        Cone cone = hit.transform.GetComponent<Cone>();
        if (cone != null)
        {
            cone.ConeHit(1);
        }
    }

    private void Pizza()
    {
        PizzaTime pizza = hit.transform.GetComponent<PizzaTime>();
        if(pizza != null)
        {
            pizza.PizzaHit(1);
        }
    }

    IEnumerator LineLife()
    {
        yield return new WaitForSeconds(0.01f);
        line.enabled = false;
    }

}
