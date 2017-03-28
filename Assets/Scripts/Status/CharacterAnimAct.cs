public enum CharacterAnimAct
{
    North,
    East,
    West,
    South,
}

public static class CharacterAnimActExt
{
    public static string ActionName(this CharacterAnimAct act)
    {
        switch(act)
        {
            case CharacterAnimAct.North:
                return "n";
            case CharacterAnimAct.East:
                return "e";
            case CharacterAnimAct.West:
                return "w";
            case CharacterAnimAct.South:
                return "s";
            default:
                return null;
        }
    }
}
