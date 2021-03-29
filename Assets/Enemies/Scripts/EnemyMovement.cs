using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.Animations.Rigging;

public class EnemyMovement : MonoBehaviour
{
    //code taken from crow move from PCG assignment

    [SerializeField] float speed = 2f;
    [SerializeField] float minRandomRange = 4f;
    [SerializeField] float maxRandomRange = 15f;
    [SerializeField] Transform location1;
    [SerializeField] Transform location2;
    [SerializeField] Transform location3;
    [SerializeField] Transform location4;
    [SerializeField] Transform location5;
    [SerializeField] Transform location6;

    private float radius = 1f;
    private bool Move = true;
    private float waitTime;
    private Animator animator;
    private int isMoveingParam = Animator.StringToHash("IsMoving");
    private EnemyShooting enemyShooting;
    private PlayerHealth playerTest;
    private LookAtPlayer lookAtPlayer;

    private List<Transform> paths = new List<Transform>();

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        enemyShooting = GetComponentInChildren<EnemyShooting>();
        playerTest = FindObjectOfType<PlayerHealth>();
        lookAtPlayer = GetComponent<LookAtPlayer>();

        CreateList();
        PickWaitTime();
    }

    private void CreateList()
    {
        paths.Add(location1);
        paths.Add(location2);
        paths.Add(location3);
        paths.Add(location4);
        paths.Add(location5);
        paths.Add(location6);
        ShuffleList();
    }

    private void PickWaitTime()
    {
        waitTime = Random.Range(minRandomRange, maxRandomRange);
    }

    private void ShuffleList()
    {
        paths.Shuffle();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerTest.DeathCheck() == false)
        {
            if (Vector3.Distance(paths[0].position, gameObject.transform.position) < radius)
            {
                Move = false;
                animator.SetBool(isMoveingParam, false);
                enemyShooting.MovingToDest(false);
                lookAtPlayer.enabled = true;
                ShuffleList();
                PickWaitTime();
                StartCoroutine(StopMovement());
            }
            if (Move == true)
            {
                MoveToPoint();
                enemyShooting.MovingToDest(true);
                lookAtPlayer.enabled = false;
            }
        }
        else
        {
            return;
        }
    }

    IEnumerator StopMovement()
    {
        yield return new WaitForSeconds(waitTime);
        Move = true;
    }


    private void MoveToPoint()
    {
        animator.SetBool(isMoveingParam, true);
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, paths[0].position, Time.deltaTime * speed);
        gameObject.transform.LookAt(paths[0]);
    }
}
