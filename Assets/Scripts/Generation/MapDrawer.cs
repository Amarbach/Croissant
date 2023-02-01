using System;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum Direction
{
    UP,
    DOWN,
    LEFT,
    RIGHT
}

public class MapDrawer : MonoBehaviour
{
    [SerializeField] private Grid grid;
    [SerializeField] private Tilemap floor;
    [SerializeField] private Tilemap Walls;
    [SerializeField] private Tilemap Characters;
    [SerializeField] private Tilemap Interactable;
    [SerializeField] private RuleTile floorTile;
    [SerializeField] private RuleTile wallTile;
    [SerializeField] private RuleTile doorTile;
    [SerializeField] private RuleTile enemy1;
    [SerializeField] private MovementController playerCharacter;

    Dungeon dungeon;
    Stopwatch stopwatch;

    // Start is called before the first frame update
    void Start()
    {
        dungeon = new Dungeon(50, 5);
        //stopwatch = new Stopwatch();
        //stopwatch.Reset();
        //stopwatch.Start();
        dungeon.GenerateDungeon();
        //stopwatch.Stop();
        //UnityEngine.Debug.Log(stopwatch.ElapsedMilliseconds);
        //UnityEngine.Debug.Log(stopwatch.Elapsed);
        DrawRooms();
    }

    private void DrawRooms()
    {
        int disp = 0;
        foreach (DungeonFloor _floor in dungeon.GetFloors())
        {
            for (int i = 0; i < _floor.Size; i++)
            {
                for (int j = 0; j < _floor.Size; j++)
                {
                    switch (_floor.Map[i, j])
                    {
                        case TileType.WALL:
                            Walls.SetTile(new Vector3Int(j + (int)(disp * 50 * 1.5f), i), wallTile);
                            break;
                        case TileType.DOOR:
                            Interactable.SetTile(new Vector3Int(j + (int)(disp * 50 * 1.5f), i), doorTile);
                            floor.SetTile(new Vector3Int(j + (int)(disp * 50 * 1.5f), i), floorTile);
                            break;
                        case TileType.FLOOR:
                            floor.SetTile(new Vector3Int(j + (int)(disp * 50 * 1.5f), i), floorTile);
                            break;
                        case TileType.PLAYERSTART:
                            floor.SetTile(new Vector3Int(j + (int)(disp * 50 * 1.5f), i), floorTile);
                            playerCharacter.SetPosition(j  + disp * 50 * 1.5f, i );
                            break;
                        case TileType.ENEMY1:
                            floor.SetTile(new Vector3Int(j + (int)(disp * 50 * 1.5f), i), floorTile);
                            Characters.SetTile(new Vector3Int(j + (int)(disp * 50 * 1.5f), i), enemy1);
                            break;
                    }
                }
            }
            disp++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.F1))
        //{
        //    test.GenerateRooms();
        //}
    }

    private void OnDrawGizmos()
    {
        //if (test != null)
        //{
        //    List<BinarySplitRoom> rooms = test.RetriveLeaves();
        //    foreach (var room in rooms)
        //    {
        //        room.DrawGizmo();
        //    }
        //}
    }
}
