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
        //currenti al eğer 3se entrancea at 
        yield return new WaitForSecondsRealtime(1f);
        
        //if it's the last exit
        int currenstSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (currenstSceneIndex == 3)
        {
            SceneManager.LoadScene(0);
        }
        
        int nextSceneIndex = currenstSceneIndex + 1;
        
        if (nextSceneIndex != SceneManager.sceneCountInBuildSettings)
        {
            FindFirstObjectByType<ScenePersist>().ResetScene();
            SceneManager.LoadScene(nextSceneIndex);
        } 
            
    }
}
