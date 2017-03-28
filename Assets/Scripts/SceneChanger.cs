using UnityEngine;

/// <summary>
/// イベントにつける用
/// </summary>
public class SceneChanger : MonoBehaviour
{
    /// <summary>
    /// GameObject側で設定する
    /// </summary>
    [SerializeField]
    Scenes nextScene;
    public void ChangeScene()
    {
        Debug.Log("change scene -> " + nextScene.SceneName());
        GameMain.Instance.changeScene(nextScene);
    }
}
