using Godot;
using System;

public class Utils : Node
{
    private static readonly RandomNumberGenerator _rng = new RandomNumberGenerator();

    public static int GenerateRandomInt(int min, int max)
    {
        _rng.Randomize();
        return _rng.RandiRange(min, max);
    }
    public static float GenerateRandomFloat(float min, float max)
    {
        _rng.Randomize();
        return _rng.RandfRange(min, max);
    }
}
