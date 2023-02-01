
using UnityEngine;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using UnityEngine.Tilemaps;

public class BinarySplitRoom
{
    private int up, down, left, right, minWidth, maxWidth, minHeight, maxHeight;
    private BinarySplitRoom firstRoom, secondRoom;

    public int Up  { get { return this.up; } }
    public int Down { get { return this.down; } }
    public int Left { get { return this.left; } }
    public int Right { get { return this.right; } }

    public Vector2Int Center { get { return new Vector2Int((this.Right - this.Left) / 2 + this.Left, (this.Up - this.Down) / 2 + this.Down); } }

    public BinarySplitRoom(int up, int down, int left, int right, int minWidth, int maxWidth, int minHeight, int maxHeight)
    {
        this.up = up;
        this.down = down;
        this.left = left;
        this.right = right;
        this.minWidth = minWidth;
        this.maxWidth = maxWidth;
        this.minHeight = minHeight;
        this.maxHeight = maxHeight;

        this.GenerateRooms();

        if (IsLeaf && right - left >= (minWidth/2) && up - down >= (minHeight/2))
        {
            Trim(1);
        }
    }

    public void GenerateRooms()
    {
        List<BinarySplitRoom> rooms = this.Split();
        if (rooms != null)
        {
            firstRoom = rooms[0];
            secondRoom = rooms[1];
        }
        else
        {
            firstRoom = null;
            secondRoom = null;
        }
    }

    private List<BinarySplitRoom> Split()
    {
        if(right - left > maxWidth)
        {
            return this.VerticalSplit();
        } 
        else if (up - down > maxHeight)
        {
            return this.HorizontalSplit();
        }
        else
        {
            int RNG = UnityEngine.Random.Range(0, 150);
            if (RNG >= 125 && (right - left) > minWidth)
            {
                return this.VerticalSplit();
            } 
            else if (RNG >= 100 && up - down > minHeight)
            {
                return this.HorizontalSplit();
            }
            else return null;
        }
    }

    private List<BinarySplitRoom> VerticalSplit()
    {
        List<BinarySplitRoom> ret = new List<BinarySplitRoom>();
        int offset = UnityEngine.Random.Range(1, (right - left) / 4) * (UnityEngine.Random.Range(0, 2) * 2 - 1);
        int boundry = left + ((right - left) / 2) + offset;
        ret.Add(new BinarySplitRoom(up, down, left, boundry, minWidth, maxWidth, minHeight, maxHeight));
        ret.Add(new BinarySplitRoom(up, down, boundry, right, minWidth, maxWidth, minHeight, maxHeight));
        return ret;
    }

    private List<BinarySplitRoom> HorizontalSplit()
    {
        List<BinarySplitRoom> ret = new List<BinarySplitRoom>();
        int offset = UnityEngine.Random.Range(1, (up - down) / 4) * (UnityEngine.Random.Range(0, 2) * 2 - 1);
        int boundry = down + ((up - down) / 2) + offset;
        ret.Add(new BinarySplitRoom(boundry, down, left, right, minWidth, maxWidth, minHeight, maxHeight));
        ret.Add(new BinarySplitRoom(up, boundry, left, right, minWidth, maxWidth, minHeight, maxHeight));
        return ret;
    }

    private void Trim(int value)
    {
        up -= value;
        down += value;
        left += value;
        right -= value;
    }

    public bool IsLeaf => (firstRoom == null && secondRoom == null);
    public bool IsTooSmall => (right - left <= 2 || up - down <= 2);

    public List<BinarySplitRoom> RetriveLeaves()
    {
        List<BinarySplitRoom> ret;
        if (IsLeaf)
        {
            ret = new List<BinarySplitRoom>() { this };
        }
        else
        {
            ret = firstRoom.RetriveLeaves();
            ret.AddRange(secondRoom.RetriveLeaves());
        }
        return ret;
    }
}
