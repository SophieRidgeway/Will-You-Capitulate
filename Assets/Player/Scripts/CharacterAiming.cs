using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAiming : MonoBehaviour
{
    [SerializeField] int maxShots;
    [SerializeField] float turnSpeed = 15f;
    [SerializeField] Cinemachine.AxisState xAxis;
    [SerializeField] Cinemachine.AxisState yAxis;
    [SerializeField] Transform camraLookAt;

    private Camera mainCamera;
    private PlayerWeaponFire weaponFire;
    private bool fireOk;
    private Animator animator;
    private int isAimingParam = Animator.StringToHash("isAiming");
    private bool isAimming;
    private bool notSprint;
    private PowerBar powerBar;
    private int currentShots;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        weaponFire = GetComponentInChildren<PlayerWeaponFire>();
        animator = GetComponent<Animator>();
        powerBar = FindObjectOfType<PowerBar>();
        currentShots = maxShots;
        powerBar.SetMaxPower(maxShots);
        StartCoroutine(RegenPowerTimer());
    }
    IEnumerator RegenPowerTimer()
    {
        while(true)
        {
            yield return new WaitForSeconds(3f);
            RegenPower();
        }
    }

    private void RegenPower()
    {
        if (currentShots != maxShots)
        {
            currentShots = currentShots + 1;
            powerBar.SetSliderValue(currentShots);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        xAxis.Update(Time.fixedDeltaTime);
        yAxis.Update(Time.fixedDeltaTime);
        camraLookAt.eulerAngles = new Vector3(yAxis.Value, xAxis.Value, 0);
        float yawCamra = mainCamera.transform.rotation.eulerAngles.y;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, yawCamra, 0), turnSpeed * Time.fixedDeltaTime);
    }

    public void CanFire(bool fire)
    {
        fireOk = fire; 
    }

    public void NoSprint(bool sprint)
    {
        notSprint = sprint;
    }

    private void Update()
    {
        isAimming = Input.GetMouseButton(1);


        if (isAimming && notSprint == true)
        {
            animator.SetBool(isAimingParam, isAimming);
        }
        else
        {
            animator.SetBool(isAimingParam, false);
        }

        if (Input.GetButtonDown("Fire1") && fireOk == true && currentShots > 0)
        {
            weaponFire.Fire();
            currentShots = currentShots - 1;
            powerBar.SetSliderValue(currentShots);
        }

    }
}
