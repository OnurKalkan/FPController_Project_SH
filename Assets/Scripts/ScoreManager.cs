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
    }

    public void IncreaseScore(TeamPlayer.TeamColor tColor)
    {
        if(tColor == TeamPlayer.TeamColor.BlueTeam)
        {
            blueTeamScore++;
            uimanager.ScoreUpdate();
        }
        else if (tColor == TeamPlayer.TeamColor.RedTeam)
        {
            redTeamScore++;
            uimanager.ScoreUpdate();
        }
    }
}
