﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum VoxelType { Empty, Connection, Block };
public class Voxel
{
    public Vector3Int Index;
    public VoxelType Type;
    public Vector3Int Orientation;
    public Pattern ParentPattern { get; private set; }
    public Block ParentBlock { get; set; }
    public string Name;
    public GameObject Go;
    public List<Vector3Int> WalkableFaces = new List<Vector3Int>();
    public List<Face> Faces = new List<Face>(6);

    /// <summary>
    /// Instantiate a voxel.
    /// </summary>
    /// <param name="x">X index of the voxel</param>
    /// <param name="y">Y index of the voxel</param>
    /// <param name="z">Z index of the voxel</param>
    /// /// <param name="type">The type of the voxel (Empty, Connection, Block) </param>
    public Voxel(int x, int y, int z, VoxelType type)
    {
        Index = new Vector3Int(x, y, z);
        Type = type;
    }

    /// <summary>
    /// Instantiate a block or connection voxel
    /// </summary>
    /// <param name="x">X index of the voxel</param>
    /// <param name="y">Y index of the voxel</param>
    /// <param name="z">Z index of the voxel</param>
    /// <param name="type">The type of the voxel (Empty, Connection, Block) </param>
    /// <param name="parentPattern">Parrent Pattern of the connection</param>
    /// <param name="orientation">Orientation of the block or connection</param>
    public Voxel(int x, int y, int z, VoxelType type, Vector3Int orientation, Pattern parentPattern)
    {
        Index = new Vector3Int(x, y, z);
        Type = type;
        Orientation = orientation;
        ParentPattern = parentPattern;
        Name = $"x {x}, y {y}, z {z}";
    }

    /// <summary>
    /// Instantiate a block voxel with walkable faces assigned
    /// </summary>
    /// <param name="x">X index of the voxel</param>
    /// <param name="y">Y index of the voxel</param>
    /// <param name="z">Z index of the voxel</param>
    /// <param name="type">The type of the voxel (Empty, Connection, Block) </param>
    /// <param name="parentPattern">Parrent Pattern of the connection</param>
    /// <param name="orientation">Orientation of the block</param>
    /// <param name="walkableFaces">The direction vectors between the center of the block and the climable faces eg. (1,0,0)</param>
    public Voxel(int x, int y, int z, VoxelType type, Vector3Int orientation, Pattern parentPattern, List<Vector3Int> walkableFaces)
        : this(x, y, z, type, orientation, parentPattern)
    {
        WalkableFaces = walkableFaces;
    }

    public void Copy(Voxel orig)
    {
        this.Type = orig.Type;
        this.Orientation = orig.Orientation;
        if (this.Type != VoxelType.Block)
        {
            this.ParentBlock = orig.ParentBlock;
        }
        this.ParentPattern = orig.ParentPattern;
        this.Name = orig.Name;
        this.WalkableFaces = orig.WalkableFaces;
    }

    public Voxel ShallowClone()
    {
        return MemberwiseClone() as Voxel;
    }

    public void DestroyGoVoxel()
    {
        Debug.Log("The voxel went to the dark side!");
        GameObject.Destroy(Go);
    }
}