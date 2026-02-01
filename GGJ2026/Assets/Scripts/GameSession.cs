using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    [SerializeField] private int playerLives = 3;
    [SerializeField] private TextMeshProUGUI livesText;
    
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
        livesText.text = playerLives.ToString();
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
            SceneManager.LoadScene(1);
            return;
        }
        playerLives--;
        int activeScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(activeScene);
        livesText.text = playerLives.ToString();
    }
    
}
