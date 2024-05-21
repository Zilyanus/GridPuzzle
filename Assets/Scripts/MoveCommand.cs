using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCommand : ICommand
{
    PlayerMovement _movement;
    Vector3 _dir;
    Transform _transform;
    List<ISurroundable> ObjectsToMove = new List<ISurroundable>();

    int LastParentIndex;
    float _time;

    public MoveCommand(PlayerMovement movement, Vector3 Dir, Transform transform)
    {
        _movement = movement;
        _dir = Dir;
        _transform = transform;
        ObjectsToMove = transform.GetComponentInParent<GridParent>().Grids;
        LastParentIndex = transform.GetComponentInParent<GridParent>().GridParentdIndex;
        _time = Time.time;
    }

    public void Execute()
    {
        _movement.Move(_dir, _transform);
    }

    public bool Undo()
    {
        if (_transform.GetComponentInParent<GridParent>().GridParentdIndex == LastParentIndex || _transform.GetComponentInParent<GridParent>().GridParentdIndex == 0)
        {
            Debug.Log("True Undo: " + _transform.GetComponentInParent<GridParent>().GridParentdIndex + " == " + LastParentIndex + " Time: " + _time);
            _movement.Move(-_dir, _transform);
            return true;
        }
        else
        {
            for (int i = 0; i < ObjectsToMove.Count; i++)
            {
                Transform newTransform = ObjectsToMove[i].ChangeParent(LastParentIndex);
                _transform = newTransform != null ? newTransform : _transform;
            }
            _movement.Move(-_dir, _transform);
            Debug.Log("False Undo: " + _transform.GetComponentInParent<GridParent>().GridParentdIndex + " == " + LastParentIndex + " Time: " + _time);
        }
        return false;
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
