using Godot;
using System;

public class Brick : RigidBody2D
{
    [Export] public int _health = 1;

    private Sprite _sprite;

    public static readonly Color[] colors = new Color[3]{new Color(1.0f, 0.5f, 0.5f), new Color(1.0f, 1.0f, 0.5f), new Color(0.5f, 1.0f, 0.5f)};
    
    public override void _Ready()
    {
        _sprite = GetNode<Sprite>("Sprite");
        ChangeColor();
    }

    private void ChangeColor()
    {
        _sprite.Modulate = colors[_health - 1];
    }

    public int TakeDamage()
    {
        _health--;
        if (_health > 0)
        {
            ChangeColor();
        }
        return _health;
    }
}
