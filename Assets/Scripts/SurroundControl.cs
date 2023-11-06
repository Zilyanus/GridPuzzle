using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SurroundControl : MonoBehaviour
{
    /*
     * -1: Out of level
     * 0: Empty
     * 1: Grid
     * 2: Wall
     */

    public List<int> Surrounding = new List<int>() {0,0,0,0 };
    public LayerMask layerMask;

    public abstract int Control(Vector2 vector2);

    public abstract void ControlSurround();
}
