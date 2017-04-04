using System;
using UnityEngine;

[Serializable]
public class CharacterInfo
{
    public string name;
    public CharacterType type;
    public PointXY position;
    //public int posX ;
    //public int posY ;
    public int hp;
    public int power;
    public int attackRange;
    public string animNameBase;

    [SerializeField]
    private CharacterAnimAct Action;
    public CharacterAnimAct action
    {
        get { return Action; }
        set { Action = value; charaMan.updateAnimator(action.ActionName()); }
    }

    [NonSerialized]
    private CharacterManager charaMan;
    public void setCharacterManager(CharacterManager characterManager)
    {
        charaMan = characterManager;
    }
}
