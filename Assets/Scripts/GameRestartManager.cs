using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRestartManager : MonoBehaviour
{
    [SerializeField] AudioSource backNoice;

    private bool noWelcome = false;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        backNoice.Play();
    }

    public void GameReset(bool reset)
    {
        noWelcome = reset;
    }

    public bool NoWelcomeScreen()
    {
        return noWelcome;
    }
}
