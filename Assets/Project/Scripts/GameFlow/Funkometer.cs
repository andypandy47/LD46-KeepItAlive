using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Funkometer : MonoBehaviour
{
    public static Funkometer instance;
    public Image colouredBar;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void SetFunkometer(float spunkLevel)
    {
        var ratio = Mathf.Clamp(spunkLevel / 100, 0, 100);
        colouredBar.transform.localScale = new Vector3(ratio, 1, 1);
    }
}
