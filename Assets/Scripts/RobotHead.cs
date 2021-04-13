using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotHead : MonoBehaviour
{
    private AchievementManager achievement;

    // Start is called before the first frame update
    void Start()
    {
        achievement = FindObjectOfType<AchievementManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            achievement.RobotHead(1);
            Destroy(this.gameObject);
        }
    }
}
