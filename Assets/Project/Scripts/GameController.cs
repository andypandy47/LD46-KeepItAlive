using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public enum GameState
{
    TutorialScreen,
    InPlay
}

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public static float funkLevel;
    public static int score;

    public bool lost = false;
    public bool debugGame;

    public float funkDepletionSpeed;

    public Dictionary<int, BaseController> controllers;

    private int uniqueId = 0;
    private BoxCollider2D boundary;

    public GameState gameState;
    public GameObject tutorialOverlay;

    public void Awake()
    {
        if (instance == null)
            instance = this;

        controllers = new Dictionary<int, BaseController>();
        boundary = GetComponentInChildren<BoxCollider2D>();
        gameState = GameState.TutorialScreen;
    }

    public void Start()
    {
        Pool.instance.InitPool();

        if (debugGame)
            InitialiseGame();
    }

    public void InitialiseGame()
    {
        if (tutorialOverlay.activeInHierarchy)
            tutorialOverlay.SetActive(false);

        gameState = GameState.InPlay;
        lost = false;
        funkLevel = 100.0f;
        Funkometer.instance.SetFunkometer(funkLevel);

        MusicManager.instance.StartMusic();

        EnemySpawnManager.instance.SpawnEnemy(true);
        EnemySpawnManager.instance.SpawnEnemy(false);
    }

    public void Update()
    {
        if (gameState == GameState.InPlay)
        {
            if (Effects.myTimeScale == 1)
            {
                for (int i = 0; i < controllers.Count; i++)
                {
                    BaseController c;
                    if (controllers.TryGetValue(i, out c))
                    {
                        if (CanUpdate(c))
                        {
                            c.UpdateController();
                        }
                    }
                }
            }

            funkLevel -= funkDepletionSpeed * Time.deltaTime;
            Funkometer.instance.SetFunkometer(funkLevel);

            if (funkLevel < 0.0f && !lost)
            {
                InGameMenus.instance.SetUIMenu(InGameMenuType.Lose, true);
                MusicManager.instance.SetMusicState(1.0f);
                lost = true;
            }
        }
        
    }

    public void AddController(BaseController controller)
    {
        controllers.Add(uniqueId, controller);
        controller.id = uniqueId;
        uniqueId++;
    }

    public void RemoveController(BaseController controller)
    {
        controllers.Remove(controller.id);
    }

    public Player.PlayerController GetPlayerController()
    {
        if (controllers == null)
            Debug.LogError("Controllers are null!");
        return controllers[0] as Player.PlayerController;
    }

    public void ToggleTweenPause()
    {
        for (int i = 0; i < controllers.Count; i++)
        {
            BaseController c;
            if (controllers.TryGetValue(i, out c))
            {
                if (CanUpdate(c))
                {
                    c.transform.DOTogglePause();
                }
            }
        }
    }

    private bool CanUpdate(BaseController controller)
    {
        if (controller.gameObject.activeInHierarchy && controller != null)
            return true;
        else
        {
            return false;
        }
            
    }

    public void RestartGame()
    {
        InGameMenus.instance.SetUIMenu(InGameMenuType.Lose, false);
        MusicManager.instance.SetMusicState(0.0f);
        GetPlayerController().Restart();
        Pool.instance.Restart();
        InitialiseGame();
    }
}
