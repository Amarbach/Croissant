using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;

public enum TileType
{
    WALL,
    FLOOR,
    DOOR,
    PLAYERSTART,
    STAIRSUP,
    STAIRSDOWN,
    ENEMY1
}
public class DungeonFloor
{
    List<BinarySplitRoom> rooms;
    TileType[,] obstacleMap;
    int floorSize;

    public DungeonFloor(int floorSize)
    {
        this.floorSize = floorSize;
    }

    public void GenerateFloor(int roomMinWidth, int roomMaxWidth, int roomMinHeight, int roomMaxHeight)
    {
        rooms = new BinarySplitRoom(floorSize, 0, 0, floorSize, roomMinWidth, roomMaxWidth, roomMinHeight, roomMaxHeight).RetriveLeaves();
        obstacleMap = new TileType[floorSize, floorSize];
        for (int i = 0; i < floorSize; i++)
        {
            for (int j = 0; j < floorSize; j++)
            {
                obstacleMap[i, j] = TileType.WALL;
            }
        }

        foreach (BinarySplitRoom room in rooms)
        {
            for (int i = room.Down + 1; i < room.Up; i++)
            {
                for (int j = room.Left + 1; j < room.Right; j++)
                {
                    obstacleMap[i, j] = TileType.FLOOR;
                }
            }
            int closestDist = int.MaxValue;
            BinarySplitRoom closestRoom = null;
            List<BinarySplitRoom> connectedRooms = new List<BinarySplitRoom>();
            foreach (BinarySplitRoom room2 in rooms)
            {
                if (room2 != room && !connectedRooms.Contains(room2))
                {
                    var dist = Math.Abs(room.Center.x - room2.Center.x) + Math.Abs(room.Center.y - room.Center.y);
                    if (dist < closestDist && dist != 0)
                    {
                        closestDist = dist;
                        closestRoom = room2;
                    }
                }
            }

            for (int i = 0; i <= Math.Abs(closestRoom.Center.x - room.Center.x); i++)
            {
                var x = room.Center.x + i * Math.Sign(closestRoom.Center.x - room.Center.x);
                var y = room.Center.y;
                if (x == room.Left || x == room.Right) obstacleMap[y, x] = TileType.DOOR;
                else obstacleMap[y, x] = TileType.FLOOR;
            }
            for (int i = 0; i <= Math.Abs(closestRoom.Center.y - room.Center.y); i++)
            {
                var y = room.Center.y + i * Math.Sign(closestRoom.Center.y - room.Center.y);
                var x = closestRoom.Center.x;
                if (y == closestRoom.Up || y == closestRoom.Down) obstacleMap[y, x] = TileType.DOOR;
                else obstacleMap[y, x] = TileType.FLOOR;
            }
        }
    }

    public void SetupStart()
    {
        var room = rooms[UnityEngine.Random.Range(0, rooms.Count)];
        var y = UnityEngine.Random.Range(room.Down + 1, room.Up);
        var x = UnityEngine.Random.Range(room.Left + 1, room.Right);
        obstacleMap[y, x] = TileType.PLAYERSTART;
    }

    public void SetupMonsters()
    {
        foreach(BinarySplitRoom room in rooms)
        {
            var P = (room.Right - room.Left) * (room.Up - room.Down);
            int numEnemies = UnityEngine.Random.Range(1, (int)Mathf.Sqrt(P));
            for(int i = 0; i < numEnemies; i++)
            {
                var y = UnityEngine.Random.Range(room.Down + 1, room.Up);
                var x = UnityEngine.Random.Range(room.Left + 1, room.Right);
                obstacleMap[y, x] = TileType.ENEMY1;
            }
        }
    }

    public TileType[,] Map { get { return obstacleMap; } }
    public int Size { get { return floorSize; } }
}
