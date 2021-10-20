using Godot;
using System;

public class Player : RigidBody2D
{
    public int Health { get; set; } = 3;
    private const float SPEED = 750.0f;
    private bool _isHoldingLeft = false;
    private bool _isHoldingRight = false;
    private float _leftBoundary;
    private float _rightBoundary;

    public override void _Ready()
    {
        _leftBoundary = GetNode<Sprite>("Sprite").Texture.GetWidth() / 2.0f;
        _rightBoundary = GetViewportRect().Size.x - _leftBoundary;
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
            if (Position.x < _leftBoundary)
            {
                Position = new Vector2(_leftBoundary, Position.y);
            }
        }
        else if (_isHoldingRight)
        {
            Translate(new Vector2(SPEED * delta, 0.0f));
            if (Position.x > _rightBoundary)
            {
                Position = new Vector2(_rightBoundary, Position.y);
            }
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
