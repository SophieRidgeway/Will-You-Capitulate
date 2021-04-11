using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRestartManager : MonoBehaviour
{
    private bool noWelcome = false;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
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
