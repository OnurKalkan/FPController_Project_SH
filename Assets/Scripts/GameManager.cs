using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool roundEnd, resetData;

    private void Start()
    {
        if (resetData) 
        {
            PlayerPrefs.DeleteAll();
        }
    }
}
