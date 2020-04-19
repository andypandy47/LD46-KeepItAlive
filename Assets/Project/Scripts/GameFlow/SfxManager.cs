using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SfxManager : MonoBehaviour
{
    public static SfxManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    [FMODUnity.EventRef]
    public string dodgeWhoosh;

    [FMODUnity.EventRef]
    public string playerHit;

    [FMODUnity.EventRef]
    public string enemyHit;
}
