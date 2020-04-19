using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;

    private void Start()
    {
        pauseMenu.SetActive(false);
    }

    public void SetPauseDisplay(bool value)
    {
        pauseMenu.SetActive(value);
    }

    public void Resume()
    {
        InGameMenus.instance.SetUIMenu(InGameMenuType.Pause, false);
    }

    public void ExitToMainMenu()
    {
        Application.Quit();
    }
}
