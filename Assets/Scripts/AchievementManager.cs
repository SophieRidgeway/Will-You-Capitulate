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
    [SerializeField] bool PlayingNormalVersion = false;
    [SerializeField] GameObject hatUnlockedCan;
    [SerializeField] GameObject welcomeScreenHat;
    [SerializeField] GameObject welcomScreenAchive;
    [SerializeField] GameObject welcomeScreenNormal;
    [SerializeField] GameObject exitLight;
    [SerializeField] GameObject exitCollision;
    [SerializeField] GameObject exitScreen;
    [SerializeField] GameObject helpScreen;
    [SerializeField] GameObject helpScreenAchivment;
    [SerializeField] GameObject helpScreenNormal;
    [SerializeField] GameObject hatGuideCan;
    [SerializeField] GameObject achivGuideCan;
    [SerializeField] GameObject playInformation;
    [SerializeField] GameObject beaconText;

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
    private int acomplishedamount;
    private float timer;
    private bool canTime = true;

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

        if(PlayingNormalVersion == true)
        {
            welcomeScreenNormal.SetActive(true);
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
        else if (gameRestart.NoWelcomeScreen() == true && PlayingAcheivementVersion == true)
        {
            PlayingAchivment();
        }
        else if (Input.GetKeyDown(KeyCode.Return) && hasStartedGame == false && PlayingNormalVersion == true)
        {
            PlayNormal();
        }
    }

    private void PlayNormal()
    {
        hasStartedGame = true;
        welcomeScreenNormal.SetActive(false);
        characterAiming.Cursure(true);
        DisplayGameInformation();
    }

    private void PlayingAchivment()
    {
        hasStartedGame = true;
        welcomScreenAchive.SetActive(false);
        characterAiming.Cursure(true);
        DisplayGameInformation();
    }

    private void PlayGameHat()
    {
        hasStartedGame = true;
        welcomeScreenHat.SetActive(false);
        characterAiming.Cursure(true);
        DisplayGameInformation();
    }

    private void DisplayGameInformation()
    {
        playInformation.SetActive(true);
        StartCoroutine(DisableCanvasInformation());
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
            if (canTime == true)
            {
                timer += Time.deltaTime;
            }
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

            if(PlayingNormalVersion == true)
            {
                HelpMenuNormal();
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
            helpScreenAchivment.SetActive(true);
            pauseGame = true;
        }
    }

    private void HelpMenuNormal()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            helpScreenNormal.SetActive(true);
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
            helpScreenAchivment.SetActive(false);
            helpScreenNormal.SetActive(false);
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
            CompletionAmount(1);
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
        if (enemieCount == 8 && killed9 == false && PlayingHatVersion == true)
        {
            if(hatDrop <= 4)
            {
                hats.Add(shower);
                ShowHatUnlocked(true);
                CompletionAmount(1);
            }
            killed9 = true;
        }
        if (PlayingAcheivementVersion == true && characterAiming.CountingBullets() <= 20 && enemieCount == 8 && killed9 == false)
        {
            QueuePopups(ecoWarrio);
            killed9 = true;
            CompletionAmount(1);
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
            CompletionAmount(1);
            if(PlayingHatVersion == true || PlayingAcheivementVersion == true)
            {
                StartCoroutine(WaitToShowEndText());
            }
            else
            {
                beaconText.SetActive(true);
                exitLight.SetActive(true);
                exitCollision.SetActive(true);
                StartCoroutine(StopBeaconText());
            }
        }

        if(isExiting == true)
        {
            GameEndingHandle();

            if (Input.GetKey(KeyCode.Escape))
            {
                ExitApplication(true);
            }
        }
    }

    private void GameEndingHandle()
    {
        exitScreen.SetActive(true);
        if (PlayingHatVersion == true || PlayingAcheivementVersion == true || PlayingNormalVersion == true)
        {
            canTime = false;
            GameObject Complet = GameObject.Find("Complet");
            GameObject timeTook = GameObject.Find("Time");
            float minutes = Mathf.FloorToInt(timer / 60);
            float seconds = Mathf.FloorToInt(timer % 60);

            timeTook.SetActive(true);
            timeTook.GetComponent<Text>().text = ("You took " + (string.Format("{0:00}:{1:00}", minutes, seconds) + " to complet the mission"));

            if (PlayingHatVersion == true)
            {
                Complet.SetActive(true);
                Complet.GetComponent<Text>().text = ("You collected " + acomplishedamount + " out of 8 hats!");
            }
            if (PlayingAcheivementVersion == true)
            {
                Complet.SetActive(true);
                Complet.GetComponent<Text>().text = ("You completed " + acomplishedamount + " out of 8 achievments!");
            }
            if(PlayingNormalVersion == true)
            {
                Complet.SetActive(true);
                Complet.GetComponent<Text>().text = ("       ");
            }
        }
    }

    IEnumerator WaitToShowEndText()
    {
        yield return new WaitForSeconds(5f);
        beaconText.SetActive(true);
        exitLight.SetActive(true);
        exitCollision.SetActive(true);
        StartCoroutine(StopBeaconText());
    }

    IEnumerator StopBeaconText()
    {
        yield return new WaitForSeconds(10f);
        beaconText.SetActive(false);
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
            if(PlayingHatVersion == true)
            {
                hats.Add(miner);
                ShowHatUnlocked(true);
            }
            if(PlayingAcheivementVersion == true)
            {
                QueuePopups(protectCone);
            }
            CompletionAmount(1);
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
            CompletionAmount(1);
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
            CompletionAmount(1);
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
            CompletionAmount(1);
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
            CompletionAmount(1);
        }
    }

    public void ShowHatUnlocked(bool show)
    {
        if (show == true)
        {
            hatUnlockedCan.SetActive(true);
            StartCoroutine(DisableCanvas(hatUnlockedCan));
            show = false;
        }
    }

    IEnumerator DisableCanvas(GameObject canvas)
    {
        yield return new WaitForSeconds(5f);
        canvas.SetActive(false);
    }

    IEnumerator DisableCanvasInformation()
    {
        yield return new WaitForSeconds(15f);
        playInformation.SetActive(false);
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
        GameObject kill4Text, kill9Text, killedAllText, robotText, traffic, PizzaText, secretText, AlmostDead;
        FindHatText(out kill4Text, out kill9Text, out killedAllText, out robotText, out traffic, out PizzaText, out secretText, out AlmostDead);

        GameObject AchiveKilled4, AchiveKill9, AchiveKillAll, AchiveRobotText, AchiveTraffic, AchivePizza, AchiveSecret, AchiveAlmostDied;
        FindAchiveText(out AchiveKilled4, out AchiveKill9, out AchiveKillAll, out AchiveRobotText, out AchiveTraffic, out AchivePizza, out AchiveSecret, out AchiveAlmostDied);

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
        if (enimeiesdead >= 8 && killed9 == false && PlayingHatVersion)
        {
            kill9Text.GetComponent<Text>().text = ("Luck failed you");
        }
        if (killed9 == true && PlayingHatVersion == true)
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
        if (coneAmount == 8 && PlayingHatVersion == true)
        {
            traffic.GetComponent<Text>().text = ("Unlocked");
        }
        if (coneAmount == 8 && PlayingAcheivementVersion == true)
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

    private static void FindAchiveText(out GameObject AchiveKilled4, out GameObject AchiveKill9, out GameObject AchiveKillAll, out GameObject AchiveRobotText, out GameObject AchiveTraffic, out GameObject AchivePizza, out GameObject AchiveSecret, out GameObject AchiveAlmostDied)
    {
        AchiveKilled4 = GameObject.Find("AchiveKill4");
        AchiveKill9 = GameObject.Find("AchiveKill9");
        AchiveKillAll = GameObject.Find("AchiveKillAll");
        AchiveRobotText = GameObject.Find("AchiveRobotText");
        AchiveTraffic = GameObject.Find("AchiveTraffic");
        AchivePizza = GameObject.Find("AchivePizza");
        AchiveSecret = GameObject.Find("AchiveSecret");
        AchiveAlmostDied = GameObject.Find("AchiveAlmostDied");
    }

    private static void FindHatText(out GameObject kill4Text, out GameObject kill9Text, out GameObject killedAllText, out GameObject robotText, out GameObject traffic, out GameObject PizzaText, out GameObject secretText, out GameObject AlmostDead)
    {
        kill4Text = GameObject.Find("Kill4");
        kill9Text = GameObject.Find("Kill9");
        killedAllText = GameObject.Find("KilledAll");
        robotText = GameObject.Find("RobotText");
        traffic = GameObject.Find("TrafficText");
        PizzaText = GameObject.Find("PizzaText");
        secretText = GameObject.Find("Secret");
        AlmostDead = GameObject.Find("AlmostDead");
    }

    private void CompletionAmount(int adding)
    {
        acomplishedamount = acomplishedamount + adding;
    }
}
