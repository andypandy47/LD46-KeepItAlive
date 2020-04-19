using UnityEngine;
using DG.Tweening;
using System.Collections;

public class Effects : MonoBehaviour
{
    public static Effects instance;

    public static float myTimeScale = 1;
    private static Vector3 originalCamPos;

    public GameObject floatyScorePrefab;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void Start()
    {
         originalCamPos = Camera.main.transform.position;
    }
    public static void CamShake()
    {
        Camera.main.DOShakePosition(0.1f, 0.8f, 18, 90).OnComplete(() => { Camera.main.gameObject.transform.position = originalCamPos; });
    }

    public void SpawnFloatyScore(Vector3 position, int score)
    {
        FloatingScore floatyScore = Instantiate(floatyScorePrefab).GetComponent<FloatingScore>();
        floatyScore.transform.position = position;
        floatyScore.Init(score);
    }

    public void PauseForSeconds(float seconds)
    {
        StartCoroutine(PauseFor(seconds));
    }

    public IEnumerator PauseFor(float seconds)
    {
        myTimeScale = 0;
        Time.timeScale = 0;
        GameController.instance.ToggleTweenPause();
        Debug.Log("Pause");

        yield return new WaitForSecondsRealtime(seconds);

        myTimeScale = 1;
        Time.timeScale = 1;
        GameController.instance.ToggleTweenPause();
        Debug.Log("Unpause");
    }
}
