using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyColissionCheck : MonoBehaviour
{
    [SerializeField] int MaxHealth = 5;

    private int currentHealth;
    private Animator animator;
    private int isDeadPar = Animator.StringToHash("IsDead");
    private EnemyMovement enemyMovement;
    private EnemyShooting enemyShooting;
    private Vector3 lower = new Vector3 (100f, 100f, 100f);
    private EnemyEvent enemyEvent;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = MaxHealth;
        gameObject.tag = "Enemy";
        animator = GetComponent<Animator>();
        enemyMovement = GetComponent<EnemyMovement>();
        enemyShooting = GetComponentInChildren<EnemyShooting>();
        enemyEvent = FindObjectOfType<EnemyEvent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0)
        {
            Dying();
        }
        if (Vector3.Distance(lower, gameObject.transform.position) < 1f)
        {
            enemyEvent.EnemyCount(1);
            Destroy(gameObject);
        }
    }

    private void Dying()
    {
        animator.SetBool(isDeadPar, true);
        enemyMovement.enabled = false;
        enemyShooting.enabled = false;
        StartCoroutine(MoveToDestroy());
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            currentHealth = currentHealth - 1;
        }
    }
    IEnumerator MoveToDestroy()
    {
        yield return new WaitForSeconds(2f);
        lower = new Vector3(gameObject.transform.position.x, -5f, gameObject.transform.position.z);
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, lower, Time.deltaTime * 2);
    }

    public void TakeDamage(int damage)
    {
        currentHealth = currentHealth - damage;
    }

}