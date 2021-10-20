using Godot;
using System;

public class LevelManager : Node2D
{
    private Ball _ball;
    private Player _player;
    private Area2D _deathZone;

    private bool _shouldLaunch = false;
    private bool _missedBall = false;
    private PackedScene _ballScene = ResourceLoader.Load<PackedScene>("res://Prefabs/Ball.tscn");

    public override void _Ready()
    {
        _ball = GetNode<Ball>("Ball");
        _player = GetNode<Player>("Player");
        _deathZone = GetNode<Area2D>("DeathZone");
        _deathZone.Connect("body_entered", this, nameof(OnMissedBall));
        _ball.ActivePlayer = _player;
        GameManager.IsPlaying = true;
    }

    public override void _Input(InputEvent @event)
    {
        if (GameManager.IsPlaying)
        {
            HandleLaunch(@event);
        }
    }

    private void HandleLaunch(InputEvent @event)
    {
        if (@event.IsActionReleased("Fire"))
        {
            if (!_ball.IsLaunched)
            {
                _shouldLaunch = true;
            }
        }
    }

    public override void _PhysicsProcess(float delta)
    {
        if (_shouldLaunch)
        {
            LaunchBall();
        }
        if (_missedBall)
        {
            HandleBallMiss();
        }
    }

    private void LaunchBall()
    {
        _ball.Launch();
        _shouldLaunch = false;
    }

    private void HandleBallMiss()
    {
        _missedBall = false;
        _ball.Reset();
        if (_player.DecreaseHealth() < 1)
        {
            HandleGameOver();
        }
    }

    private void HandleGameOver()
    {
        GD.Print("GameOVer");
    }

    private void OnMissedBall(Node _)
    {
        _missedBall = true;
    }
}
