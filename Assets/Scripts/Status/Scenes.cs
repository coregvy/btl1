using System;

public enum Scenes
{
    Title,
    Config,
}


public static class ScenesExt
{
    public static string SceneName(this Scenes scene)
    {
        return scene.ToString();
    }
}
