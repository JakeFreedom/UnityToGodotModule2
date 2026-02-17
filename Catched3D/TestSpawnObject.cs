using Godot;
using System;

public partial class TestSpawnObject : Node3D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Position += new Vector3(0, -1 * (float)delta, 0);
	}
}
