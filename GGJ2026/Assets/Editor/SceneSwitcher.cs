using UnityEditor;
using UnityEditor.SceneManagement;

public static class SceneSwitcher
{
    [MenuItem("Scenes/Sample Scene")]
    public static void OpenSampleScene()
    {
        EditorSceneManager.OpenScene("Assets/Scenes/SampleScene.unity");
    }
    
    [MenuItem("Scenes/Parallax Scene")]
    public static void OpenParallaxScene()
    {
        EditorSceneManager.OpenScene("Assets/Scenes/ParallaxScene.unity");
    }
    
    [MenuItem("Scenes/Entrance Scene")]
    public static void OpenEntranceScene()
    {
        EditorSceneManager.OpenScene("Assets/Scenes/EntranceScene.unity");
    }
    
    /*
    [MenuItem("Scenes/Main Menu")]
    public static void OpenMainMenu()
    {
        EditorSceneManager.OpenScene("Assets/Scenes/MainMenu.unity");
    }

    [MenuItem("Scenes/Levels/Level 1")]
    public static void OpenLevel1()
    {
        EditorSceneManager.OpenScene("Assets/Scenes/Level1.unity");
    }
    */
}