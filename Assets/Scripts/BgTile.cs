using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class BgTile : MonoBehaviour
{
    Color maskColor = new Color(1, 1, 1);
    Color beforeColor = new Color();
    static GameMain gameMain = GameMain.Instance;
    BgManager bgparent;
    public PointXY position = new PointXY();
    //public int posX { get; private set; }
    //public int posY { get; private set; }

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
        if(gameMain.gameStatus.ctrlStatus == ControllStatus.CharacterChooseMove)
        {

        }
        var onChar = bgparent.onTileCharacter(this);
        if (onChar != null)
        {
            onChar.GetComponent<CharacterManager>().openStatusWindow();
            isShowStatusWindow = true;
        }
        var sp = gameMain.gameStatus.selectedPlayer;
        if (sp != null)
        {
            var ci = sp.getCharacterInfo();
            var dx = position.x - ci.position.x;
            var dy = position.y - ci.position.y;
            if (dy > dx && dy < -dx)
            {
                ci.action = CharacterAnimAct.West;
            }
            else if (dy <= -dx && dy <= dx)
            {
                ci.action = CharacterAnimAct.North;
            }
            else if (dy > -dx && dy < dx)
            {
                ci.action = CharacterAnimAct.East;
            }
            else // todo
            {
                ci.action = CharacterAnimAct.South;
            }
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
            bgparent.createControllWindow(onChar);
        }
        else
        {
            Debug.Log("player none this tile. " + name);
        }
    }
    public void setPosition(int x, int y)
    {
        position.x = x;
        position.y = y;
        var pos = BgManager.getWorldPosition(x, y, -1);
        // Debug.Log("player pos: " + pos);
        transform.position = pos;
    }

    public void setMaskColor(Color color)
    {
        //Debug.Log(name + ": set color " + color);
        beforeColor = maskColor;
        maskColor = color;
        GetComponent<SpriteRenderer>().color = maskColor;
    }
    public void setBeforeMaskColor()
    {
        //Debug.Log(name + ": set before color " + beforeColor);
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
        //Debug.Log(name + ": add color x" + color + " -> " + maskColor);
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
