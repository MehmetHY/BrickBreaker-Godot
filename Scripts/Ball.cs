using Godot;
using System;

public class Ball : RigidBody2D
{
    public Player ActivePlayer { get; set; }
    private bool _isLaunched = false;
    private const float STICK_POSITION__Y = 1000.0f;

    public override void _Ready()
    {
        GameManager.ActiveBall = this;
    }
    public override void _Process(float delta)
    {
        if (GameManager.IsPlaying)
        {
            HandleGameStates();
        }
    }
    private void HandleGameStates()
    {
        if (!_isLaunched)
        {
            StickToPlayer();
        }
    }
    public void StickToPlayer()
    {
        Position = new Vector2(ActivePlayer.Position.x, STICK_POSITION__Y);
    }

}
