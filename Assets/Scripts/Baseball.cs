using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Baseball : MonoBehaviour
{
    public Transform baseballHitPoint;
    public int hitSpeed = 100;
    public bool ballThrowed = false, catched = false, groundTouch = false;
    public Transform stick;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().isKinematic = true;
    }

    public void BallPrep()
    {
        transform.parent.DORotate(new Vector3(-270, 0, -270), 0.75f, RotateMode.FastBeyond360).OnComplete(BallThrow).SetEase(Ease.Linear);
    }

    public void BallThrow()
    {
        ballThrowed = true;
        transform.parent = null;
        Vector3 hitPoint = new Vector3(baseballHitPoint.position.x + Random.Range(-0.5f, 0.5f),
            baseballHitPoint.position.y + Random.Range(0.0f, 1.0f),
            baseballHitPoint.position.z + Random.Range(-0.5f, 0.5f));

        transform.DOMove(hitPoint, 0.5f).SetEase(Ease.Linear);
        Invoke(nameof(BallHit), 0.5f);
    }

    void BallHit()
    {
        stick.transform.DOLocalMove(new Vector3(0.642f, 0.032f, 0.43f), 0.25f).SetEase(Ease.Linear);
        stick.transform.DOLocalRotate(new Vector3(3.219f, -1.54f, -85.196f), 0.25f).SetEase(Ease.Linear).OnComplete(StickDrop);        
        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<Rigidbody>().AddForce(new Vector3(1 + Random.Range(0.5f, 1.5f), 
            1 + Random.Range(0.5f, 1.5f), 
            1 + Random.Range(0.5f, 1.5f)) * hitSpeed);
    }

    void StickDrop()
    {
        stick.transform.parent = null;
        stick.AddComponent<Rigidbody>();
        ShooterToRun();
        HoldersToRun();
    }

    void ShooterToRun()
    {
        GameObject.FindWithTag("Shooter").GetComponent<TeamPlayer>().HomeRun();
    }

    void HoldersToRun()
    {
        GameObject[] holders = GameObject.FindGameObjectsWithTag("Holder");
        for (int i = 0; i < holders.Length; i++)
        {
            holders[i].GetComponent<TeamPlayer>().runToBall = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") && !groundTouch)
        {
            groundTouch = true;
        }
    }
}
