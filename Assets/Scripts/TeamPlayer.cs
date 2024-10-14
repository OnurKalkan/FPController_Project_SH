using DG.Tweening;
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
    public bool runToBall;
    GameObject baseball;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        baseball = GameObject.FindWithTag("Baseball");
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

    private void Update()
    {
        if(runToBall && !baseball.GetComponent<Baseball>().catched)
            agent.destination = baseball.transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Baseball"))
        {
            baseball.GetComponent<Baseball>().catched = true;
            runToBall = false;
            baseball.transform.parent = transform;
            baseball.GetComponent<Rigidbody>().isKinematic = true;
            baseball.transform.DOLocalMove(new Vector3(-0.82f, 0.45f, 0.2f), 0.5f);
        }
    }
}
