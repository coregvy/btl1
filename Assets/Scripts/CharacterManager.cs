﻿using System.Collections;
using System.Collections.Generic;
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
        var pos = BgManager.getWorldPosition(x, y, -3);
        Debug.Log("player pos: " + pos);
        transform.localPosition = pos;
		onTile = bgMan.getTile(x, y);
    }

	public CharacterInfo getCharacterInfo()
    {
        return status;
    }
    public void setBgManager(BgManager bg)
    {
        bgMan = bg;
		transform.parent = bgMan.transform;
	}

    public void openStatusWindow()
    {
        Debug.Log("open status window: " + name);
        bgMan.createStatusWindow(status);
    }
    public void openControllWindow()
    {
        bgMan.createControllWindow(status);
    }
	public static CharacterManager createChar(string name, string animCtrl, int posX, int posY, BgManager bgMan)
    {
		var charMan = createChar(name, animCtrl);
		charMan.setBgManager(bgMan);
		charMan.setPosition(posX, posY);
		var cs = charMan.getCharacterInfo();
		// tmp
		cs.hp = posX * 2;
		cs.power = posY * 2;
        cs.attackRange = 1;
        return charMan;
    }

	private static CharacterManager createChar(string name, string animCtrl) {
		var character = new GameObject(name, typeof(SpriteRenderer));
		var charMan = character.AddComponent<CharacterManager>();
		character.transform.localScale = new Vector2(6, 6);
        var ci = charMan.getCharacterInfo ();
        ci.name = name;
        ci.animNameBase = animCtrl;
        charMan.updateAnimator("s");
        //var anim = character.AddComponent<Animator>();
        //anim.runtimeAnimatorController = RuntimeAnimatorController.Instantiate(Resources.Load<RuntimeAnimatorController>(animCtrl));
        return charMan;
	}
    public void updateAnimator(string act)
    {
        var animName = status.animNameBase + "_" + act;
        if (animName.Equals(nowAnim)) return;
        var anim = GetComponent<Animator>();
        anim.runtimeAnimatorController = RuntimeAnimatorController.Instantiate(Resources.Load<RuntimeAnimatorController>(animName));
        nowAnim = animName;
    }
}
