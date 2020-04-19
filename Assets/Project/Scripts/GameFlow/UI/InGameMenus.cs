using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InGameMenuType
{
    Pause,
    Win,
    Lose
}
public class InGameMenus : MonoBehaviour
{
    public static InGameMenus instance;
    public static bool paused;
    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private PauseMenu pauseMenu;
    private WinMenu winMenu;
    private LoseMenu loseMenu;

    private void Start()
    {
        pauseMenu = GetComponent<PauseMenu>();
        winMenu = GetComponent<WinMenu>();
        loseMenu = GetComponent<LoseMenu>();
    }

    public void SetUIMenu(InGameMenuType menuType, bool isOpen)
    {

        switch(menuType)
        {
            case InGameMenuType.Pause:
                MusicManager.instance.SetPause(isOpen);
                pauseMenu.SetPauseDisplay(isOpen);
                TogglePause();
                break;
            case InGameMenuType.Win:
                TogglePause();
                winMenu.SetWinMenu(isOpen);
                break;
            case InGameMenuType.Lose:
                TogglePause();
                loseMenu.SetLoseMenu(isOpen);
                break;
        }
    }

    public void TogglePause()
    {
        paused = !paused;
        if (paused)
        {
            Time.timeScale = 0;
            Effects.myTimeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
            Effects.myTimeScale = 1;
        }

        GameController.instance.ToggleTweenPause();

        //SetUIMenu(InGameMenuType.Pause, paused);
    }
}
