using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Baseball : MonoBehaviour
{
    public Transform baseballHitPoint;
    public int hitSpeed = 100;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().isKinematic = true;
        BallThrow();
    }

    void BallThrow()
    {
        Vector3 hitPoint = new Vector3(baseballHitPoint.position.x + Random.Range(-0.5f, 0.5f),
            baseballHitPoint.position.y + Random.Range(-0.5f, 0.5f),
            baseballHitPoint.position.z + Random.Range(-0.5f, 0.5f));

        transform.DOMove(hitPoint, 1);
        Invoke(nameof(BallHit), 1);
    }

    void BallHit()
    {
        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<Rigidbody>().AddForce(new Vector3(1 + Random.Range(0.5f, 1.5f), 
            1 + Random.Range(0.5f, 1.5f), 
            1 + Random.Range(0.5f, 1.5f)) * hitSpeed);
    }
}
