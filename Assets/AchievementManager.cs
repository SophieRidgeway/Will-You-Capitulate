using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AchievementManager : MonoBehaviour
{
    public GameObject hatUnlockedCan;
    public GameObject welcomeScreen;
    public GameObject exitLight;
    public GameObject exitCollision;
    public GameObject exitScreen;

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
    private bool lifeFlashedClaimed = false;
    private bool pizzaHatClaimed = false;
    private bool secretRoomClaimed = false;
    private bool hasStartedGame = false;
    private CharacterAiming characterAiming;
    private bool isExiting = false;

    private List<GameObject> hats = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        characterAiming = FindObjectOfType<CharacterAiming>();
        var badGuys = FindObjectsOfType<EnemyMovement>();
        enemieCount = badGuys.Length;
        tempGameObject = new GameObject();
        hats.Add(tempGameObject);
        killedFirstFour = false;
        hatDrop = Random.Range(1, 10);
        FindHats();
    }

    private void StartGame()
    {
        if(Input.GetKeyDown(KeyCode.Return) && hasStartedGame == false)
        {
            hasStartedGame = true;
            welcomeScreen.SetActive(false);
            characterAiming.Cursure(true);
        }
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
        StartGame();
        if(hasStartedGame)
        {
            CycleHats();
            First4Down();
            killed9Enemies();
            KilledAll();
        }
    }

    private void First4Down()
    {
        if (enemieCount == 13 && killedFirstFour == false)
        {
            hats.Clear();
            hats.Add(cowboy);
            ShowHatUnlocked(true);
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
                ShowHatUnlocked(true);
                Killed9Hat = true;
            }
        }
    }

    private void KilledAll()
    {
        if(enemieCount == 0 && killedAllHat == false)
        {
            hats.Add(crown);
            ShowHatUnlocked(true);
            killedAllHat = true;
            exitLight.SetActive(true);
            exitCollision.SetActive(true);
        }

        if(isExiting == true)
        {
            exitScreen.SetActive(true);
            if(Input.GetKey(KeyCode.Escape))
            {
                ExitApplication(true);
            }
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
        if (coneAmount == 8)
        {
            hats.Add(miner);
            ShowHatUnlocked(true);
        }

    }

    public void PizzaHat(bool pizza)
    {
        if (pizza == true && pizzaHatClaimed == false)
        {
            hats.Add(police);
            ShowHatUnlocked(true);
            pizzaHatClaimed = true;
        }
    }

    public void SecretRoomEntered(bool entered)
    {
        if(entered == true && secretRoomClaimed == false)
        {
            hats.Add(magicHat);
            ShowHatUnlocked(true);
            secretRoomClaimed = true;
        }
    }

    public void LifeFlash(bool flashed)
    {
        if(flashed == true && lifeFlashedClaimed == false)
        {
            hats.Add(viking);
            ShowHatUnlocked(true);
            lifeFlashedClaimed = true;
        }
    }

    public void RobotHead(int head)
    {
        robotHeadCollectio = robotHeadCollectio + head;
         if(robotHeadCollectio == 5)
        {
            hats.Add(sombrero);
            ShowHatUnlocked(true);
        }
    }

    public void ShowHatUnlocked(bool show)
    {
        print("Called canvas");
        if (show == true)
        {
            StartCoroutine(DisableCanvas());
            hatUnlockedCan.SetActive(true);
            show = false;
        }
    }

    IEnumerator DisableCanvas()
    {
        yield return new WaitForSeconds(3f);
        hatUnlockedCan.SetActive(false);
    }

    public bool GameInSession()
    {
        return hasStartedGame;
    }

    public void CanExit(bool exit)
    {
        print("In The achivment script");
        isExiting = exit;
    }

    private void ExitApplication(bool exit)
    {
        if(exit == true)
        {
            Application.Quit();
        }
    }
}
