using UnityEngine;

/// <summary>
/// イベントにつける用
/// </summary>
public class SceneChanger : MonoBehaviour
{
    /// <summary>
    /// set by GameObject
    /// </summary>
    [SerializeField]
    Scenes nextScene = Scenes.Title;

    public void ChangeScene()
    {
        Debug.Log("change scene -> " + nextScene.SceneName());
        GameMain.Instance.changeScene(nextScene);
    }
}
