using Godot;
using System;

public partial class SwitchLever : Node3D
{
	[Export] Node3D light;
	Area3D switchHitBox;
	bool switchLocation = false;
	AnimationPlayer switchPlayer;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		switchHitBox = GetNode<Area3D>("HitBox");
		switchHitBox.AreaEntered += AreaEnteredHandler;
		switchPlayer = GetNode<AnimationPlayer>("AnimationPlayer");

		light.Visible = false;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}


	private void AreaEnteredHandler(Area3D otherArea)
	{
		if(switchLocation == false)
		{
			switchPlayer.Play("TurnOn");
			switchLocation = true;
			light.Visible = true;
		}
		else
		{
			switchPlayer.Play("TurnOff");
			switchLocation = false;
			light.Visible = false;
		}

	}
}
