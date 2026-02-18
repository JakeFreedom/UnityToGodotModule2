using Godot;
using System;
using System.Collections.Generic;

public partial class SpawnerRing : Node3D
{
	[Export]
	public int maxSpawnDelay = 5; //We will pick a random number between 0 and 5 to set the delay for the next drop
	[Export]
	public PackedScene[] objectToSpawn;

	[Export(PropertyHint.Range, "1.0,8.0,.1")]
	public float spawnRadius = 8;

	[Export] private int BurstSpawnThreshold = 10;

	[Export] private int BurstSpawnAmount = 8;
	
	RandomNumberGenerator rng;
	Timer t;
	List<Node3D> spawnedDropList;
	
	public override void _Ready()
	{
		spawnedDropList = new List<Node3D>();
		rng = new RandomNumberGenerator();
		t = new Timer();
		t.WaitTime = 1;// rng.RandiRange(0, maxSpawnDelay);
		t.Autostart = true;
		t.Timeout += CreateDrop;
		AddChild(t);
		t.Start();
	}


	Vector3 spawnPosition = Vector3.Zero;
	int spawnedDrops;
	private void CreateDrop()
	{
		if (spawnedDrops >= BurstSpawnThreshold)
		{
			for(int i = 0; i < BurstSpawnAmount; i++)
			{
                spawnPosition = GetPosition();
                SpawnDrop(spawnPosition);

				spawnedDrops = 0;
            }
		}
		else
		{
			spawnPosition = GetPosition();
			SpawnDrop(spawnPosition);
		}

		t.Start(rng.RandiRange(0, maxSpawnDelay));
	}

	private Vector3 GetPosition()
	{
        Vector2 randDirection = Vector2.Right.Rotated(rng.RandfRange(0, Mathf.Tau));
        spawnPosition = GetNode<CsgCylinder3D>("SpawnCenter").GlobalPosition + (new Vector3(randDirection.X, 0, randDirection.Y) * rng.RandfRange(-spawnRadius, spawnRadius));
		return spawnPosition;
    }

	private void SpawnDrop(Vector3 spawnPosition)
	{
        Node3D spawnedObject = objectToSpawn[rng.RandiRange(0, objectToSpawn.Length - 1)].Instantiate<Node3D>();
        spawnedObject.Position = spawnPosition;
        AddChild(spawnedObject);
		spawnedDrops += 1;
		spawnedDropList.Add(spawnedObject);
    }
}
