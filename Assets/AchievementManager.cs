using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class AchievementManager : MonoBehaviour
{
    [SerializeField] bool PlayingHatVersion = false;
    [SerializeField] bool PlayingAcheivementVersion = false;
    [SerializeField] GameObject hatUnlockedCan;
    [SerializeField] GameObject welcomeScreenHat;
    [SerializeField] GameObject welcomScreenAchive;
    [SerializeField] GameObject exitLight;
    [SerializeField] GameObject exitCollision;
    [SerializeField] GameObject exitScreen;
    [SerializeField] GameObject helpScreen;
    [SerializeField] GameObject hatGuideCan;
    [SerializeField] GameObject achivGuideCan;

    [SerializeField] GameObject deathSquare;
    [SerializeField] GameObject pizzaPizzaPizza;
    [SerializeField] GameObject protectCone;
    [SerializeField] GameObject trobleRelic;
    [SerializeField] GameObject lifeFlashBeforeEyes;
    [SerializeField] GameObject beyondWall;
    [SerializeField] GameObject ecoWarrio;
    [SerializeField] GameObject allHailMe;

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
    private bool hasKilledAllClaimed = false;
    private bool killed9 = false;
    private int hatDrop;
    private bool lifeFlashedClaimed = false;
    private bool claimedPizza = false;
    private bool secretRoomClaimed = false;
    private bool hasStartedGame = false;
    private CharacterAiming characterAiming;
    private bool isExiting = false;
    private GameRestartManager gameRestart;
    private bool pauseGame = false;
    private int enimeiesdead;

    private List<GameObject> hats = new List<GameObject>();
    private Queue popUp = new Queue(); 

    // Start is called before the first frame update
    void Start()
    {
        characterAiming = FindObjectOfType<CharacterAiming>();
        gameRestart = FindObjectOfType<GameRestartManager>();
        var badGuys = FindObjectsOfType<EnemyMovement>();
        enemieCount = badGuys.Length;
        killedFirstFour = false;
        if(PlayingHatVersion == true)
        {
            welcomeScreenHat.SetActive(true);
            tempGameObject = new GameObject();
            hats.Add(tempGameObject);
            hatDrop = Random.Range(1, 10);
            FindHats();
        }

        if(PlayingAcheivementVersion == true)
        {
            welcomScreenAchive.SetActive(true);
        }
    }


    private void StartGame()
    {
        if(gameRestart.NoWelcomeScreen() == true && PlayingHatVersion == true)
        {
            PlayGameHat();
        }
        else if (Input.GetKeyDown(KeyCode.Return) && hasStartedGame == false && PlayingHatVersion == true)
        {
            PlayGameHat();
        }
        else if (Input.GetKeyDown(KeyCode.Return) && hasStartedGame == false && PlayingAcheivementVersion == true)
        {
            PlayingAchivment();
        }
    }

    private void PlayingAchivment()
    {
        hasStartedGame = true;
        welcomScreenAchive.SetActive(false);
        characterAiming.Cursure(true);
    }

    private void PlayGameHat()
    {
        hasStartedGame = true;
        welcomeScreenHat.SetActive(false);
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
            if(PlayingHatVersion == true)
            {
                HelpMenuHats();
                HatGuide();
                CycleHats();
            }

            if(PlayingAcheivementVersion == true)
            {
                HelpMenuAchive();
                AchivmentGuide();
            }
            First4Down();
            killed9Enemies();
            KilledAll();
            ResumeGame();
        }
    }

    private void HelpMenuHats()
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
            UpdatePopUpText();
        }
    }

    private void AchivmentGuide()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            achivGuideCan.SetActive(true);
            pauseGame = true;
            UpdatePopUpText();
        }
    }

    private void HelpMenuAchive()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            //help menu
            pauseGame = true;
        }
    }


    private void ResumeGame()
    {
        if(pauseGame == true && Input.GetKeyDown(KeyCode.Return))
        {
            helpScreen.SetActive(false);
            hatGuideCan.SetActive(false);
            achivGuideCan.SetActive(false);
            pauseGame = false;
        }
    }

    private void First4Down()
    {
        if (enemieCount <= 13 && killedFirstFour == false)
        {
            if(PlayingHatVersion == true)
            {
                hats.Clear();
                hats.Add(cowboy);
                ShowHatUnlocked(true);
            }
            if(PlayingAcheivementVersion == true)
            {
                QueuePopups(deathSquare);
            }
            killedFirstFour = true;
        }
    }

    private void QueuePopups(GameObject popItem)
    {
        popUp.Enqueue(popItem);
        foreach (var obj in popUp)
        {
            popItem.SetActive(true);
            StartCoroutine(DisableCanvas(popItem));
        }
    }

    private void killed9Enemies()
    {
        if (enemieCount == 8 && killed9 == false)
        {
            if(hatDrop <= 4 && PlayingHatVersion == true)
            {
                hats.Add(shower);
                ShowHatUnlocked(true);
            }
            if(PlayingAcheivementVersion == true && characterAiming.CountingBullets() <= 20)
            {
                QueuePopups(ecoWarrio);
            }
            killed9 = true;
        }
    }

    private void KilledAll()
    {
        if(enemieCount == 0 && hasKilledAllClaimed == false)
        {
            if(PlayingHatVersion == true)
            {
                hats.Add(crown);
                ShowHatUnlocked(true);
            }
            if(PlayingAcheivementVersion == true)
            {
                QueuePopups(allHailMe);
            }
            hasKilledAllClaimed = true;
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
        if (coneAmount >= 8)
        {
            if(PlayingHatVersion == true)
            {
                hats.Add(miner);
                ShowHatUnlocked(true);
            }
            if(PlayingAcheivementVersion == true)
            {
                QueuePopups(protectCone);
            }
        }

    }

    public void Pizza(bool pizza)
    {
        if (pizza == true && claimedPizza == false)
        {
            if(PlayingHatVersion == true)
            {
                hats.Add(police);
                ShowHatUnlocked(true);
            }
            if(PlayingAcheivementVersion == true)
            {
                QueuePopups(pizzaPizzaPizza);
            }
            claimedPizza = true;
        }
    }

    public void SecretRoomEntered(bool entered)
    {
        if(entered == true && secretRoomClaimed == false)
        {
            if(PlayingHatVersion == true)
            {
                hats.Add(magicHat);
                ShowHatUnlocked(true);
            }
            if(PlayingAcheivementVersion == true)
            {
                QueuePopups(beyondWall);
            }
            secretRoomClaimed = true;
        }
    }

    public void LifeFlash(bool flashed)
    {
        if(flashed == true && lifeFlashedClaimed == false)
        {
            if(PlayingHatVersion == true)
            {
                hats.Add(viking);
                ShowHatUnlocked(true);
            }
            if(PlayingAcheivementVersion == true)
            {
                QueuePopups(lifeFlashBeforeEyes);
            }
            lifeFlashedClaimed = true;
        }
    }

    public void RobotHead(int head)
    {
        robotHeadCollectio = robotHeadCollectio + head;
         if(robotHeadCollectio == 5)
        {
            if(PlayingHatVersion == true)
            {
                hats.Add(sombrero);
                ShowHatUnlocked(true);
            }
            if(PlayingAcheivementVersion == true)
            {
                QueuePopups(trobleRelic);
            }
        }
    }

    public void ShowHatUnlocked(bool show)
    {
        print("Called canvas");
        if (show == true)
        {
            StartCoroutine(DisableCanvas(hatUnlockedCan));
            hatUnlockedCan.SetActive(true);
            show = false;
        }
    }

    IEnumerator DisableCanvas(GameObject canvas)
    {
        yield return new WaitForSeconds(5f);
        canvas.SetActive(false);
    }

    public bool GameInSession()
    {
        return hasStartedGame;
    }

    public void CanExit(bool exit)
    {
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

    private void UpdatePopUpText()
    {
        GameObject kill4Text = GameObject.Find("Kill4");
        GameObject kill9Text = GameObject.Find("Kill9");
        GameObject killedAllText = GameObject.Find("KilledAll");
        GameObject robotText = GameObject.Find("RobotText");
        GameObject traffic = GameObject.Find("TrafficText");
        GameObject PizzaText = GameObject.Find("PizzaText");
        GameObject secretText = GameObject.Find("Secret");
        GameObject AlmostDead = GameObject.Find("AlmostDead");

        GameObject AchiveKilled4 = GameObject.Find("AchiveKill4");
        GameObject AchiveKill9 = GameObject.Find("AchiveKill9");
        GameObject AchiveKillAll = GameObject.Find("AchiveKillAll");
        GameObject AchiveRobotText = GameObject.Find("AchiveRobotText");
        GameObject AchiveTraffic = GameObject.Find("AchiveTraffic");
        GameObject AchivePizza = GameObject.Find("AchivePizza");
        GameObject AchiveSecret = GameObject.Find("AchiveSecret");
        GameObject AchiveAlmostDied = GameObject.Find("AchiveAlmostDied");

        if (killedFirstFour == false && PlayingHatVersion == true)
        {
            kill4Text.GetComponent<Text>().text = (enimeiesdead + "/4");
        }
        if (killedFirstFour == false && PlayingAcheivementVersion == true)
        {
            AchiveKilled4.GetComponent<Text>().text = (enimeiesdead + "/4");
        }
        if (killedFirstFour == true && PlayingHatVersion == true)
        {
            kill4Text.GetComponent<Text>().text = ("Unlocked");
        }
        if (killedFirstFour == true && PlayingAcheivementVersion == true)
        {
            AchiveKilled4.GetComponent<Text>().text = ("Unlocked");
        }
        if (enimeiesdead >= 8 && killed9 == false)
        {
            kill9Text.GetComponent<Text>().text = ("Luck failed you");
        }
        if(killed9 == true && PlayingHatVersion == true)
        {
            kill9Text.GetComponent<Text>().text = ("Unlocked");
        }
        if (killed9 == true && PlayingAcheivementVersion == true)
        {
            AchiveKill9.GetComponent<Text>().text = ("Unlocked");
        }
        if (killed9 != true && PlayingHatVersion == true)
        {
            kill9Text.GetComponent<Text>().text = ("Locked");
        }
        if (hasKilledAllClaimed == true && PlayingHatVersion == true)
        {
            killedAllText.GetComponent<Text>().text = ("Unlocked");
        }
        if (hasKilledAllClaimed == true && PlayingAcheivementVersion == true)
        {
            AchiveKillAll.GetComponent<Text>().text = ("Unlocked");
        }
        if (robotHeadCollectio != 5 && PlayingHatVersion == true)
        {
            robotText.GetComponent<Text>().text = (robotHeadCollectio + "/5");
        }
        if (robotHeadCollectio != 5 && PlayingAcheivementVersion == true)
        {
            AchiveRobotText.GetComponent<Text>().text = (robotHeadCollectio + "/5");
        }
        if (robotHeadCollectio == 5 && PlayingHatVersion == true)
        {
            robotText.GetComponent<Text>().text = ("Unlocked");
        }
        if (robotHeadCollectio == 5 && PlayingAcheivementVersion == true)
        {
            AchiveRobotText.GetComponent<Text>().text = ("Unlocked");
        }
        if (coneAmount != 8 && PlayingHatVersion == true)
        {
            traffic.GetComponent<Text>().text = (coneAmount + "/8");
        }
        if (coneAmount != 8 && PlayingAcheivementVersion == true)
        {
            AchiveTraffic.GetComponent<Text>().text = (coneAmount + "/8");
        }
        if (coneAmount >=8 && PlayingHatVersion == true)
        {
            traffic.GetComponent<Text>().text = ("Unlocked");
        }
        if (coneAmount >= 8 && PlayingAcheivementVersion == true)
        {
            AchiveTraffic.GetComponent<Text>().text = ("Unlocked");
        }
        if (claimedPizza == false && PlayingHatVersion == true)
        {
            PizzaText.GetComponent<Text>().text = ("Locked");
        }
        if (claimedPizza == false && PlayingAcheivementVersion == true)
        {
            AchivePizza.GetComponent<Text>().text = ("Locked");
        }
        if (claimedPizza == true && PlayingHatVersion == true)
        {
            PizzaText.GetComponent<Text>().text = ("Unlocked");
        }
        if (claimedPizza == true && PlayingAcheivementVersion == true)
        {
            AchivePizza.GetComponent<Text>().text = ("Unlocked");
        }
        if (secretRoomClaimed == true && PlayingHatVersion == true)
        {
            secretText.GetComponent<Text>().text = ("Unlocked");
        }
        if (secretRoomClaimed == true && PlayingAcheivementVersion == true)
        {
            AchiveSecret.GetComponent<Text>().text = ("Unlocked");
        }
        if (lifeFlashedClaimed == true && PlayingHatVersion == true)
        {
            AlmostDead.GetComponent<Text>().text = ("Unlocked");
        }
        if (lifeFlashedClaimed == true && PlayingAcheivementVersion == true)
        {
            AchiveAlmostDied.GetComponent<Text>().text = ("Unlocked");
        }
    }
}
