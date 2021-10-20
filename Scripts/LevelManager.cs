using Godot;
using System;

public class LevelManager : Node2D
{
    private Ball _ball;
    private Player _player;
    private Area2D _deathZone;
    private Node2D _bricks;

    private bool _shouldLaunch = false;
    private bool _missedBall = false;
    private int _brickCount;
    private static PackedScene _ballScene = ResourceLoader.Load<PackedScene>("res://Prefabs/Ball.tscn");

    [Export] public readonly string NEXT_SCENE = GameManager.MainMenuScenePath;

    public override void _Ready()
    {
        _ball = GetNode<Ball>("Ball");
        _player = GetNode<Player>("Player");
        _deathZone = GetNode<Area2D>("DeathZone");
        _bricks = GetNode<Node2D>("Bricks");
        _deathZone.Connect("body_entered", this, nameof(OnMissedBall));
        _ball.ActivePlayer = _player;
        _brickCount = _bricks.GetChildCount();

        foreach (Brick brick in _bricks.GetChildren())
        {
            brick.Connect("body_entered", this, nameof(HandleBrickDamage), new Godot.Collections.Array(brick));
            GD.Print(brick.Name);
        }

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
        GameManager.ChangeScene(this, GameManager.MainMenuScenePath);
    }

    private void OnMissedBall(Node _)
    {
        _missedBall = true;
    }

    private void HandleBrickDamage(Node _, Brick brick)
    {
        GD.Print("hit");
        if (brick.TakeDamage() < 1)
        {
            brick.QueueFree();
            _brickCount--;
            if (_brickCount < 1)
            {
                LoadNextLevel();
            }
        }
    }

    private void LoadNextLevel()
    {
        GameManager.ChangeScene(this, NEXT_SCENE);
    }
}
