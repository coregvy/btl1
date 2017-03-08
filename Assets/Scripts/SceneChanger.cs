using UnityEngine;

public class SceneChanger : MonoBehaviour
{
    [SerializeField]
    Scenes nextScene;
    public void ChangeScene()
    {
        Debug.Log("change scene -> " + nextScene.SceneName());
        GameMain.Instance.changeScene(nextScene);
    }
}
