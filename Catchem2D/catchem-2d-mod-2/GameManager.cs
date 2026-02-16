using Godot;
using System;
using System.Collections.Generic;

public partial class GameManager : Node3D
{
    [Export] PackedScene obstacleScene;
    [Export]int spawnDelay;
    [Export(PropertyHint.Range, "5,15,1")] int obstaclesToSpawn = 5;


    Timer spawnTimer;
    RandomNumberGenerator rng = new RandomNumberGenerator();

    //Spawn obstacle at random locations
    //Have how many configurable in the inspector
    //Grab a random rotation on the z axis
    //Create a random color 

    public override void _Ready()
    {
        //We don't really need a timer to do this.
        //You really need to start following the program and have everything laid on in writting before getting to any programming.
        spawnTimer = new Timer();
        spawnTimer.WaitTime = spawnDelay;
        //We could use this timer to change the locations of the obstacles

        //But for now let's just spawn 5 random obstacles.
        //We can spawn them with in the viewport rect minus a margin on the side of 150 pixels/uints
        Vector2 screenSize = GetViewport().GetVisibleRect().Size;
        screenSize.X -= 150;
        screenSize.Y -= 150;
        GD.Print(screenSize);
        List<Vector2> spawnPoints = new List<Vector2>();
        for(int i = 0; i < obstaclesToSpawn; i++)
        {
            //Make 5 random spawnpoints
            Vector2 spawnPoint = new Vector2(rng.RandiRange(150, (int)screenSize.X), rng.RandiRange(150, (int)screenSize.Y));
            spawnPoints.Add(spawnPoint);
        }

        foreach(Vector2 spawnPoint in spawnPoints)
        {
            Obstacles obstacle = obstacleScene.Instantiate<Obstacles>() as Obstacles;
            obstacle.Position = spawnPoint;
            
            GenerateColor(obstacle);
            GiveRandomZRotation(obstacle);
            
            AddChild(obstacle);
        }

    }

    private void GenerateColor(Obstacles obj) {
        obj.color = new Vector3(rng.RandfRange(0, 1), rng.RandfRange(0, 1), rng.RandfRange(0, 1));
    
    }
    private void GiveRandomZRotation(Obstacles obj) {

        obj.Rotate(Mathf.DegToRad(rng.RandiRange(1, 89)));
    
    }
}
