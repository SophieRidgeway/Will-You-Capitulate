using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    private Transform target;

    // Start is called before the first frame update
    void Start()
    {
        target = FindObjectOfType<PlayerHealth>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPostition = new Vector3(target.position.x, gameObject.transform.position.y, target.position.z);
        gameObject.transform.LookAt(targetPostition);
    }
}
