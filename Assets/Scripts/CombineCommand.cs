using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombineCommand : ICommand
{
    GameObject g1, g2, SubGrid;

    public CombineCommand(GameObject Object1, GameObject Object2, GameObject SubGridObject)
    {
        g1 = Object1;
        g2 = Object2;
        SubGrid = SubGridObject;
    }

    public void Execute()
    {
        //Refactor Edilecek
        GridParent gridParent = null;
        if (g1.transform.rotation != Quaternion.Euler(0,0,0) && g1.transform.parent != null && g1.transform.parent != g2.transform.parent)
        {
            Transform OldParent = g2.transform.parent;

            List<Transform> Childs = new();
            for (int i = 0; i < OldParent.childCount; i++)
            {
                Childs.Add(OldParent.GetChild(i));
            }
            for (int i = 0; i < Childs.Count; i++)
            {
                Childs[i].parent = g1.transform.parent;
            }

            if (OldParent != null)
                Object.Destroy(OldParent.gameObject);
            
            gridParent = g1.GetComponentInParent<GridParent>();
            gridParent.RegainIndex();
        }
        else if (g2.transform.rotation != Quaternion.Euler(0, 0, 0) && g2.transform.parent != null && g1.transform.parent != g2.transform.parent)
        {
            Transform OldParent = g1.transform.parent;

            List<Transform> Childs = new();
            for (int i = 0; i < OldParent.childCount; i++)
            {
                Childs.Add(OldParent.GetChild(i));
            }
            for (int i = 0; i < Childs.Count; i++)
            {
                Childs[i].parent = g2.transform.parent;
            }

            if (OldParent != null)
                Object.Destroy(OldParent.gameObject);

            gridParent = g2.GetComponentInParent<GridParent>();
            gridParent.RegainIndex();
        }
        else if (g1.transform.parent != null && g1.transform.parent != g2.transform.parent)
        {
            Transform OldParent = g2.transform.parent;

            List<Transform> Childs = new();
            for (int i = 0; i < OldParent.childCount; i++)
            {
                Childs.Add(OldParent.GetChild(i));
            }
            for (int i = 0; i < Childs.Count; i++)
            {
                Childs[i].parent = g1.transform.parent;
            }

            if (OldParent != null)
                Object.Destroy(OldParent.gameObject);

            gridParent = g1.GetComponentInParent<GridParent>();
            gridParent.RegainIndex();
        }
        else if (g2.transform.parent != null && g1.transform.parent != g2.transform.parent)
        {
            Transform OldParent = g1.transform.parent;

            List<Transform> Childs = new();
            for (int i = 0; i < OldParent.childCount; i++)
            {
                Childs.Add(OldParent.GetChild(i));
            }
            for (int i = 0; i < Childs.Count; i++)
            {
                Childs[i].parent = g2.transform.parent;
            }

            if (OldParent != null)
                Object.Destroy(OldParent.gameObject);

            gridParent = g2.GetComponentInParent<GridParent>();
            gridParent.RegainIndex();
        }
        else if (g1.transform.parent == null && g2.transform.parent == null)
        {
            GameObject Parent = new GameObject();
            Parent.name = "GridParent";
            gridParent = Parent.AddComponent<GridParent>();

            g1.transform.parent = Parent.transform;
            g2.transform.parent = Parent.transform;
        }

        if (gridParent != null)
        {
            SpawnSubGrid(g1, g2, gridParent.transform);
            gridParent.ControlChilds();
            gridParent.CombineFeel();
        }
    }

    public bool Undo()
    {
        return true;
    }

    public void Redo()
    {

    }

    void SpawnSubGrid(GameObject g1, GameObject g2, Transform GridParent)
    {
        Vector3 Pos = (g1.transform.position + g2.transform.position) / 2;

        GameObject NewSubGrid = Object.Instantiate<GameObject>(SubGrid, GridParent);
        NewSubGrid.transform.position = Pos;
    }
    public float ReturnExecutionTime()
    {
        return 0f;
    }
}
