using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    [SerializeField] private int playerLives = 3;
    [SerializeField] private TextMeshProUGUI livesText;
    [SerializeField] private float maskTimer;
    [SerializeField] private TextMeshProUGUI timerText;
    private float _maskDuration;
    [SerializeField] private GameObject infoText;
    
    void Awake()
    {
        //singleton
        int numGameSessions = FindObjectsByType<GameSession>(FindObjectsSortMode.None).Length;
        if (numGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    
    void Start()
    {
        infoText.SetActive(false);
        
        _maskDuration = FindFirstObjectByType<LimboManager>().maskDuration;
        int minutes = Mathf.FloorToInt(_maskDuration); // tam sayı kısmı
        int fraction = Mathf.FloorToInt((_maskDuration * 100) % 100); // virgülden sonraki iki hane
        timerText.text = string.Format("{0:00}:{1:00}", minutes, fraction);
        
        livesText.text = playerLives.ToString();
    }

    void FixedUpdate()
    {
       
        maskTimer = FindFirstObjectByType<LimboManager>().GetCurrentMaskTime();
        
        int minutes = Mathf.FloorToInt(maskTimer); // tam sayı kısmı
        int fraction = Mathf.FloorToInt((maskTimer * 100) % 100); // virgülden sonraki iki hane
        timerText.text = string.Format("{0:00}:{1:00}", minutes, fraction);
        
    }
    
    private void ResetGameSession()
    {
        //load the first scene (it might be first level or main menu)
        FindFirstObjectByType<ScenePersist>().ResetScene();
        SceneManager.LoadScene(1);
        Destroy(gameObject); //we need to reset all the process
    }
    
    public void ProcessPlayerDeath()
    {
        if (playerLives > 1)
        {
            TakeLife();
        }
        else
        {
            ResetGameSession();
        }
    }
    
    private void TakeLife()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            //coroutine 5 sn görünsün textmesh startta disable burda enable
            StartCoroutine(ShowInformation());
            return;
        }
        playerLives--;
        int activeScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(activeScene);
        livesText.text = playerLives.ToString();
    }
    
    IEnumerator ShowInformation()
    {
        infoText.SetActive(true);
        yield return new WaitForSeconds(10f);
        infoText.SetActive(false);
        SceneManager.LoadScene(1);
    }
    
}
