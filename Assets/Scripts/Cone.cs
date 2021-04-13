using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cone : MonoBehaviour
{
    private AchievementManager achive;
    private int hitCount;

    // Start is called before the first frame update
    void Start()
    {
        achive = FindObjectOfType<AchievementManager>();
    }

    public void ConeHit(int hit)
    {
        hitCount = hitCount + hit;
        if(hitCount == 1)
        {
            achive.ConeAchive(1);
        }
    }
}
