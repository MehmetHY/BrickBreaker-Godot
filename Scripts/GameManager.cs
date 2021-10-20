using Godot;
using System;

public class GameManager : Node
{
    public static readonly string MainMenuScenePath = "res://Scenes/MainMenu.tscn";
    public static readonly string FirstLevelScenePath = "res://Scenes/Level01.tscn";

    public static bool IsPlaying { get; set; } = false;

    public static void ChangeScene(Node node, string path)
    {
        node.GetTree().ChangeScene(path);
    }
}
