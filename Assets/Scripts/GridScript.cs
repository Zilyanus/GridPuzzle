using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class GridScript : SurroundControl
{
    public static UnityEvent<GameObject,GameObject> OnCombineGrids = new UnityEvent<GameObject,GameObject>();

    public List<float> DebugFloats = new List<float>();
    [SerializeField] List<Vector2> DebugFloats2 = new List<Vector2>();

    [SerializeField] GameObject DebugObject;
    // Start is called before the first frame update
    void Start()
    {
        ControlSurround();

        //GameObject DebugObjectt = Instantiate(DebugObject, transform.position, Quaternion.identity);
        //DebugObjectt.GetComponent<DebugObject>().gridScript = this;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public override void ControlSurround()
    {
        SetSurroundAtIndex(0, Control(Vector2.up, 0));
        SetSurroundAtIndex(1, Control(-Vector2.up, 1));
        SetSurroundAtIndex(2, Control(Vector2.right, 2));
        SetSurroundAtIndex(3, Control(-Vector2.right, 3));

        for (int i = 0; i < DebugFloats.Count; i++)
        {
            DebugFloats[i] = GetSurroundAtIndex(i);
        }
    }

    public override int Control(Vector2 vector2, int index)
    {
        SetPuzzleGridAtIndex(index, null);
        Vector2 Pos = transform.position;
        RaycastHit2D hit = Physics2D.Raycast(Pos + vector2 * 4 / 10, vector2, 0.5f, layerMask);

        DebugFloats2[index] = Pos + vector2 * 4 / 10;
        if (hit && hit.collider.gameObject.layer == 3 && hit.collider.gameObject != gameObject)
        {
            if (GetComponentInParent<GridParent>() == null || hit.collider.GetComponentInParent<GridParent>() == null || (hit.collider.GetComponentInParent<GridParent>() != null && GetComponentInParent<GridParent>() != null && GetComponentInParent<GridParent>() != hit.collider.GetComponentInParent<GridParent>()))
                OnCombineGrids.Invoke(gameObject, hit.collider.gameObject);
            return 1;
        }
        else if (hit && hit.collider.gameObject.layer == 11)
        {
            hit.collider.gameObject.GetComponent<PuzzleGrid>().MainObjects[-vector2] = transform;
            SetPuzzleGridAtIndex(index, hit.collider.gameObject.GetComponent<PuzzleGrid>());
            return 4;
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


    public GridParent TransformToGridParent()
    {
        GameObject Parent = new GameObject();
        Parent.name = "GridParent";
        GridParent gridParent = Parent.AddComponent<GridParent>();

        gameObject.transform.parent = Parent.transform;
        gridParent.ControlChilds();

        return gridParent;
    }
}
