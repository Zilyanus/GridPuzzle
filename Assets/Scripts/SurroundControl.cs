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
     */

    public LayerMask layerMask;


    public abstract int Control(Vector2 vector2);

    public abstract void ControlSurround();

    private IList<int> _Surrendings;

    private void Awake()
    {
        _Surrendings = new List<int>() { 0, 0, 0, 0 };
    }

    public void SetSurroundAtIndex(int index, int value)
    {
       _Surrendings[index] = value;
    }

    public int GetSurroundAtIndex(int index)
    {
        return _Surrendings[index];
    }

    public Transform GetTransform()
    {
        return transform;
    }
}
    