using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseMenu : MonoBehaviour
{
    public GameObject loseMenu;

    public void Start()
    {
        loseMenu.SetActive(false);
    }

    public void Retry()
    {
        GameController.instance.RestartGame();
    }

    public void SetLoseMenu(bool value)
    {
        loseMenu.SetActive(value);
    }
}
