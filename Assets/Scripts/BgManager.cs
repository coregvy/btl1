using System.Collections.Generic;
using UnityEngine;

public class BgManager : MonoBehaviour
{
    BgTile[][] baseTiles;
    [SerializeField]
    Rect movableField = new Rect(-23, 4, 16, 20);
    static GameMain gameMain = GameMain.Instance;
    //static Vector2 tileBasePose = new Vector2(-6, 6);
    List<CharacterManager> characters;

    // Use this for initialization
    void Start()
    {
        Sprite[] tiles = Resources.LoadAll<Sprite>("baseTiles");
        // var basePose = new Vector2(0, 0);
        const int tileX = 11;
        const int tileY = 10;
        // transform.position = new Vector3(basePose.x + tileSize * tileScale * 2.0f, basePose.y + tileSize * tileScale * 1.5f);
        int[][] tileMap = {
            new int[]{ 136, 136, 113,136, 49,136,136,136,136,136, 136 },
            new int[]{ 136,   0,   1,  3,  2,  3,  3,  4,  1,  5, 136 },
            new int[]{ 136,  22,  25, 88, 89, 88, 89, 88, 89, 90, 136 },
            new int[]{   6,  22,  26, 89, 88, 89, 88, 89, 88, 68, 136 },
            new int[]{ 136,  22,  25, 88, 89, 88, 89, 88, 89, 90, 136 },
            new int[]{ 136,  22,  26, 89, 88, 89, 88, 89, 88, 68, 136 },
            new int[]{ 136,  22,  25, 88, 89, 88, 89, 88, 89, 90, 136 },
            new int[]{ 136,  22,  26, 89, 88, 89, 88, 89, 88, 68, 136 },
            new int[]{ 136, 107, 108,109,108,109,108,109,111,112, 136 },
            new int[]{ 136, 136,   6,136,136,136,136, 91,136,136, 136 }
        };
        baseTiles = new BgTile[tileY][];
        var parent = new GameObject("TileBase");
        parent.transform.parent = transform;

        // baseTiles = new GameObject[tileY][tileX];
        for (int y = 0; y < tileY; ++y)
        {
            baseTiles[y] = new BgTile[tileX];
            for (int x = 0; x < tileX; ++x)
            {
                baseTiles[y][x] = BgTile.createTile(x, y, tiles[tileMap[y][x]], this, parent.transform);
            }
        }

        transform.position = new Vector3(-14, 6, 0);
        Debug.Log($"movableField x: {movableField.xMin} - {movableField.xMax}");

        characters = new List<CharacterManager>();
		characters.Add(CharacterManager.createChar("char1", "player1", 3, 3, this));
		characters.Add(CharacterManager.createChar("char2", "player1", 4, 4, this));
        var c3 = CharacterManager.createChar ("char3", "player2", 5, 5, this);
        c3.getCharacterInfo().type = CharacterType.Enemy;
        characters.Add(c3);
    }

    void Update()
    {

    }

    public CharacterManager onTileCharacter(BgTile tile)
    {
        var tilePos = tile.transform.position;
        foreach (var character in characters)
        {
			var cs = character.GetComponent<CharacterManager>().getCharacterInfo();
            if (tile.posX == cs.posX && tile.posY == cs.posY)
                return character;
        }
        return null;
    }

    public static float getWorldPositionX(int x)
    {
        return gameMain.systemConfig.tileSize * gameMain.systemConfig.tileScale * x;
    }
    public static float getWorldPositionY(int y)
    {
        return -gameMain.systemConfig.tileSize * gameMain.systemConfig.tileScale * y;
    }
    public static Vector3 getWorldPosition(int x, int y, float z)
    {
        return new Vector3(getWorldPositionX(x), getWorldPositionY(y), z);
    }

    GameObject statusWindow;
	public GameObject createStatusWindow(CharacterInfo status)
    {
        if (statusWindow != null)
        {
            deleteStatusWindow();
        }
        var statusWindowPrf = (GameObject)Resources.Load("Prefabs/statusWindow");
        statusWindow = Instantiate(statusWindowPrf, GameObject.Find("UI").transform) as GameObject;
        var swman = statusWindow.GetComponent<StatusWindowManager>();
        swman.updateText(status);
        return statusWindow;
    }
    public void deleteStatusWindow()
    {
        if (statusWindow != null)
        {
            Debug.Log("delete status window.");
            Destroy(statusWindow);
            statusWindow = null;
        }
    }

    Object controllWindow;
	public Object createControllWindow(CharacterInfo status)
    {
        if (controllWindow != null)
        {
            return controllWindow;
        }
        var prefab = Resources.Load("Prefabs/controllWindow");
        controllWindow = Instantiate(prefab, GameObject.Find("UI").transform);
        var cwm = ((GameObject)controllWindow).GetComponent<ControllWindowManager>();
        cwm.setBgManager(this);
		cwm.setCharacterInfo(status);
        return controllWindow;
    }
    public void deleteControllWindow()
    {
        if (controllWindow != null)
        {
            Debug.Log("delete controll window.");
            Destroy(controllWindow);
            controllWindow = null;
        }
    }

    public void moveWorld(Vector3 pos)
    {
        //Debug.Log ($"new pos: {pos}");
        if (movableField.Contains(pos))
        {
            transform.position = pos;
        }
    }
    public void updateTileColor(int centerX, int centerY, int dist, Color newColor)
    {
        if (gameMain.gameStatus.ctrlStatus != ControllStatus.Free)
            return;
        getTile(centerX, centerY).setMaskColor(newColor);
        for (int dx = -dist; dx <= dist; ++dx)
        {
            int distY = dist - Mathf.Abs(dx);
            for (int dy = -distY; dy <= distY; ++dy)
            {
                getTile(centerY + dx, centerX + dy).setMaskColor(newColor);
            }
        }
    }

	public void updateTileColor(CharacterInfo cs, Color newColor)
    {
        updateTileColor(cs.posX, cs.posY, cs.attackRange, newColor);
    }

    public BgTile getTile(int x, int y)
    {
        return baseTiles[y][x];
    }
}
