using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cone : MonoBehaviour
{
    private EnemyEvent enemyEvent;
    private int hitCount;

    // Start is called before the first frame update
    void Start()
    {
        enemyEvent = FindObjectOfType<EnemyEvent>();
    }

    public void ConeHit(int hit)
    {
        hitCount = hitCount + hit;
        if(hitCount == 1)
        {
            enemyEvent.ConeAchive(1);
        }
    }
}
