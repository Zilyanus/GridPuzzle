using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombineCommand : ICommand
{
    GameObject g1, g2, SubGrid;

    GridParent NewParent;
    List<Transform> g1Childs = new();
    List<Transform> g2Childs = new();
    List<Transform> CreatedSubGrids = new();

    public CombineCommand(GameObject Object1, GameObject Object2, GameObject SubGridObject)
    {
        g1 = Object1;
        g2 = Object2;
        SubGrid = SubGridObject;
        NewParent = GetParent();
    }

    public void Execute()
    {
        Combine();
    }

    GridParent GetParent()
    {
        if (g1.GetComponentInParent<GridParent>() == null) 
        {
            g1.GetComponent<GridScript>().TransformToGridParent();
        }

        if (g2.GetComponentInParent<GridParent>() == null)
        {
            g2.GetComponent<GridScript>().TransformToGridParent();
        }
        return g1.GetComponentInParent<GridParent>();
    }

    void Combine()
    {
        if (g1.transform.parent == g2.transform.parent)
            return;

        GridParent gridParent = g1.GetComponentInParent<GridParent>();
        Transform OldParent = g2.transform.parent;

        for (int i = 0; i < g1.transform.parent.childCount; i++)
        {  
            g1Childs.Add(g1.transform.parent.GetChild(i));
        }

        for (int i = 0; i < OldParent.childCount; i++)
        {
            g2Childs.Add(OldParent.GetChild(i));
        }

        for (int i = 0; i < g2Childs.Count; i++)
        {
            g2Childs[i].parent = gridParent.transform;
        }

        Object.Destroy(OldParent.gameObject);

        gridParent.GetComponentInParent<GridParent>().RegainIndex();

        CreatedSubGrids.Add(SpawnSubGrid(g1, g2, gridParent.transform));
        gridParent.ControlChilds();
        gridParent.CombineFeel();
    }

    public void Undo()
    {
        for (int i = 0; i < CreatedSubGrids.Count; i++)
        {
            Object.DestroyImmediate(CreatedSubGrids[i].gameObject);
        }
        CreatedSubGrids.Clear();

        GridParent NewG2 = null;

        for (int i = 0; i < g2Childs.Count; i++)
        {
            if (NewG2 == null)
            {
                NewG2 = g2Childs[i].GetComponent<GridScript>().TransformToGridParent();
                NewG2.isSpawnedFromUndo = true;
            }
            else
                g2Childs[i].parent = NewG2.transform;
        }

        NewParent.ControlChilds();
        NewG2.ControlChilds();
    }

    public void Redo()
    {

    }

    Transform SpawnSubGrid(GameObject g1, GameObject g2, Transform GridParent)
    {
        Vector3 Pos = (g1.transform.position + g2.transform.position) / 2;

        GameObject NewSubGrid = Object.Instantiate<GameObject>(SubGrid, GridParent);
        NewSubGrid.transform.position = Pos;

        return NewSubGrid.transform;
    }
    public float ReturnExecutionTime()
    {
        return 0f;
    }
}
