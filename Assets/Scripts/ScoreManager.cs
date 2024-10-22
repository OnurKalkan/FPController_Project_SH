using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int blueTeamScore = 0, redTeamScore = 0, roundNo = 1;
    TeamPlayer.TeamColor teamColor;
    UIManager uimanager;

    private void Awake()
    {
        uimanager = GetComponent<UIManager>();
        roundNo = PlayerPrefs.GetInt("RoundNo", 0);
        blueTeamScore = PlayerPrefs.GetInt("BlueTeamScore", 0);
        redTeamScore = PlayerPrefs.GetInt("RedTeamScore", 0);
    }

    public void IncreaseScore(TeamPlayer.TeamColor tColor)
    {
        roundNo++;
        PlayerPrefs.SetInt("RoundNo", roundNo);
        if (tColor == TeamPlayer.TeamColor.BlueTeam)
        {
            blueTeamScore++;
            PlayerPrefs.SetInt("BlueTeamScore", blueTeamScore);
            uimanager.ScoreUpdate();
        }
        else if (tColor == TeamPlayer.TeamColor.RedTeam)
        {
            redTeamScore++;
            PlayerPrefs.SetInt("RedTeamScore", redTeamScore);
            uimanager.ScoreUpdate();
        }
    }
}
