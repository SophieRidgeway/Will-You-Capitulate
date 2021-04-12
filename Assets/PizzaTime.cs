using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaTime : MonoBehaviour
{
    private int pizzaHitAmount;
    private AchievementManager achive;

    // Start is called before the first frame update
    void Start()
    {
        achive = FindObjectOfType<AchievementManager>();
    }

    public void PizzaHit(int hit)
    {
        pizzaHitAmount = pizzaHitAmount + hit;

        if(pizzaHitAmount >= 3)
        {
            achive.Pizza(true);
        }
    }
}
