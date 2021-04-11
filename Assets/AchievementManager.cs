using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class AchievementManager : MonoBehaviour
{
    [SerializeField] GameObject hatUnlockedCan;
    [SerializeField] GameObject welcomeScreen;
    [SerializeField] GameObject exitLight;
    [SerializeField] GameObject exitCollision;
    [SerializeField] GameObject exitScreen;
    [SerializeField] GameObject helpScreen;
    [SerializeField] GameObject hatGuideCan;

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
    private GameRestartManager gameRestart;
    private bool pauseGame = false;
    private int enimeiesdead;

    private List<GameObject> hats = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        characterAiming = FindObjectOfType<CharacterAiming>();
        gameRestart = FindObjectOfType<GameRestartManager>();
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
        if(gameRestart.NoWelcomeScreen() == true)
        {
            PlayGame();
        }
        else if (Input.GetKeyDown(KeyCode.Return) && hasStartedGame == false)
        {
            PlayGame();
        }
    }

    private void PlayGame()
    {
        hasStartedGame = true;
        welcomeScreen.SetActive(false);
        characterAiming.Cursure(true);
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
            HelpMenu();
            HatGuide();
            CycleHats();
            First4Down();
            killed9Enemies();
            KilledAll();
            ResumeGame();
        }
    }

    private void HelpMenu()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            helpScreen.SetActive(true);
            pauseGame = true;
        }
    }

    private void HatGuide()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            hatGuideCan.SetActive(true);
            pauseGame = true;
            UpdateHatText();
        }
    }

    private void ResumeGame()
    {
        if(pauseGame == true && Input.GetKeyDown(KeyCode.Return))
        {
            helpScreen.SetActive(false);
            hatGuideCan.SetActive(false);
            pauseGame = false;
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
        enimeiesdead = enimeiesdead + death;
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

    public bool GamePaued()
    {
        return pauseGame;
    }

    private void UpdateHatText()
    {
        GameObject kill4Text = GameObject.Find("Kill4");
        GameObject kill9Text = GameObject.Find("Kill9");
        GameObject killedAllText = GameObject.Find("KilledAll");
        GameObject robotText = GameObject.Find("RobotText");
        GameObject traffic = GameObject.Find("TrafficText");
        GameObject PizzaText = GameObject.Find("PizzaText");
        GameObject secretText = GameObject.Find("Secret");
        GameObject AlmostDead = GameObject.Find("AlmostDead");

        if (killedFirstFour == false)
        {
            kill4Text.GetComponent<Text>().text = (enimeiesdead + "/4");
        }
        if(killedFirstFour == true)
        {
            kill4Text.GetComponent<Text>().text = ("Unlocked");
        }
        if(enimeiesdead >= 8 && Killed9Hat == false)
        {
            kill9Text.GetComponent<Text>().text = ("Luck failed you");
        }
        if(Killed9Hat == true)
        {
            kill9Text.GetComponent<Text>().text = ("Unlocked");
        }
        if (killedAllHat == true)
        {
            killedAllText.GetComponent<Text>().text = ("Unlocked");
        }
        if(robotHeadCollectio != 5)
        {
            robotText.GetComponent<Text>().text = (robotHeadCollectio + "/5");
        }
        if(robotHeadCollectio == 5)
        {
            robotText.GetComponent<Text>().text = ("Unlocked");
        }
        if(coneAmount != 8)
        {
            traffic.GetComponent<Text>().text = (coneAmount + "/8");
        }
        if(coneAmount >=8)
        {
            traffic.GetComponent<Text>().text = ("Unlocked");
        }
        if(pizzaHatClaimed == false)
        {
            PizzaText.GetComponent<Text>().text = ("Locked");
        }
        if(pizzaHatClaimed == true)
        {
            PizzaText.GetComponent<Text>().text = ("Unlocked");
        }
        if(secretRoomClaimed == true)
        {
            secretText.GetComponent<Text>().text = ("Unlocked");
        }
        if(lifeFlashedClaimed == true)
        {
            AlmostDead.GetComponent<Text>().text = ("Unlocked");
        }
    }
}
