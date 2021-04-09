using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementManager : MonoBehaviour
{
    private GameObject crown;
    private GameObject magicHat;
    private GameObject cowboy;
    private GameObject miner;
    private GameObject police;
    private GameObject shower;
    private GameObject sombrero;
    private GameObject viking;

    private int enemieCount;
    private int currentHatInt;
    private int coneAmount;
    private GameObject currentHat;
    private GameObject tempGameObject;
    private bool killedFirstFour;
    private int robotHeadCollectio;

    private List<GameObject> hats = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        var badGuys = FindObjectsOfType<EnemyMovement>();
        enemieCount = badGuys.Length;
        tempGameObject = new GameObject();
        hats.Add(tempGameObject);
        killedFirstFour = false;
        FindHats();

        //AddToListOfHats();
    }

    private void FindHats()
    {
        crown = GameObject.Find("Cyborg/Hips/Spine/Spine1/Spine2/Neck/Head/Crown");
        magicHat = GameObject.Find("Cyborg/Hips/Spine/Spine1/Spine2/Neck/Head/MagicianHat");
        cowboy = GameObject.Find("Cyborg/Hips/Spine/Spine1/Spine2/Neck/Head/CowboyHat");
        miner = GameObject.Find("Cyborg/Hips/Spine/Spine1/Spine2/Neck/Head/MinerHat");
        police = GameObject.Find("Cyborg/Hips/Spine/Spine1/Spine2/Neck/Head/PoliceCap");
        shower = GameObject.Find("Cyborg/Hips/Spine/Spine1/Spine2/Neck/Head/ShowerCap");
        sombrero = GameObject.Find("Cyborg/Hips/Spine/Spine1/Spine2/Neck/Head/Sombrero");
        viking = GameObject.Find("Cyborg/Hips/Spine/Spine1/Spine2/Neck/Head/VikingHelmet");
    }

    private void AddToListOfHats()
    {
        hats.Add(crown);
        //hats.Add(magicHat);
        //hats.Add(cowboy);
        //hats.Add(miner);
        //hats.Add(police);
        hats.Add(shower);
        //hats.Add(sombrero);
        //hats.Add(viking);
    }

    // Update is called once per frame
    void Update()
    {
        CycleHats();

        if (enemieCount == 4 && killedFirstFour == false)
        {
            hats.Clear();
            hats.Add(cowboy);
            killedFirstFour = true;
        }
    }

    private void CycleHats()
    {
        if (Input.GetKeyDown("."))
        {
            currentHatInt += 1;
            currentHat.SetActive(false);
        }

        if (Input.GetKeyDown(","))
        {
            currentHatInt -= 1;
            currentHat.SetActive(false);
        }

        if (currentHatInt >= hats.Count)
        {
            currentHatInt = 0;
        }

        if (currentHatInt < 0)
        {
            currentHatInt = hats.Count - 1;
        }

        currentHat = hats[currentHatInt];

        if (currentHat.activeInHierarchy != true)
        {
            currentHat.SetActive(true);
        }
    }

    public void EnemyCount(int death)
    {
        enemieCount = enemieCount - death;
    }

    public void ConeAchive(int cones)
    {
        coneAmount = coneAmount + cones;
        if (coneAmount == 4)
        {
            hats.Add(miner);
        }

    }

    public void PizzaHat(bool pizza)
    {
        bool claimed = false;

        if (pizza == true && claimed == false)
        {
            hats.Add(police);
            claimed = true;
        }
    }

    public void SecretRoomEntered(bool entered)
    {
        bool claimed = false;

        if(entered == true && claimed == false)
        {
            hats.Add(magicHat);
            claimed = true;
        }
    }

    public void LifeFlash(bool flashed)
    {
        bool clamied = false;

        if(flashed == true && clamied == false)
        {
            hats.Add(viking);
            clamied = true;
        }
    }

    public void RobotHead(int head)
    {
        robotHeadCollectio = robotHeadCollectio + head;
         if(robotHeadCollectio == 5)
        {
            hats.Add(sombrero);
        }
    }
}
