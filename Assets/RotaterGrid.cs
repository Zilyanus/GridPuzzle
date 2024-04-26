using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotaterGrid : PuzzleGrid
{
    public override void CreateCommand()
    {
        Command = new RotateCommand(MainObject);
    }
}
