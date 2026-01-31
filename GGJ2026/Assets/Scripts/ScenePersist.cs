using UnityEngine;

public class ScenePersist : MonoBehaviour
{
    void Awake()
    {
        //singleton
        int numScenePersists = FindObjectsByType<ScenePersist>(FindObjectsSortMode.None).Length;
        if (numScenePersists > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    public void ResetScene()
    {
        Destroy(gameObject);
    }
}
