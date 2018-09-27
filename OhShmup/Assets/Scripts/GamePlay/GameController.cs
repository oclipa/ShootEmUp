using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    // wolf collider dimensions
    float wolfHeight;
    float wolfWidth;

    // wolf timer
    Timer wolfTimer;

    List<GameObject> wolves = new List<GameObject>();

    private bool isActive;

    private void Awake()
    {
        EventManager.Reset();
    }

    // Use this for initialization
    void Start () {

    }

    private void OnLevelWasLoaded(int level)
    {
        if (level == 1)
        {
            isActive = true;

            ScreenUtils.Initialize();
            Spawner.Initialize();

            Wolf tempWolf = Object.Instantiate<Wolf>(Resources.Load<Wolf>("Wolf"));
            BoxCollider2D boxCollider = tempWolf.GetComponent<BoxCollider2D>();
            wolfHeight = boxCollider.size.y;
            wolfWidth = boxCollider.size.x;
            Destroy(tempWolf.gameObject);

            EventManager.AddGameOverListener(GameOver);

            wolfTimer = gameObject.AddComponent<Timer>();
            wolfTimer.Duration = GameConstants.InitialWolfSpawnDuration;
            wolfTimer.Run();

        }
    }

    // Update is called once per frame
    void Update () 
    {
        if (isActive && wolfTimer.Finished)
        {
            float startingY = Random.Range(ScreenUtils.ScreenBottom + wolfHeight, ScreenUtils.ScreenTop - wolfHeight);
            Vector2 startingPosition = new Vector2(ScreenUtils.ScreenRight + wolfWidth, startingY);

            GameObject wolf = Spawner.SpawnWolf(startingPosition);
            if(wolf != null)
            {
                wolves.Add(wolf);
                wolf.SetActive(true);
                wolf.GetComponent<Wolf>().StartMoving();
            }
            wolfTimer.Duration = wolfTimer.Duration - GameConstants.WolfSpawnSpeedup;
            wolfTimer.Run();
        }
    }

    void GameOver()
    {
        AudioManager.Play(AudioClipName.GameOver);
        isActive = false;

        foreach (GameObject wolf in wolves)
            Spawner.ReturnWolf(wolf);

        wolves.Clear();

        GameState.IsWon = false;

        EventManager.Reset();

        SceneManager.LoadScene("GameOver", LoadSceneMode.Single);
    }
}
