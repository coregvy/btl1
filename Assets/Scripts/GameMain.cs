using UnityEngine.SceneManagement;

public sealed class GameMain
{
    private static GameMain instance = new GameMain();

    SystemConfig _systemConfig;
    GameConfig _gameConfig;
    GameStatus _gameStatus;

    public static GameMain Instance
    {
        get
        {
            return instance;
        }
    }
    private GameMain()
    {
        _gameStatus = new GameStatus();
        _gameStatus.nowScene = Scenes.Title;
        _systemConfig = new SystemConfig();
        _systemConfig.tileScale = 20.0f;
        _systemConfig.tileSize = 0.14f;
        _gameConfig = new GameConfig();
    }


    public void changeScene(Scenes nextScene)
    {
        _gameStatus.nowScene = nextScene;
        SceneManager.LoadScene(nextScene.SceneName());
    }

    public GameConfig gameConfig { get { return _gameConfig; } }
    public SystemConfig systemConfig { get { return _systemConfig; } }
	public GameStatus gameStatus{ get { return _gameStatus; } }
}
