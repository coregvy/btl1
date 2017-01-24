using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]
public class BgTile : MonoBehaviour {

    BgManager bgparent;
    
    public void SetParent(BgManager bgparent)
    {
        this.bgparent = bgparent;
    }
    // Use this for initialization
    void Start()
    {
        GetComponent<BoxCollider2D> ().size = new Vector2 (0.16f, 0.16f);
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
    }

    void OnMouseDrag()
    {
        Vector3 currentScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 currentPosition = Camera.main.ScreenToWorldPoint(currentScreenPoint) + this.offset;
        transform.parent.transform.position = currentPosition;
    }
    void OnMouseUp()
    {
        if(bgparent.isPlayerOn(this))
        {
            Debug.Log("player is none this tile." + name);
        }
        else
        {
            Debug.Log("player on this tile." + name);
        }
    }
}
