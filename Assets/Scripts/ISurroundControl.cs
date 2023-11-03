using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ISurroundControl<T> : MonoBehaviour
{
    public List<int> Surrounding = new List<int>() {0,0,0,0 };
    public LayerMask layerMask;

    public abstract int Control(T t);

    public abstract void ControlSurround();
}
