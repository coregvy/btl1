using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]
public class BgTile : MonoBehaviour
{
    Color maskColor = new Color(1, 1, 1);
    Color beforeColor = new Color();
    static GameMain gameMain = GameMain.Instance;
    BgManager bgparent;
    public int posX { get; private set; }
    public int posY { get; private set; }

    public void SetParent(BgManager bgparent)
    {
        this.bgparent = bgparent;
    }
    // Use this for initialization
    void Start()
    {
        GetComponent<BoxCollider2D>().size = new Vector2(0.16f, 0.16f);
    }


    bool isShowStatusWindow = false;
    void OnMouseEnter()
    {
        //if (gameMain.gameStatus.ctrlStatus != ControllStatus.Free)
        //    return;
        //Debug.Log("mouse enter: " + name);
        //GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f);
        addMaskColor(new Color(0.5f, 0.5f, 0.5f));
        var onChar = bgparent.onTileCharacter(this);
        if (onChar != null)
        {
            onChar.GetComponent<CharacterManager>().openStatusWindow();
            isShowStatusWindow = true;
        }
    }
    void OnMouseExit()
    {
        //if (gameMain.gameStatus.ctrlStatus != ControllStatus.Free)
        //    return;
        // Debug.Log("mouse exit: " + name);
        //GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f);
        setBeforeMaskColor();
        if (isShowStatusWindow)
        {
            bgparent.deleteStatusWindow();
            isShowStatusWindow = false;
        }
    }

    private Vector3 screenPoint;
    private Vector3 offset;
    void OnMouseDown()
    {
        Debug.Log("bg mouse down: " + name);
        this.screenPoint = Camera.main.WorldToScreenPoint(transform.parent.transform.position);
        this.offset = transform.parent.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
    }

    void OnMouseDrag()
    {
        Vector3 currentScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 currentPosition = Camera.main.ScreenToWorldPoint(currentScreenPoint) + this.offset;
        bgparent.moveWorld(currentPosition);
        //transform.parent.transform.position = currentPosition;
    }
    void OnMouseUp()
    {
        var onChar = bgparent.onTileCharacter(this);
        if (onChar != null)
        {
            Debug.Log(onChar.name + " is on this tile. " + name);
            var charMan = onChar.GetComponent<CharacterManager>();
			bgparent.createControllWindow(charMan.getCharacterInfo());
        }
        else
        {
            Debug.Log("player none this tile. " + name);
        }
    }
    public void setPosition(int x, int y)
    {
        posX = x;
        posY = y;
        var pos = BgManager.getWorldPosition(x, y, -1);
        // Debug.Log("player pos: " + pos);
        transform.position = pos;
    }

    public void setMaskColor(Color color)
    {
        Debug.Log(name + ": set color " + color);
        beforeColor = maskColor;
        maskColor = color;
        GetComponent<SpriteRenderer>().color = maskColor;
    }
    public void setBeforeMaskColor()
    {
        Debug.Log(name + ": set before color " + beforeColor);
        Color tmp = beforeColor;
        beforeColor = maskColor;
        maskColor = tmp;
        GetComponent<SpriteRenderer>().color = maskColor;
        //setMaskColor(beforeColor);
    }
    public void addMaskColor(Color color)
    {
        beforeColor = maskColor;
        maskColor = color * maskColor;
        Debug.Log(name + ": add color x" + color + " -> " + maskColor);
        GetComponent<SpriteRenderer>().color = maskColor;
    }
    public static BgTile createTile(int x, int y, Sprite tileSprite, BgManager bgMan, Transform parent)
    {
        var tile = new GameObject("Sprite" + x + "-" + y);
        var tileRenderer = tile.AddComponent<SpriteRenderer>();
        var bgt = tile.AddComponent<BgTile>();
        tileRenderer.sprite = tileSprite;
        // tileRenderer.transform.position = new Vector3(getWorldPositionX(x), getWorldPositionY(y), 10.0f);
        bgt.setPosition(x, y);
        tileRenderer.transform.localScale = new Vector3(gameMain.systemConfig.tileScale, gameMain.systemConfig.tileScale);
        tileRenderer.transform.parent = parent;
        bgt.SetParent(bgMan);
        return bgt;
    }
}
