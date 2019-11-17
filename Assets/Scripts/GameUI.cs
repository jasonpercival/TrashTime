using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameUI : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI collectedText;
    public TextMeshProUGUI timeText;
    public Text infoText;

    public AudioClip goIndicator;

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;                
    }

    // Update is called once per frame
    void Update()
    {
        collectedText.text = GameManager.Instance.trashCollected.ToString();
        scoreText.text = GameManager.Instance.playerScore.ToString();

        if (GameManager.Instance.isGameOver)
        {
            infoText.text = "TIMES UP! GAME OVER";
            infoText.gameObject.SetActive(true);
        }
        else
        {
            // show time left if game is started
            if (GameManager.Instance.isGameActive)
            {
                int secondsLeft = (int)GameManager.Instance.timeLeft;
                int min = secondsLeft / 60;
                int sec = secondsLeft - (min * 60);

                timeText.text = string.Format("{0:D2}:{1:D2}", min, sec);
                infoText.gameObject.SetActive(false);

            }
            else
            {
                // show count down to before game start
                int countDown = (int)GameManager.Instance.startCountDown;
                timeText.text = string.Format("STARTING IN {0:D1}", countDown);
                infoText.gameObject.SetActive(true);

                if ((int)countDown == 4)
                {
                    SoundManager.Instance.PlaySound(goIndicator);
                }
            }
        }

    }

    public void ShowGoIndicator()
    {
        //goAnimator.SetTrigger("Indicator");
        StartCoroutine(GoIndicator());
    }

    // Make the player sprite flash temporarily with a given color
    IEnumerator GoIndicator()
    {
        for (int i = 0; i < 3; i++)
        {
            SoundManager.Instance.PlaySound(goIndicator);
            yield return new WaitForSeconds(goIndicator.length);
        }
    }
}
