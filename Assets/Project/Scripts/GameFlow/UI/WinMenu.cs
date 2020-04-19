using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinMenu : MonoBehaviour
{
    public GameObject winMenu;

    public void Start()
    {
        winMenu.SetActive(false);
    }
    
    public void SetWinMenu(bool value)
    {
        winMenu.SetActive(value);
    }
}
