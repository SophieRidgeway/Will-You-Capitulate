using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

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
    private bool killedAllHat = false;
    private bool Killed9Hat = false;
    private int hatDrop;

    private List<GameObject> hats = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        var badGuys = FindObjectsOfType<EnemyMovement>();
        enemieCount = badGuys.Length;
        tempGameObject = new GameObject();
        hats.Add(tempGameObject);
        killedFirstFour = false;
        hatDrop = Random.Range(1, 10);
        FindHats();
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

    // Update is called once per frame
    void Update()
    {
        CycleHats();
        First4Down();
        killed9Enemies();
        KilledAll();
    }

    private void First4Down()
    {
        if (enemieCount == 13 && killedFirstFour == false)
        {
            hats.Clear();
            hats.Add(cowboy);
            killedFirstFour = true;
        }
    }

    private void killed9Enemies()
    {
        if (enemieCount == 8 && Killed9Hat == false)
        {
            if(hatDrop <= 4)
            {
                hats.Add(shower);
                Killed9Hat = true;
            }
        }
    }

    private void KilledAll()
    {
        if(enemieCount == 0 && killedAllHat == false)
        {
            hats.Add(crown);
            killedAllHat = true;
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
