using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{

    [SerializeField]
    CharacterStatus status = new CharacterStatus();
    // Use this for initialization
    void Start()
    {
		status.name = name;
        // setPosition (3, 3);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void setPosition(int x, int y)
    {
        status.posX = x;
        status.posY = y;
        var pos = BgManager.getWorldPosition(x, y, -1);
        Debug.Log("player pos: " + pos);
        transform.position = pos;
    }

	public CharacterStatus getCharacterStatus() {
		return status;
	}
    BgManager bgMan;
    public void setBgManager(BgManager bg)
    {
        bgMan = bg;
    }

    public void openStatusWindow()
    {
		Debug.Log ("open status window: " + name);
		bgMan.createStatusWindow(status);
    }
    public void openControllWindow()
    {
        bgMan.createControllWindow(status);
    }
}
