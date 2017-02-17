using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgManager : MonoBehaviour
{
    [SerializeField]
    GameObject playerSprite;

    GameObject[][] baseTiles;

    static GameMain gameMain = GameMain.Instance;
    static Vector2 tileBasePose = new Vector2(-6, 6);
    List<GameObject> characters;

    // Use this for initialization
    void Start()
    {
        Debug.Log("start: " + playerSprite.GetType());
        Sprite[] tiles = Resources.LoadAll<Sprite>("baseTiles");
        // var basePose = new Vector2(0, 0);
        const int tileX = 11;
        const int tileY = 10;
        // transform.position = new Vector3(basePose.x + tileSize * tileScale * 2.0f, basePose.y + tileSize * tileScale * 1.5f);
        int[][] tileMap = {
            new int[]{ 136, 136, 113, 136, 49, 136, 136, 136, 136, 136, 136 },
            new int[]{ 136,   0,   1,  3,  2,  3,  3,  4,  1,  5, 136 },
            new int[]{ 136,  22,  25, 88, 89, 88, 89, 88, 89, 90, 136 },
            new int[]{   6,  22,  26, 89, 88, 89, 88, 89, 88, 68, 136 },
            new int[]{ 136,  22,  25, 88, 89, 88, 89, 88, 89, 90, 136 },
            new int[]{ 136,  22,  26, 89, 88, 89, 88, 89, 88, 68, 136 },
            new int[]{ 136,  22,  25, 88, 89, 88, 89, 88, 89, 90, 136 },
            new int[]{ 136,  22,  26, 89, 88, 89, 88, 89, 88, 68, 136 },
            new int[]{ 136,  107, 108, 109, 108, 109, 108, 109, 111, 112, 136 },
            new int[]{ 136, 136, 6, 136, 136, 136, 136, 91, 136, 136, 136 }
        };
        var tileBasePos = new Vector3(-6, 6, 0);

        createChar("char1", 3, 3);
        createChar("char2", 4, 4);

        // baseTiles = new GameObject[tileY][tileX];
        for (int y = 0; y < tileY; ++y)
        {
            for (int x = 0; x < tileX; ++x)
            {
                var tile = new GameObject("Sprite" + x + "-" + y);
                var tileRenderer = tile.AddComponent<SpriteRenderer>();
                tileRenderer.sprite = tiles[tileMap[y][x]];
                tileRenderer.transform.position = new Vector3(getWorldPositionX(x), getWorldPositionY(y), 10.0f);
                tileRenderer.transform.localScale = new Vector3(gameMain.systemConfig.tileScale, gameMain.systemConfig.tileScale);
                tileRenderer.transform.parent = transform;
                tile.AddComponent<BgTile>().SetParent(this);
                // baseTiles [y] [x] = tile;
            }
        }
        transform.position = new Vector3(-14, 6, 0);
    }

    void Update()
    {

    }

    public bool isPlayerOn(BgTile tile)
    {
        if (tile.transform.position == playerSprite.transform.position)
        {
            return true;
        }
        return false;
    }
    public GameObject onTileCharacter(BgTile tile)
    {
        var tilePos = tile.transform.position;
        foreach(var character in characters)
        {
            if (tilePos == character.transform.position)
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

    public void createChar(string name, int posX, int posY)
    {
        var character = new GameObject(name, typeof(SpriteRenderer));
        var charMan = character.AddComponent<CharacterManager>();
        charMan.transform.parent = transform;
        character.transform.localScale = new Vector2(6, 6);
        charMan.setPosition(posX, posY);
        var anim = character.AddComponent<Animator>();
        anim.runtimeAnimatorController = RuntimeAnimatorController.Instantiate(Resources.Load<RuntimeAnimatorController>("aPlayer_0"));
        characters.Add(character);
    }
    GameObject statusWindow;
    public GameObject createStatusWindow()
    {
        if (statusWindow != null)
        {
            deleteStatusWindow();
        }
        statusWindow = new GameObject("statusWindow");
        var render = statusWindow.AddComponent<SpriteRenderer>();
        render.sprite = Resources.Load<Sprite>("frame");
        // status rendering
        var parent = GameObject.Find("UI");
        var statusText = new GameObject("statusText");
        var text = statusText.AddComponent<GUIText>();
        text.text = "status";
        statusText.transform.parent = parent.transform;
        return statusWindow;
    }
    public void deleteStatusWindow()
    {
        if (statusWindow != null)
        {
            Destroy(statusWindow);
            statusWindow = null;
        }
    }
}
