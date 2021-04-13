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
    [SerializeField] AudioSource fireShot;

    private Camera mainCamera;
    private PlayerWeaponFire weaponFire;
    private bool fireOk;
    private Animator animator;
    private int isAimingParam = Animator.StringToHash("isAiming");
    private bool isAimming;
    private bool notSprint;
    private PowerBar powerBar;
    private int currentShots;
    private int shotCount;
    private AchievementManager achievement;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        weaponFire = GetComponentInChildren<PlayerWeaponFire>();
        animator = GetComponent<Animator>();
        powerBar = FindObjectOfType<PowerBar>();
        currentShots = maxShots;
        powerBar.SetMaxPower(maxShots);
        achievement = FindObjectOfType<AchievementManager>();
        StartCoroutine(RegenPowerTimer());
    }

    public void Cursure(bool hide)
    {
        if(hide == true)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    IEnumerator RegenPowerTimer()
    {
        while(true)
        {
            yield return new WaitForSeconds(2f);
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
       if(achievement.GameInSession() == true && achievement.GamePaued() == false)
        {
            isAimming = Input.GetMouseButton(1);

            CountingBullets();

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
                fireShot.Play();
                weaponFire.Fire();
                currentShots = currentShots - 1;
                shotCount = shotCount + 1;
                powerBar.SetSliderValue(currentShots);
            }
        }
    }

    public int CountingBullets()
    {
        return shotCount;
    }
}
