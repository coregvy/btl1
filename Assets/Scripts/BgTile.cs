using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]
public class BgTile : MonoBehaviour
{

    BgManager bgparent;
	public int posX{ get; private set; }
	public int posY{ get; private set; }

    public void SetParent(BgManager bgparent)
    {
        this.bgparent = bgparent;
    }
    // Use this for initialization
    void Start()
    {
        GetComponent<BoxCollider2D>().size = new Vector2(0.16f, 0.16f);
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnMouseEnter()
    {
        Debug.Log("mouse enter: " + name);
        GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f);
    }
    void OnMouseExit()
    {
        Debug.Log("mouse exit: " + name);
        GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f);
    }

    private Vector3 screenPoint;
    private Vector3 offset;
    void OnMouseDown()
    {
        Debug.Log("bg mouse down: " + name);
        this.screenPoint = Camera.main.WorldToScreenPoint(transform.parent.transform.position);
        this.offset = transform.parent.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
		var onChar = bgparent.onTileCharacter (this);
		if (onChar != null) {
			onChar.GetComponent<CharacterManager> ().openStatusWindow ();
		}
    }

    void OnMouseDrag()
    {
        Vector3 currentScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 currentPosition = Camera.main.ScreenToWorldPoint(currentScreenPoint) + this.offset;
        transform.parent.transform.position = currentPosition;
    }
    void OnMouseUp()
    {
        var onChar = bgparent.onTileCharacter(this);
        if (onChar!=null)
        {
            Debug.Log(onChar.name + " is on this tile. " + name);
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
		Debug.Log("player pos: " + pos);
		transform.position = pos;
	}
}
