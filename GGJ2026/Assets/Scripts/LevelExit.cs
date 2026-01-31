using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    bool _levelExited = false;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !_levelExited)
        {
            _levelExited = true;
            StartCoroutine(LoadNextLevel());

        }
    }

    IEnumerator LoadNextLevel()
    {
        yield return new WaitForSecondsRealtime(1f);
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        
        
        if (nextSceneIndex != SceneManager.sceneCountInBuildSettings)
        {
            FindFirstObjectByType<ScenePersist>().ResetScene();
            SceneManager.LoadScene(nextSceneIndex);
        } 
            
    }
}
