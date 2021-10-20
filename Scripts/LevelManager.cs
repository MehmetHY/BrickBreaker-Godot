using Godot;
using System;

public class LevelManager : Node2D
{
    private Ball _ball;
    private Player _player;

    public override void _Ready()
    {
        _ball = GetNode<Ball>("Ball");
        _player = GetNode<Player>("Player");
        _ball.ActivePlayer = _player;
        GameManager.IsPlaying = true;
    }
}
