using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISurroundable
{
    int Control(Vector2 vector2, int index);

    void ControlSurround();

    void SetSurroundAtIndex(int index, int value);

    int GetSurroundAtIndex(int index);

    void SetPuzzleGridAtIndex(int index, PuzzleGrid puzzleGrid);
    PuzzleGrid GetPuzzleGridAtIndex(int index);

    Transform GetTransform();
}
