using Godot;
using System;

public class Ball : RigidBody2D
{
    public Player ActivePlayer { get; set; }
    public bool IsLaunched { get; set; } = false;

    private bool _shouldLaunch = false;
    private bool _shouldReset = false;

    private const float RANDOM_FORCE = 200.0f;
    private const float MAX_SPEED = 1000.0f;
    private const float LAUNCH_SPEED = 8000.0f;
    private const float STICK_Y_OFFSET = -50.0f;

    public override void _Ready()
    {
        this.Connect("body_entered", this, nameof(ApplyRandomImpulseOnHit));
    }

    public override void _IntegrateForces(Physics2DDirectBodyState state)
    {
        if (GameManager.IsPlaying)
        {
            if (!IsLaunched)
            {
                if (_shouldLaunch)
                {
                    FreeBall(state);
                    return;
                }
                StickToPlayer(state);
            }
            else
            {
                CheckMaxSpeed(state);
            }
            if (_shouldReset)
            {
                _shouldReset = false;
                state.LinearVelocity = Vector2.Zero;
            }
        }
    }

    public void Reset() 
    {
        IsLaunched = false;
        _shouldLaunch = false;
        _shouldReset = true;
    }

    private void StickToPlayer(Physics2DDirectBodyState state)
    {
        state.Transform = new Transform2D(0, new Vector2(ActivePlayer.Position.x, ActivePlayer.Position.y + STICK_Y_OFFSET));
    }

    private void FreeBall(Physics2DDirectBodyState state)
    {
        IsLaunched = true;
        _shouldLaunch = false;
        state.ApplyCentralImpulse(new Vector2(Utils.GenerateRandomFloat(-1, 1), -2).Normalized() * LAUNCH_SPEED);
    }

    private void CheckMaxSpeed(Physics2DDirectBodyState state)
    {
        if (state.LinearVelocity.Length() > MAX_SPEED)
        {
            state.LinearVelocity = state.LinearVelocity.Normalized() * MAX_SPEED;
        }
    }

    public void Launch()
    {
        _shouldLaunch = true;
    }

    private void ApplyRandomImpulseOnHit(Node body)
    {
        ApplyCentralImpulse(new Vector2(Utils.GenerateRandomFloat(-RANDOM_FORCE, RANDOM_FORCE), Utils.GenerateRandomFloat(-RANDOM_FORCE, RANDOM_FORCE)));
    }

}
