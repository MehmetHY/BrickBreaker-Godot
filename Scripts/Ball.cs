using Godot;
using System;

public class Ball : RigidBody2D
{
    public Player ActivePlayer { get; set; }
    public bool IsLaunched { get; set; } = false;
    
    private const float RANDOM_FORCE = 200.0f;
    private const float MAX_SPEED = 1000.0f;
    private const float LAUNCH_SPEED = 8000.0f;
    private const float STICK_Y_OFFSET = -50.0f;



    public override void _Ready()
    {
        this.Connect("body_entered", this, nameof(ApplyRandomImpulseOnHit));
    }

    public override void _PhysicsProcess(float delta)
    {
        if (GameManager.IsPlaying)
        {
            if (!IsLaunched)
            {
                Transform = new Transform2D(0.0f, ActivePlayer.Position + new Vector2(0, STICK_Y_OFFSET));
            }
            else
            {
                CheckMaxSpeed();
            }
        }
    }

    public void Launch()
    {
        IsLaunched = true;
        Mode = ModeEnum.Rigid;
        ApplyCentralImpulse(new Vector2(Utils.GenerateRandomFloat(-1, 1), -2).Normalized() * LAUNCH_SPEED);
    }

    private void CheckMaxSpeed()
    {
        if (LinearVelocity.Length() > MAX_SPEED)
        {
            LinearVelocity = LinearVelocity.Normalized() * MAX_SPEED;
        }
    }

    private void ApplyRandomImpulseOnHit(Node body)
    {
        ApplyCentralImpulse(new Vector2(Utils.GenerateRandomFloat(-RANDOM_FORCE, RANDOM_FORCE), Utils.GenerateRandomFloat(-RANDOM_FORCE, RANDOM_FORCE)));
    }

}
