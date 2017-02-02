using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public sealed class GameMain {
	private static GameMain instance = new GameMain();

	public static GameMain Instance {
		get {
			return instance;
		}
	}
	private GameMain(){
	}

    Scenes nowScene = Scenes.Title;

	public void changeScene(Scenes nextScene) {
		nowScene = nextScene;
		SceneManager.LoadScene (nextScene.SceneName());
	}
	int count = 0;
	public int incrementCounter() {
		return count++;
	}

	public GameConfig getConfig(){
		return null;
	}
}
