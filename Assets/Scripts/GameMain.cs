using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public sealed class GameMain
{
    private static GameMain instance = new GameMain();

	public Scenes nowScene{ get; set; }
    SystemConfig _systemConfig;
    GameConfig _gameConfig;

    public static GameMain Instance
    {
        get
        {
            return instance;
        }
    }
    private GameMain()
    {
		nowScene = Scenes.Title;
        _systemConfig = new SystemConfig();
        _systemConfig.tileScale = 20.0f;
        _systemConfig.tileSize = 0.16f;
        _gameConfig = new GameConfig();
    }


    public void changeScene(Scenes nextScene)
    {
        nowScene = nextScene;
        SceneManager.LoadScene(nextScene.SceneName());
    }

    // test
    int count = 0;
    public int incrementCounter()
    {
        return count++;
    }

    public GameConfig gameConfig { get { return _gameConfig; } }
    public SystemConfig systemConfig { get { return _systemConfig; } }
}
