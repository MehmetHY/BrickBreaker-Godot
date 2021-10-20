using Godot;
using System;

public class MainMenu : Control
{
    private Button _startButton;
    private Button _quitButton;

    public override void _Ready()
    {
        _startButton = GetNode<Button>("Panel/StartButton");
        _quitButton = GetNode<Button>("Panel/QuitButton");
        _startButton.Connect("pressed", this, nameof(HandleStartButton));
        _quitButton.Connect("pressed", this, nameof(HandleQuitButton));
    }

    private void HandleStartButton()
    {
        GameManager.ChangeScene(this, GameManager.FirstLevelScenePath);
    }
    private void HandleQuitButton()
    {
        GetTree().Quit();
    }
}
