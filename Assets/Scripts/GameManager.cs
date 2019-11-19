using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState { NULLSTATE, MAIN, OPTIONS, TUTORIAL, GAME }

public delegate void OnStateChangeHandler();

public class GameManager : MonoBehaviour
{
    static GameManager _instance = null;
    public GameState gameState { get; private set; }

    public int playerScore = 0;
    public float speedBoostFuel = 3.0f;
    public float speedBoostFuelMax = 3.0f;

    public float gameTime = 60;     // maximum time per game in seconds
    public float timeLeft = 0;      // current time left 

    public float startCountDown { get; private set; }
    public bool isGameActive { get; private set; }
    public bool isGameOver { get; private set; }

    public int[] highscores;
    public string[] initials;


    // TODO: Make player a prefab
    // public GameObject playerPrefab;
    // public GameObject player1 { get; set; }

    // player preferences
    private const string MUSIC_VOLUME = "MusicVolume";
    private const string SOUND_VOLUME = "SoundVolume";

    public event OnStateChangeHandler OnStateChange;

    public void SetGameState(GameState state)
    {
        gameState = state;
        if (OnStateChange != null)
            OnStateChange();
    }


    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject();
                _instance = go.AddComponent<GameManager>();
                // TODO: Instantiate player prefab
                //_instance.playerPrefab = Resources.Load("Truck", typeof(GameObject)) as GameObject;
                go.name = "_GameManager";
                DontDestroyOnLoad(_instance);
            }

            return _instance;
        }
    }

    public void LoadScores()
    {
        highscores = new int[10];
        initials = new string[10];

        for (int i = 0; i < 10; i++)
        {
            // scores
            if (PlayerPrefs.HasKey("SCORE" + i))
            {
                highscores[i] = PlayerPrefs.GetInt("SCORE" + i);
            }
            else
            {
                highscores[i] = 0;
                PlayerPrefs.SetInt("SCORE" + i, highscores[i]);
                PlayerPrefs.Save();
            }

            // initials
            if (PlayerPrefs.HasKey("INITIALS" + i))
            {
                initials[i] = PlayerPrefs.GetString("INITIALS" + i);
            }
            else
            {
                initials[i] = "";
                PlayerPrefs.SetString("INITIALS" + i, initials[i]);
                PlayerPrefs.Save();
            }
        }
    }

    public void LoadPlayerPrefs()
    {
        // music volume
        if (PlayerPrefs.HasKey(MUSIC_VOLUME))
        {
            SoundManager.Instance.musicSource.volume = PlayerPrefs.GetFloat(MUSIC_VOLUME);
        }
        else
        {
            PlayerPrefs.SetFloat(MUSIC_VOLUME, SoundManager.Instance.musicSource.volume);
            PlayerPrefs.Save();
        }

        // sound volume
        if (PlayerPrefs.HasKey(SOUND_VOLUME))
        {
            SoundManager.Instance.sfxSource.volume = PlayerPrefs.GetFloat(SOUND_VOLUME);
        }
        else
        {
            PlayerPrefs.SetFloat(SOUND_VOLUME, SoundManager.Instance.sfxSource.volume);
            PlayerPrefs.Save();
        }
    }

    public void SavePlayerPrefs()
    {
        PlayerPrefs.SetFloat(MUSIC_VOLUME, SoundManager.Instance.musicSource.volume);
        PlayerPrefs.SetFloat(SOUND_VOLUME, SoundManager.Instance.sfxSource.volume);
        PlayerPrefs.Save();
    }

    public void StartGame()
    {
        playerScore = 0;
        speedBoostFuel = speedBoostFuelMax;
        timeLeft = gameTime;
        isGameActive = false;
        isGameOver = false;
        startCountDown = 10.0f;
        LoadLevel("Game");
    }

    public void ShowTutorial()
    {
        LoadLevel("Tutorial");
    }

    public void ShowHighScores()
    {
        LoadLevel("Scores");
    }


    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void LoadMainMenu()
    {
        LoadLevel("Main");
    }

    public void LoadLevel(string levelName)
    {
        switch (levelName)
        {
            case "Main":
                SetGameState(GameState.MAIN);
                break;
            case "Game":
                SetGameState(GameState.GAME);
                break;
            case "Options":
                SetGameState(GameState.OPTIONS);
                break;
            case "Tutorial":
                SetGameState(GameState.TUTORIAL);
                break;
            default:
                SetGameState(GameState.NULLSTATE);
                break;
        }

        SceneManager.LoadScene(levelName);
    }

    //TODO: Implement player spawning
    public void SpawnPlayer(Transform spawnLocation)
    {
        //    if (playerPrefab && spawnLocation)
        //    {
        //        player1 = Instantiate(playerPrefab, spawnLocation.position, spawnLocation.rotation);
        //    }
    }

    public void GameOver()
    {
        isGameActive = false;
        isGameOver = true;
        Invoke("ShowHighScores", 5);
    }

    public void UpdateScore(int amount)
    {
        playerScore += amount;
        //trashCollected++;
    }

    private void Update()
    {
        // handle game play
        if (gameState == GameState.GAME && !isGameOver)
        {
            GamePlay();
        }
    }

    private void GamePlay()
    {
        if (isGameActive)
        {
            if (timeLeft > 0f)
            {
                timeLeft -= Time.deltaTime;
            }
            else
            {
                timeLeft = 0.0f;
                GameOver();
            }
        }
        else
        {
            // starting count down
            startCountDown -= Time.deltaTime;
            if (startCountDown < 0)
            {
                startCountDown = 0;
                isGameActive = true;
            }
        }
    }
}


