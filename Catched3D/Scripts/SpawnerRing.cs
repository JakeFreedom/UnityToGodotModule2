using Godot;
using System;

public partial class SpawnerRing : Node3D
{
	[Export]
	public int maxSpawnDelay = 5; //We will pick a random number between 0 and 5 to set the delay for the next drop
	[Export]
	public PackedScene[] objectToSpawn;
	// Called when the node enters the scene tree for the first time.
	RandomNumberGenerator rng;
	Timer t;
	public override void _Ready()
	{
		rng = new RandomNumberGenerator();
		t = new Timer();
		t.WaitTime = 1;// rng.RandiRange(0, maxSpawnDelay);
		t.Autostart = true;
		t.Timeout += GetSpawnPosition;
		AddChild(t);
		t.Start();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	Vector3 spawnPosition = Vector3.Zero;
	private void GetSpawnPosition()
	{
		//GD.Print("Rand Direction" + randDirection);
		Vector2 randDirection = Vector2.Right.Rotated(rng.RandfRange(0, Mathf.Tau));
		spawnPosition = GetNode<CsgCylinder3D>("SpawnCenter").GlobalPosition + (new Vector3(randDirection.X, 0, randDirection.Y) * rng.RandfRange(-8.0f, 8.0f));

		Node3D spawnedObject = objectToSpawn[rng.RandiRange(0, objectToSpawn.Length-1)].Instantiate<Node3D>();
		spawnedObject.Position = spawnPosition;
		AddChild(spawnedObject);



		t.Start(rng.RandiRange(0, maxSpawnDelay));
	}
}
