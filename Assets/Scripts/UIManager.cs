using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    Baseball baseBall;
    public GameObject inGameMenu, endGameMenu, matchResult;
    public TextMeshProUGUI redTeamText, blueTeamText, roundNoText, matchDayInfoText, gameSpeedText;
    public TextMeshPro redTeamEText, blueTeamEText;
    ScoreManager scoreManager;
    GameManager gameManager;
    public int gameSpeed = 1;
    public Material redMat, blueMat;

    private void Awake()
    {
        baseBall = GameObject.FindWithTag("Baseball").GetComponent<Baseball>();
        scoreManager = GetComponent<ScoreManager>();
        gameManager = GetComponent<GameManager>();
    }

    private void Start()
    {
        StartMenu();
        MatchDayInfo();
        ScoreUpdate();
        Time.timeScale = gameSpeed;
        if (scoreManager.roundNo > 2)
        {
            GameObject shooter = GameObject.FindWithTag("Shooter");
            shooter.GetComponent<Renderer>().material = redMat;
            shooter.GetComponent<TeamPlayer>().teamColor = TeamPlayer.TeamColor.RedTeam;
            GameObject thrower = GameObject.FindWithTag("Thrower");
            thrower.GetComponent<Renderer>().material = blueMat;
            thrower.GetComponent<TeamPlayer>().teamColor = TeamPlayer.TeamColor.BlueTeam;
            GameObject[] baseThrowers = GameObject.FindGameObjectsWithTag("BaseTh");
            foreach (GameObject bt in baseThrowers)
            {
                bt.GetComponent<Renderer>().material = blueMat;
                bt.GetComponent<TeamPlayer>().teamColor = TeamPlayer.TeamColor.BlueTeam;
            }
            GameObject[] holders = GameObject.FindGameObjectsWithTag("Holder");
            foreach (GameObject holder in holders)
            {
                holder.GetComponent<Renderer>().material = blueMat;
                holder.GetComponent<TeamPlayer>().teamColor = TeamPlayer.TeamColor.BlueTeam;
            }
        }
    }

    public void StartMenu()
    {
        inGameMenu.SetActive(true);
        endGameMenu.SetActive(false);
        matchResult.SetActive(false);
    }

    public void GameSpeed()
    {
        if(gameSpeed == 1)
        {
            gameSpeed++;
            Time.timeScale = gameSpeed;
            gameSpeedText.text = "SPEED X" + gameSpeed;
        }
        else if (gameSpeed == 2)
        {
            gameSpeed++;
            Time.timeScale = gameSpeed;
            gameSpeedText.text = "SPEED X" + gameSpeed;
        }
        else
        {
            gameSpeed = 1;
            Time.timeScale = gameSpeed;
            gameSpeedText.text = "SPEED X" + gameSpeed;
        }
    }

    public void EndMenu()
    {
        inGameMenu.SetActive(false);
        endGameMenu.SetActive(true);
        MatchDayInfo();
        if(scoreManager.roundNo == 6)
        {
            matchResult.SetActive(true);
        }            
        else
            Invoke(nameof(ResetTheGame), 4);
    }

    public void PlayAgain()
    {
        PlayerPrefs.DeleteAll();
        Invoke(nameof(ResetTheGame), 4);
    }

    public void ExitTheGame()
    {
        PlayerPrefs.DeleteAll();
        Application.Quit();
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
