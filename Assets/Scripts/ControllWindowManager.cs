using System;
using UnityEngine;

public class ControllWindowManager : MonoBehaviour
{
    static GameMain gameMain = GameMain.Instance;
    CharacterManager charMan;
    readonly Vector3 scale = new Vector3(0.7f, 3, 1);
    readonly Vector3 position = new Vector3(4, 3, -2);

    // Use this for initialization
    void Start()
    {
        Debug.Log("controll window start.");
        transform.localScale = scale;
        transform.localPosition = position;
    }
    void OnGUI()
    {
        var cs = charMan.getCharacterInfo();
        if (GUI.Button(new Rect(260, 150, 200, 50), "攻撃"))
        {
            Debug.Log("pushed! 1");
            // bgMan.deleteControllWindow();
            bgMan.updateTileColor(cs, cs.attackRange, new Color(1, 0, 0), TileStatus.Attackable);
            gameMain.gameStatus.ctrlStatus = ControllStatus.CharacterChooseAttack;
        }
        if (GUI.Button(new Rect(260, 210, 200, 50), "移動"))
        {
            Debug.Log("pushed! 2");
            bgMan.updateTileColor(cs, cs.attackRange, new Color(0, 1, 0));
            charMan.prepareMove();
            gameMain.gameStatus.ctrlStatus = ControllStatus.CharacterChooseMove;
        }
        if (GUI.Button(new Rect(260, 270, 200, 50), "キャンセル"))
        {
            Debug.Log("pushed! 3");
            deleteWindow();
            //bgMan.deleteControllWindow();
        }
    }

    public void deleteWindow()
    {
        var cs = charMan.getCharacterInfo();
        //bgMan.deleteControllWindow();
        Debug.Log("selected player: " + charMan.name);
        charMan.getCharacterInfo().action = CharacterAnimAct.South;
        if (gameMain.gameStatus.ctrlStatus == ControllStatus.CharacterChooseAttack)
        {
            bgMan.updateTileColor(cs, cs.attackRange, new Color(1, 1, 1));
        }
        else if (gameMain.gameStatus.ctrlStatus == ControllStatus.CharacterChooseMove)
        {
            bgMan.updateTileColor(cs, cs.moveRange, new Color(1, 1, 1));
            charMan.cancelMove();
        }
        gameMain.gameStatus.selectedPlayer = null;
        gameMain.gameStatus.ctrlStatus = ControllStatus.Free;
    }

    // Update is called once per frame
    void Update()
    {

    }
    BgManager bgMan;
    public void setBgManager(BgManager bg)
    {
        bgMan = bg;
    }
    public void setCharacter(CharacterManager cm)
    {
        charMan = cm;
        gameMain.gameStatus.selectedPlayer = cm;
    }
}
