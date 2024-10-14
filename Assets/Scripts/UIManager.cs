using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    Baseball baseBall;

    private void Awake()
    {
        baseBall = GameObject.FindWithTag("Baseball").GetComponent<Baseball>();
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
