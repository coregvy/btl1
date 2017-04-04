using UnityEngine;

public class ControllWindowManager : MonoBehaviour
{
    static GameMain gameMain = GameMain.Instance;
    CharacterInfo charStatus;

    // Use this for initialization
    void Start()
    {
        Debug.Log("controll window start.");
    }
    void OnGUI()
    {
        if (GUI.Button(new Rect(260, 150, 200, 50), "攻撃"))
        {
            Debug.Log("pushed! 1");
            // bgMan.deleteControllWindow();
            bgMan.updateTileColor(charStatus, new Color(1, 0, 0));
            gameMain.gameStatus.ctrlStatus = ControllStatus.CharacterChooseTarget;
        }
        if (GUI.Button(new Rect(260, 210, 200, 50), "キャンセル"))
        {
            Debug.Log("pushed! 2");
            bgMan.deleteControllWindow();
            var selPlayer = gameMain.gameStatus.selectedPlayer;
            if(selPlayer != null) {
                Debug.Log("selected player: " + selPlayer.name);
                selPlayer.getCharacterInfo().action = CharacterAnimAct.South;
                gameMain.gameStatus.ctrlStatus = ControllStatus.Free;
                bgMan.updateTileColor(charStatus, new Color(1, 1, 1));
                gameMain.gameStatus.selectedPlayer = null;
            }
        }
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
	public void setCharacterInfo(CharacterInfo status)
    {
        charStatus = status;
    }
}
