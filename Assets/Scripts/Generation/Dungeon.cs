using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dungeon
{
    List<DungeonFloor> floors;
    int numberOfFloors;
    public int FloorNumber { get { return numberOfFloors; } }
    int floorSize;

    public Dungeon(int floorSize, int numberOfFloors)
    {
        this.floorSize = floorSize;
        this.numberOfFloors = numberOfFloors;
    }

    public void GenerateDungeon()
    {
        floors = new List<DungeonFloor>();
        for (int i = 0; i < numberOfFloors; i++)
        {
            DungeonFloor newFloor = new DungeonFloor(floorSize);
            newFloor.GenerateFloor(9, 24, 9, 24);
            floors.Add(newFloor);
            newFloor.SetupMonsters();
        }

        floors[0].SetupStart();
    }

    public List<DungeonFloor> GetFloors() => floors; 
}
