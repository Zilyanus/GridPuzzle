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

    public override int Control(Vector2 vector2)
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
        SetSurroundAtIndex(0, Control(transform.up));
        SetSurroundAtIndex(1, Control(-transform.up));
        SetSurroundAtIndex(2, Control(transform.right));
        SetSurroundAtIndex(3, Control(-transform.right));
    }

    public override Transform ChangeParent(int index)
    {
        return null;
    }
}
