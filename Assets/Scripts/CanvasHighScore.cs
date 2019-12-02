using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CanvasHighScore : MonoBehaviour
{
    public Text[] Scores;
    public Text[] Initials;

    public Button btnOk;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        btnOk.onClick.AddListener(GameManager.Instance.LoadMainMenu);
        SetHighScores();
        RefreshScores();
    }

    void RefreshScores()
    {
        //PlayerPrefs.DeleteAll();
        GameManager.Instance.LoadScores();
        for (int i = 0; i < 10; i++)
        {
            Scores[i].text = (GameManager.Instance.highscores[i] > 0 ? GameManager.Instance.highscores[i].ToString() : "-");
            Initials[i].text = (!string.IsNullOrEmpty(GameManager.Instance.initials[i]) ? GameManager.Instance.initials[i] : "-");
        }
    }

    void SetHighScores()
    {
        PlayerPrefs.SetInt("SCORE0", 530); PlayerPrefs.SetString("INITIALS0", "JWP");
        PlayerPrefs.SetInt("SCORE1", 520); PlayerPrefs.SetString("INITIALS1", "ENP");
        PlayerPrefs.SetInt("SCORE2", 500); PlayerPrefs.SetString("INITIALS2", "SNP");
        PlayerPrefs.SetInt("SCORE3", 490); PlayerPrefs.SetString("INITIALS3", "SJB");
        PlayerPrefs.SetInt("SCORE4", 460); PlayerPrefs.SetString("INITIALS4", "YYZ");
        PlayerPrefs.SetInt("SCORE5", 450); PlayerPrefs.SetString("INITIALS5", "RAT");
        PlayerPrefs.SetInt("SCORE6", 440); PlayerPrefs.SetString("INITIALS6", "POP");
        PlayerPrefs.SetInt("SCORE7", 430); PlayerPrefs.SetString("INITIALS7", "HJS");
        PlayerPrefs.SetInt("SCORE8", 420); PlayerPrefs.SetString("INITIALS8", "LAP");
        PlayerPrefs.SetInt("SCORE9", 400); PlayerPrefs.SetString("INITIALS9", "OJH");
        PlayerPrefs.Save();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            GameManager.Instance.LoadMainMenu();
        }
    }
}
