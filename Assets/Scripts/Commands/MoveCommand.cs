using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCommand : ICommand
{
    PlayerMovement _movement;
    Vector3 _dir;
    Transform _transform;

    public MoveCommand(PlayerMovement movement, Vector3 Dir, Transform transform)
    {
        _movement = movement;
        _dir = Dir;
        _transform = transform;
    }

    public void Execute()
    {
        _movement.Move(_dir, _transform);
    }

    public void Undo()
    {
        _movement.Move(-_dir, _transform, -1);
    }

    public void Redo()
    {
        _movement.Move(_dir, _transform);
    }

    public float ReturnExecutionTime()
    {
        return 0.332f;
    }
}
