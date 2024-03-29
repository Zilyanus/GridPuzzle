using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISurroundable
{
    int Control(Vector2 vector2);

    void ControlSurround();

    void SetSurroundAtIndex(int index, int value);

    int GetSurroundAtIndex(int index);

    Transform ChangeParent(int index);

    Transform GetTransform();
}
