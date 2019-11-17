using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasTutorial : MonoBehaviour
{
    public Button btnStart;

    void Start()
    {
        GameManager.Instance.SetGameState(GameState.TUTORIAL);
        btnStart.onClick.AddListener(GameManager.Instance.StartGame);
    }

    void Update()
    {
        if (Input.GetButtonDown("Start"))
        {
            GameManager.Instance.StartGame();
        }    
    }
}
