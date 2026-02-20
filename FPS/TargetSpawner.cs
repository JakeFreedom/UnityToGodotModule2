using Godot;
using System;
using System.Threading;

public partial class TargetSpawner : Node3D
{
	[Export]
	public PackedScene sceneToSpawn;
	[Export(PropertyHint.Range, "-13, 0 ,1")]
	public int SpawnRangeMin = 0;
    [Export(PropertyHint.Range, "-13, 0 ,1")]
    public int SpawnRangeMax = -13;
	// We have to lock the y, since height should not matteer, we will have to lock the z to 2 locations. Front/Back, x can be anything between
	//the edges of the room -- Which seems clunky, but we are not going
	//going to get in the weeds trying to come up with something clever.


	Godot.RandomNumberGenerator rng;
    Godot.Timer spawnTimer;
	public override void _Ready()
	{
		spawnTimer = new Godot.Timer();
		spawnTimer.WaitTime = 5.0f;
		spawnTimer.Autostart = true;
		spawnTimer.Timeout += SpawnTarget;
		AddChild(spawnTimer);
		spawnTimer.Start();
		rng = new Godot.RandomNumberGenerator();
	}

	public override void _Process(double delta)
	{
	}


	private void SpawnTarget() {
		//What can we do here so these don't spawn on top of eachother.
		//Can we keep track of the say past 10 spawn points and if we have that again, we just move it a little bit.
		//We will come back to this at a later date.
		Node3D spawnedScene = sceneToSpawn.Instantiate() as Node3D;
		spawnedScene.Position = new Vector3(rng.RandiRange(SpawnRangeMin, SpawnRangeMax), 7,rng.RandfRange(-6.5f, -5.5f));
		AddChild(spawnedScene);
		//spawnTimer.Start();
	}
}
