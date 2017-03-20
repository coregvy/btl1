using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public BgTile onTile { private get; set; }
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
        var pos = BgManager.getWorldPosition(x, y, -3);
        Debug.Log("player pos: " + pos);
        transform.localPosition = pos;
    }

    public CharacterStatus getCharacterStatus()
    {
        return status;
    }
    BgManager bgMan;
    public void setBgManager(BgManager bg)
    {
        bgMan = bg;
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
    public static CharacterManager createChar(string name, int posX, int posY, BgManager bgMan)
    {
        var character = new GameObject(name, typeof(SpriteRenderer));
        var charMan = character.AddComponent<CharacterManager>();
        charMan.transform.parent = bgMan.transform;
        charMan.setBgManager(bgMan);
        character.transform.localScale = new Vector2(6, 6);
        charMan.setPosition(posX, posY);
        charMan.onTile = bgMan.getTile(posX, posY).GetComponent<BgTile>();
        var cs = charMan.getCharacterStatus();
        // tmp
        cs.hp = posX * 2;
        cs.power = posY * 2;
        var anim = character.AddComponent<Animator>();
        anim.runtimeAnimatorController = RuntimeAnimatorController.Instantiate(Resources.Load<RuntimeAnimatorController>("aPlayer_0"));
        //characters.Add(character);
        return charMan;
    }

}
