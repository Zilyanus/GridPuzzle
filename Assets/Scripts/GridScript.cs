using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class GridScript : SurroundControl
{
    public static UnityEvent<GameObject,GameObject> OnCombineGrids = new UnityEvent<GameObject,GameObject>();     
    // Start is called before the first frame update
    void Start()
    {
        ControlSurround();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override int Control(Vector2 vector2)
    {
        Vector2 Pos = transform.position;
        RaycastHit2D hit = Physics2D.Raycast(Pos + vector2 * 4 / 10, vector2, 0.5f, layerMask);
        if (hit && hit.collider.gameObject.layer == 3 && hit.collider.gameObject != gameObject)
        {
            OnCombineGrids.Invoke(gameObject, hit.collider.gameObject);
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
        SetSurroundAtIndex(0, Control(transform.up));
        SetSurroundAtIndex(1, Control(-transform.up));
        SetSurroundAtIndex(2, Control(transform.right));
        SetSurroundAtIndex(3, Control(-transform.right));
    }

    public GridParent TransformToGridParent()
    {
        GameObject Parent = new GameObject();
        Parent.name = "GridParent";
        GridParent gridParent = Parent.AddComponent<GridParent>();

        gameObject.transform.parent = Parent.transform;
        gridParent.ControlChilds();

        return gridParent;
    }

    public override Transform ChangeParent(int index)
    {
        return null;
    }
}
