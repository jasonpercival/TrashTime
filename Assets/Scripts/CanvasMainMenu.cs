using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasMainMenu : MonoBehaviour
{
    public Button btnStart;
    public Button btnOptions;
    public Button btnTutorial;
    public Button btnQuit;
    public Button btnScores;

    public GameObject optionsMenu;
    public AudioClip titleMusic;

    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        btnStart.onClick.AddListener(GameManager.Instance.ShowTutorial);
        btnOptions.onClick.AddListener(ShowOptions);
        //btnTutorial.onClick.AddListener(GameManager.Instance.ShowTutorial);
        btnQuit.onClick.AddListener(GameManager.Instance.QuitGame);
        btnScores.onClick.AddListener(GameManager.Instance.ShowHighScores);

        GameManager.Instance.SetGameState(GameState.MAIN);

        if (titleMusic)
            SoundManager.Instance.PlayMusic(titleMusic);
    }

    void Update()
    {
        if (Input.GetButtonDown("Start"))
        {
            GameManager.Instance.ShowTutorial();
        }    
    }

    void ShowOptions()
    {
        if (optionsMenu)
        {
            Instantiate(optionsMenu);
        }
    }
}
