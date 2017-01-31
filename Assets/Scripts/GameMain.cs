using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMain : SingletonMonoBehaviourFast<GameMain> {
    Scenes nowScene = Scenes.Title;
    [SerializeField]
    GameConfig config;
	// static GameMain singleton = null;

	// Use this for initialization
	void Start () {
		config = new GameConfig ();
		DontDestroyOnLoad (this);
	}

	// Update is called once per frame
	void Update () {
		
	}

	void changeScene(Scenes nextScene) {
		nowScene = nextScene;
		SceneManager.LoadScene (nextScene.SceneName());
	}
}
