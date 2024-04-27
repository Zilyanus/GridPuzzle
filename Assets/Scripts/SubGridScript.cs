using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubGridScript : SurroundControl
{
    private void Start()
    {
        ControlSurround();
    }

    private void Update()
    {
        ControlSurround();
    }

    public override int Control(Vector2 vector2, int index)
    {
        Vector2 Pos = transform.position;
        RaycastHit2D hit = Physics2D.Raycast(Pos + vector2 * 4 / 10, vector2, 1f, layerMask);
        if (hit && hit.collider.gameObject.layer == 7)
        {
            return 2;
        }
        else
        {
            return 0;
        }
    }

    public override void ControlSurround()
    {
        SetSurroundAtIndex(0, Control(Vector2.up,0));
        SetSurroundAtIndex(1, Control(-Vector2.up,1));
        SetSurroundAtIndex(2, Control(Vector2.right,2));
        SetSurroundAtIndex(3, Control(-Vector2.right,3));
    }

    public override Transform ChangeParent(int index)
    {
        return null;
    }
}
