using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSurrounding : SurroundControl<Vector2>
{
    public override int Control(Vector2 vector2)
    {
        Vector2 Pos = transform.position;
        RaycastHit2D hit = Physics2D.Raycast(Pos + vector2 * 4 / 10, vector2, 0.5f, layerMask);
        if (hit && hit.collider.gameObject.layer == 3 && hit.collider.gameObject != gameObject)
        {
            return 1;
        }
        else if (hit && hit.collider.gameObject.layer == 7)
        {
            return 2;
        }
        else if (hit && hit.collider.gameObject.layer == 6)
        {
            return 0;
        }
        else
        {
            return -1;
        }
    }

    public override void ControlSurround()
    {
        Surrounding[0] = Control(transform.up);
        Surrounding[1] = Control(-transform.up);
        Surrounding[2] = Control(transform.right);
        Surrounding[3] = Control(-transform.right);
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ControlSurround();
    }
}
