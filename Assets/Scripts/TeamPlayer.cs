using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.WSA;

public class TeamPlayer : MonoBehaviour
{
    NavMeshAgent agent;
    public TeamColor teamColor;
    public FormationType formationType;    
    public PlayerType playerType;
    public bool runToBall;
    GameObject baseball;
    ScoreManager scoreManager;
    UIManager uiManager;
    public Transform[] bases;//shooterlar icin
    public int baseCounter = 0, baseThCounter = 1;
    public GameObject firstThrower = null;

    private void Awake()
    {
        scoreManager = GameObject.Find("GameManager").GetComponent<ScoreManager>();
        uiManager = GameObject.Find("GameManager").GetComponent<UIManager>();
    }

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
        if(playerType == PlayerType.Holder)
        {
            if (runToBall && !baseball.GetComponent<Baseball>().catched)
                agent.destination = baseball.transform.position;
            else if (!runToBall && baseball.GetComponent<Baseball>().catched)
                agent.speed = 0;
        }        
    }

    void BaseballFree()
    {
        baseball.transform.parent = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Baseball"))
        {
            if(playerType == PlayerType.BaseThrowers)
            {
                if (GetComponent<BaseThrower>().throwerNo == 1)
                {
                    GameObject[] throwers = GameObject.FindGameObjectsWithTag("BaseTh");
                    for (int i = 0; i < throwers.Length; i++)
                    {
                        if (throwers[i].GetComponent<BaseThrower>().throwerNo == baseThCounter)
                            firstThrower = throwers[i];
                    }
                    baseball.transform.DOMove(firstThrower.transform.position, 2).SetDelay(0.5f);
                }
                else if (GetComponent<BaseThrower>().throwerNo < 4 && GetComponent<BaseThrower>().throwerNo > 1)
                {
                    GameObject[] throwers = GameObject.FindGameObjectsWithTag("BaseTh");
                    for (int i = 0; i < throwers.Length; i++)
                    {
                        throwers[i].GetComponent<TeamPlayer>().baseThCounter++;
                    }
                    for (int i = 0; i < throwers.Length; i++)
                    {
                        if (throwers[i].GetComponent<BaseThrower>().throwerNo == baseThCounter)
                            firstThrower = throwers[i];                        
                    }
                    baseball.transform.DOMove(firstThrower.transform.position, 2).SetDelay(0.5f);
                }
                else if (GetComponent<BaseThrower>().throwerNo == 4)
                {
                    //final kontrolu
                }
            }            
            else if (playerType == PlayerType.Holder && !other.GetComponent<Baseball>().catched)
            {
                baseball.GetComponent<Rigidbody>().isKinematic = true;
                baseball.transform.DOLocalMove(new Vector3(-0.82f, 0.45f, 0.2f), 0.5f).OnComplete(BaseballFree);
                baseball.transform.parent = transform;
                if (other.GetComponent<Baseball>().groundTouch && !other.GetComponent<Baseball>().catched)
                {
                    other.GetComponent<Baseball>().catched = true;                    
                    //oyun devam edecek, birinci kaleye atacak topu
                    GameObject[] throwers = GameObject.FindGameObjectsWithTag("BaseTh");                    
                    for (int i = 0; i < throwers.Length; i++)
                    {
                        if (throwers[i].GetComponent<BaseThrower>().throwerNo == baseThCounter)
                            firstThrower = throwers[i];
                        throwers[i].GetComponent<TeamPlayer>().baseThCounter++;
                    }                    
                    baseball.transform.DOMove(firstThrower.transform.position, 2).SetDelay(0.6f);
                }
                else if (!other.GetComponent<Baseball>().groundTouch && !other.GetComponent<Baseball>().catched)
                {
                    other.GetComponent<Baseball>().catched = true;
                    scoreManager.IncreaseScore(teamColor);
                    Invoke(nameof(OpenEndMenu), 1);
                }                
                GameObject[] holders = GameObject.FindGameObjectsWithTag("Holder");
                for (int i = 0; i < holders.Length; i++)
                {
                    holders[i].GetComponent<TeamPlayer>().runToBall = false;
                }
            }            
        }
        if (other.CompareTag("Base"))//shooter icin homerun
        {
            if(baseCounter == 0 && other.GetComponent<Base>().baseNo != 4)
            {
                agent.destination = bases[baseCounter].position;
                baseCounter++;
            }            
            else if(baseCounter < 4 && other.GetComponent<Base>().baseNo != 4)
            {
                agent.destination = bases[baseCounter].position;
                baseCounter++;
            }
            else if (baseCounter == 4 && other.GetComponent<Base>().baseNo == 4)
            {
                agent.speed = 0;
                //final hesaplamasi
            }
        }
    }

    void OpenEndMenu()
    {
        uiManager.EndMenu();
    }

    public void HomeRun()
    {
        agent.destination = bases[baseCounter].position;
        baseCounter++;
    }
}
