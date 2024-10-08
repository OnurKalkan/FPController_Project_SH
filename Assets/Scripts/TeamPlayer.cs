using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TeamPlayer : MonoBehaviour
{
    NavMeshAgent agent;
    public TeamColor teamColor;
    public FormationType formationType;    
    public PlayerType playerType;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public enum FormationType
    {
        Attackers,
        Defenders
    }

    public enum TeamColor
    {
        BlueTeam,
        RedTeam
    }

    public enum PlayerType
    {
        Thrower,
        Shooter,
        Holder,
        BaseThrowers
    }
}
