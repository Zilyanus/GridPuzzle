using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SurroundControl : MonoBehaviour, ISurroundable
{
    /*
     * -1: Out of level
     * 0: Empty
     * 1: Grid
     * 2: Wall
     * 3: InWall
     * 4: PuzzleBlock
     */

    public LayerMask layerMask;


    public abstract int Control(Vector2 vector2, int index);

    public abstract void ControlSurround();

    private IList<int> _Surrendings;
    private List<PuzzleGrid> puzzleGrids; 

    private void Awake()
    {
        _Surrendings = new List<int>() { 0, 0, 0, 0 };
        puzzleGrids = new List<PuzzleGrid> { null, null, null, null };
    }

    public void SetSurroundAtIndex(int index, int value)
    {
       _Surrendings[index] = value;
    }

    public int GetSurroundAtIndex(int index)
    {
        return _Surrendings[index];
    }

    public void SetPuzzleGridAtIndex(int index, PuzzleGrid value)
    {
        puzzleGrids[index] = value;
    }

    public PuzzleGrid GetPuzzleGridAtIndex(int index)
    {
        return puzzleGrids[index];
    }

    public Transform GetTransform()
    {
        return transform;
    }

    public abstract Transform ChangeParent(int index);
}
    