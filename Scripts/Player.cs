using Godot;
using System;

public class Player : Node2D
{
    private const float _speed = 750.0f;
    private bool _isHoldingLeft = false;
    private bool _isHoldingRight = false;

    public override void _Ready()
    {
        GameManager.ActivePlayer = this;
    }

    public override void _Process(float delta)
    {
        HandleInput();
        HandleMovement(delta);
        CheckBoundaries();
    }


    private void HandleInput()
    {
        if (Input.IsActionJustPressed("Left"))
        {
            _isHoldingLeft = true;
        }
        else if (Input.IsActionJustReleased("Left"))
        {
            _isHoldingLeft = false;
        }
        if (Input.IsActionJustPressed("Right"))
        {
            _isHoldingRight = true;
        }
        else if (Input.IsActionJustReleased("Right"))
        {
            _isHoldingRight = false;
        }
    }

    private void HandleMovement(float delta)
    {
        if (_isHoldingLeft)
        {
            Translate(new Vector2(-_speed * delta, 0.0f));
        }
        else if (_isHoldingRight)
        {
            Translate(new Vector2(_speed * delta, 0.0f));
        }
    }

    private void CheckBoundaries()
    {
    }
}
