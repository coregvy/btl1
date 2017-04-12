using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CharacterManager : MonoBehaviour
{
    private BgTile onTile {  get;  set; }
	private BgManager bgMan;
    private string nowAnim;
    [SerializeField]
	CharacterInfo status = new CharacterInfo();
    // Use this for initialization
    void Start()
    {
        status.setCharacterManager(this);
        status.name = name;
        // setPosition (3, 3);
    }

    // Update is called once per frame
    void Update()
    {

    }
	public void setPosition(int x, int y)
    {
        Debug.Log(name + ": move " + status.position + " -> (" + x + ", " + y + ")");
        //status.posX = x;
        //status.posY = y;
        status.position.x = x;
        status.position.y = y;
        var pos = BgManager.getWorldPosition(x, y, -3);
        //Debug.Log("player pos: " + pos);
        transform.localPosition = pos;
		onTile = bgMan.getTile(x, y);
    }

    public void setPosition(PointXY pos)
    {
        setPosition(pos.x, pos.y);
    }

    public CharacterInfo getCharacterInfo()
    {
        return status;
    }
    public void setBgManager(BgManager bg)
    {
        bgMan = bg;
		transform.parent = bgMan.transform;
        status.setCharacterManager(this);
	}

    public void prepareMove()
    {
        // ControllStatusはcwmで設定
        Debug.Log(name + ": prepare move begin.");
        createDummy();
        bgMan.updateTileColor(status, status.moveRange, new Color(0, 1, 0), TileStatus.Movable);
    }

    GameObject dummy;
    private void createDummy()
    {
        dummy = Instantiate(gameObject);
        dummy.name = name + "-dummy";
        var cm = dummy.GetComponent<CharacterManager>();
        cm.setBgManager(bgMan);
        cm.setPosition(status.position);
        cm.status.action = CharacterAnimAct.South;
        dummy.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);
        bgMan.setMoveDummy(cm);
    }

    public void endMove()
    {
        Debug.Log(name + ": end move and set new position.");
        var newPos = dummy.GetComponent<CharacterManager>().getCharacterInfo().position;
        cancelMove();
        setPosition(newPos.x, newPos.y);
    }
    public void cancelMove()
    {
        GameMain.Instance.gameStatus.ctrlStatus = ControllStatus.Free;
        // 移動前に
        bgMan.updateTileColor(status, status.moveRange, new Color(1, 1, 1), TileStatus.None);
        bgMan.deleteMoveDummy();
        Destroy(dummy);
        dummy = null;
        GameMain.Instance.gameStatus.ctrlStatus = ControllStatus.Free;
        bgMan.deleteControllWindow();
    }

    public void openStatusWindow()
    {
        Debug.Log("open status window: " + name);
        bgMan.createStatusWindow(status);
    }
    //public void openControllWindow()
    //{
    //    bgMan.createControllWindow(status);
    //}

	public static CharacterManager createChar(string name, string animCtrl, int posX, int posY, BgManager bgMan)
    {
		var charMan = createCharMain(name, animCtrl);
		charMan.setBgManager(bgMan);
		charMan.setPosition(posX, posY);
		var cs = charMan.getCharacterInfo();
		// tmp
		cs.hp = posX * 2;
		cs.power = posY * 2;
        cs.attackRange = 2;
        cs.moveRange = 3;
        return charMan;
    }

	private static CharacterManager createCharMain(string name, string animCtrl) {
		var character = new GameObject(name, typeof(SpriteRenderer));
		var charMan = character.AddComponent<CharacterManager>();
		character.transform.localScale = new Vector2(6, 6);
        var ci = charMan.getCharacterInfo ();
        ci.setCharacterManager(charMan);
        ci.name = name;
        ci.animNameBase = "Animator/" + animCtrl;
        ci.action = CharacterAnimAct.South;
        return charMan;
	}

    /// <summary>
    /// CharacterInfo.setActionでのみ使用する.
    /// </summary>
    /// <param name="act">next action</param>
    public void updateAnimator(string act)
    {
        var animName = status.animNameBase + "_" + act;
        if (animName.Equals(nowAnim)) return;
        var anim = GetComponent<Animator>();
        anim.runtimeAnimatorController = RuntimeAnimatorController.Instantiate(Resources.Load<RuntimeAnimatorController>(animName));
        nowAnim = animName;
    }
}
