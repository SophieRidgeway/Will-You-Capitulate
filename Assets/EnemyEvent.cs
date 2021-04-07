using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEvent : MonoBehaviour
{
    private int enemieCount;
    private GameObject crown;

    // Start is called before the first frame update
    void Start()
    {
        var badGuys = FindObjectsOfType<EnemyMovement>();
        enemieCount = badGuys.Length;
        crown = GameObject.Find("Cyborg/Hips/Spine/Spine1/Spine2/Neck/Head/Crown");
    }

    // Update is called once per frame
    void Update()
    {
        print(enemieCount);
        if(enemieCount <= 0)
        {
            print("They are no more");
            crown.SetActive(enabled);
        }
    }

    public void EnemyCount(int death)
    {
        enemieCount = enemieCount - death;
    }
}
