using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    Baseball baseBall;
    public GameObject inGameMenu, endGameMenu;
    public TextMeshProUGUI redTeamText, blueTeamText, roundNoText, matchDayInfoText;
    public TextMeshPro redTeamEText, blueTeamEText;
    ScoreManager scoreManager;
    GamaManager gameManager;

    private void Awake()
    {
        baseBall = GameObject.FindWithTag("Baseball").GetComponent<Baseball>();
        scoreManager = GetComponent<ScoreManager>();
        gameManager = GetComponent<GamaManager>();
    }

    private void Start()
    {
        StartMenu();
        MatchDayInfo();
        ScoreUpdate();
    }

    public void StartMenu()
    {
        inGameMenu.SetActive(true);
        endGameMenu.SetActive(false);
    }

    public void EndMenu()
    {
        inGameMenu.SetActive(false);
        endGameMenu.SetActive(true);
    }

    public void ScoreUpdate()
    {
        redTeamEText.text = "RED TEAM\n" + scoreManager.redTeamScore.ToString();
        redTeamText.text = "RED TEAM\n" + scoreManager.redTeamScore.ToString();
        blueTeamEText.text = "BLUE TEAM\n" + scoreManager.blueTeamScore.ToString();
        blueTeamText.text = "BLUE TEAM\n" + scoreManager.blueTeamScore.ToString();
    }

    void MatchDayInfo()
    {
        //System.DateTime.Now.DayOfWeek.ToString();
        matchDayInfoText.text = System.DateTime.Now.DayOfWeek.ToString() + ", " + System.DateTime.Now.Day.ToString() + "." + System.DateTime.Now.Month.ToString()
            + "\nIstanbul";
        roundNoText.text = "ROUND\n" + scoreManager.roundNo.ToString();
    }

    public void StartContinue()
    {
        if (!baseBall.ballThrowed)
            baseBall.BallPrep();
        else
            Time.timeScale = 1.0f;  
    }

    public void PauseTheGame()
    {
        Time.timeScale = 0;
    }

    public void ResetTheGame()
    {
        SceneManager.LoadScene(0);
    }
}
