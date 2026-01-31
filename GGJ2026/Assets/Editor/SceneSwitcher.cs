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
    
    [MenuItem("Scenes/Level 1 Scene")]
    public static void OpenLevel1Scene()
    {
        EditorSceneManager.OpenScene("Assets/Scenes/Level1Scene.unity");
    }
    
    [MenuItem("Scenes/Level 2  Scene")]
    public static void OpenLevel2Scene()
    {
        EditorSceneManager.OpenScene("Assets/Scenes/Level2Scene.unity");
    }
        
    [MenuItem("Scenes/Level 3 Scene")]
    public static void OpenLevel3Scene()
    {
        EditorSceneManager.OpenScene("Assets/Scenes/Level3Scene.unity");
    }
    
}