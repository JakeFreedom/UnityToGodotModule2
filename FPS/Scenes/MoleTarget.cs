using Godot;
using System;
using System.Reflection.Metadata;

public partial class MoleTarget : Node3D
{

	[Export(PropertyHint.Range, ".5, 3, .1")]
	public float moleSpeed = 1.0f;
	[Export(PropertyHint.Range, "1.5,3.5,.5")]
	public int maxHeight = 2;
	Godot.Timer deathTimer;
	Area3D hitBox;
	float targetDirection;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		deathTimer = new Godot.Timer();
		deathTimer.WaitTime = 9;
		deathTimer.Timeout += DeathTimerTimeOut;
		deathTimer.Autostart = true;
		AddChild(deathTimer);
		deathTimer.Start();


		hitBox = GetNode<Area3D>("HitBox");
		hitBox.AreaEntered += AreaEnteredHandler;

		targetDirection = maxHeight;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		//we are going to be movoing on the Y axis starting from our spawn point to a max height that is configurable.
		if(Position.Y <= targetDirection)
		{
			//Keep moving up
			targetDirection = maxHeight;
			Position = new Vector3(Position.X, Position.Y + (float)delta, Position.Z);
		}
		else
		{
			targetDirection = -15;
            Position = new Vector3(Position.X, Position.Y - (float)delta, Position.Z);
			//Start moving down
			if (Position.Y <= -15)
				QueueFree();
		}
	}


	private void AreaEnteredHandler(Area3D otherArea)
	{

	}

	private void DeathTimerTimeOut()
	{
		this.QueueFree();
	}
}
