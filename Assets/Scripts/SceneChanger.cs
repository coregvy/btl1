using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChanger : MonoBehaviour
{

    [SerializeField]
    Scenes nextScene;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ChangeScene()
    {
        Debug.Log("change scene -> " + nextScene.SceneName());
        Debug.Log("counter: " + GameMain.Instance.incrementCounter());
        GameMain.Instance.changeScene(nextScene);
    }
}
