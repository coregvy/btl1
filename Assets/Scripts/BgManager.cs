using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgManager : MonoBehaviour
{
    [SerializeField]
    GameObject playerSprite;
	GameObject[][] baseTiles;

    // Use this for initialization
    void Start()
    {
        Debug.Log("start: " + playerSprite.GetType());
        Sprite[] tiles = Resources.LoadAll<Sprite>("baseTiles");
        float tileSize = 0.16f;
        float tileScale = 20.0f;
        var basePose = new Vector2(0, 0);
        var tileBasePose = new Vector2(-6, 6);
        const int tileX = 5;
        const int tileY = 4;
        // transform.position = new Vector3(basePose.x + tileSize * tileScale * 2.0f, basePose.y + tileSize * tileScale * 1.5f);
        int[][] tileMap = {
            new int[]{ 0, 1, 3, 3, 5 },
            new int[]{ 22, 25, 88, 89, 90 },
            new int[]{ 22, 26, 89, 88, 68 },
            new int[]{107, 108, 109, 111, 112}
        };
		// baseTiles = new GameObject[tileY][tileX];
		for (int y = 0; y < tileY; ++y)
        {
			for (int x = 0; x < tileX; ++x)
            {
                var tile = new GameObject("Sprite" + x + "-" + y);
                var tileRenderer = tile.AddComponent<SpriteRenderer>();
                tileRenderer.sprite = tiles[tileMap[y][x]];
                tileRenderer.transform.position = new Vector3(tileBasePose.x + tileSize * tileScale * x, tileBasePose.y - tileSize * tileScale * y, 10.0f);
                tileRenderer.transform.localScale = new Vector3(tileScale, tileScale);
                tileRenderer.transform.parent = transform;
                tile.AddComponent<BgTile>().SetParent(this);
				// baseTiles [y] [x] = tile;
            }
        }
    }

    void Update()
    {

    }

    public bool isPlayerOn(BgTile tile)
    {
        if(tile.transform.position == playerSprite.transform.position)
        {
            return true;
        }
        return false;
    }
}
