using Godot;
using System;

public class Player : RigidBody2D
{
    public int Health { get; set; } = 3;
    private const float SPEED = 750.0f;
    private bool _isHoldingLeft = false;
    private bool _isHoldingRight = false;

    public override void _Ready()
    {
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
            Translate(new Vector2(-SPEED * delta, 0.0f));
        }
        else if (_isHoldingRight)
        {
            Translate(new Vector2(SPEED * delta, 0.0f));
        }
        
    }

    public int DecreaseHealth()
    {
        return --Health;
    }

    private void CheckBoundaries()
    {
    }
}
