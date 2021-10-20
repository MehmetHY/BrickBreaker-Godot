using Godot;
using System;

public class GameManager : Node
{
    public static Player ActivePlayer { get; set; } = null;
    public static Ball ActiveBall { get; set; } = null;
    public static bool IsPlaying { get; set; } = true;
}
