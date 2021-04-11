using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class characterMovement : MonoBehaviour
{
    [SerializeField] float jumpHeight;
    [SerializeField] float gravity;
    [SerializeField] float steapDown;
    [SerializeField] float airControle;
    [SerializeField] float jumpDamp;
    [SerializeField] float groundSpeed;
    [SerializeField] float pushPower;
    [SerializeField] RigBuilder RigBuilder;
    [SerializeField] GameObject gun;
    [SerializeField] GameObject crossHair;

    private Animator animator;
    private Vector2 input;
    private Vector3 rootMontion;
    private Vector3 velocity;
    private CharacterController charCon;
    private bool isJumping;
    private int isSprintingParam = Animator.StringToHash("isSprinting");
    private CharacterAiming characterAiming;
    private AchievementManager achievement;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        charCon = GetComponent<CharacterController>();
        characterAiming = GetComponentInChildren<CharacterAiming>();
        achievement = FindObjectOfType<AchievementManager>();
    }

    // Update is called once per frame
    void Update()
    {
       if(achievement.GameInSession() == true && achievement.GamePaued() == false)
        {
            input.x = Input.GetAxis("Horizontal");
            input.y = Input.GetAxis("Vertical");

            animator.SetFloat("InputX", input.x);
            animator.SetFloat("InputY", input.y);

            UpdateIsSprinting();

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }
        }
    }

    private void UpdateIsSprinting()
    {
        bool isSprinting = Input.GetKey(KeyCode.LeftShift);
        if (isSprinting == true)
        {
            print("here");
            animator.SetBool(isSprintingParam, isSprinting);
            characterAiming.NoSprint(false);
            RigBuilder.enabled = false;
            gun.SetActive(false);
            characterAiming.CanFire(false);
            crossHair.SetActive(false);
        }
        else
        {
            print("there");
            animator.SetBool(isSprintingParam, false);
            characterAiming.NoSprint(true);
            RigBuilder.enabled = true;
            gun.SetActive(true);
            characterAiming.CanFire(true);
            crossHair.SetActive(true);
        }

    }

    private void OnAnimatorMove()
    {
        rootMontion += animator.deltaPosition;
    }

    private void FixedUpdate()
    {
       if(achievement.GameInSession() == true)
        {
            if (isJumping)//In air
            {
                InAir();
            }
            else
            {
                OnGround();
            }
        }
    }

    private void InAir()
    {
        velocity.y -= gravity * Time.fixedDeltaTime;
        Vector3 displacement = velocity * Time.fixedDeltaTime;
        displacement += CalculateAirControl();
        charCon.Move(displacement);
        isJumping = !charCon.isGrounded;
        rootMontion = Vector3.zero;
    }

    private void OnGround()
    {
        Vector3 steapFowardAmount = rootMontion * groundSpeed;
        Vector3 steapDownAmount = Vector3.down * steapDown;
        charCon.Move(steapFowardAmount + steapDownAmount);
        rootMontion = Vector3.zero;
        animator.SetBool("isJumping", isJumping);

        if (!charCon.isGrounded)
        {
            SetInAir(0);
        }
    }

    private Vector3 CalculateAirControl()
    {
        return ((transform.forward * input.y) + (transform.right * input.x)) * (airControle / 100);
    }

    private void Jump()
    {
        if(!isJumping)
        {
            float jumpVelocity = Mathf.Sqrt(2 * gravity * jumpHeight);
            SetInAir(jumpVelocity);

        }
    }

    private void SetInAir(float jumpVelocity)
    {
        isJumping = true;
        velocity = animator.velocity * jumpDamp * groundSpeed;
        velocity.y = jumpVelocity;
        animator.SetBool("isJumping", true);
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        //From unity manual

        Rigidbody body = hit.collider.attachedRigidbody;

        // no rigidbody
        if (body == null || body.isKinematic)
            return;

        // We dont want to push objects below us
        if (hit.moveDirection.y < -0.3f)
            return;

        // Calculate push direction from move direction,
        // we only push objects to the sides never up and down
        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);

        // If you know how fast your character is trying to move,
        // then you can also multiply the push velocity by that.

        // Apply the push
        body.velocity = pushDir * pushPower;
    }
}
