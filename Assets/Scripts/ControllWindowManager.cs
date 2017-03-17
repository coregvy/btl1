using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ControllWindowManager : MonoBehaviour {
	// Dictionary<string, Action<string>> buttons;
//	List<GameObject> buttons;
	static GameMain gameMain = GameMain.Instance;

	// Use this for initialization
	void Start () {
//		buttons = new List<GameObject> ();
		Debug.Log ("controll window start.");
//		var btnPrf = Resources.Load ("Prefabs/Button");
//		buttons.Add (Instantiate (btnPrf, GameObject.Find ("UI").transform) as GameObject);
	}
	void OnGUI() {
		if ( GUI.Button(new Rect(260, 150, 200, 50), "Button1") ) {
			Debug.Log ("pushed! 1");
			bgMan.deleteControllWindow ();
			bgMan.updateTileColor (charStatus, 2, new Color (1, 0, 0));
			gameMain.gameStatus.ctrlStatus = ControllStatus.CharacterChooseTarget;
		}
		if ( GUI.Button(new Rect(260, 210, 200, 50), "Button2") ) {
			Debug.Log ("pushed! 2");
		}
	}
	// Update is called once per frame
	void Update () {
		
	}
	BgManager bgMan;
	public void setBgManager(BgManager bg)
	{
		bgMan = bg;
	}
	CharacterStatus charStatus;
	public void setCharacterStatus(CharacterStatus status) {
		charStatus = status;
	}
}
