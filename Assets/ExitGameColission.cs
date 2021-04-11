using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitGameColission : MonoBehaviour
{
    private AchievementManager achievement;

    private void Start()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            achievement = FindObjectOfType<AchievementManager>();
            achievement.CanExit(true);
        }
    }
}
