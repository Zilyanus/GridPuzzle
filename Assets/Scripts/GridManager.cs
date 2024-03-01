using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] GameObject SubGrid;

    private void OnEnable()
    {
        GridScript.OnCombineGrids.AddListener(CombineGrids);
    }

    private void OnDisable()
    {
        GridScript.OnCombineGrids.RemoveListener(CombineGrids);
    }

    void CombineGrids(GameObject g1,GameObject g2)
    {       
        GridParent gridParent = null;
        if (g1.transform.parent != null && g1.transform.parent != g2.transform.parent)
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
                Destroy(OldParent.gameObject);

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
                Destroy(OldParent.gameObject);

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
        else
        {
            return;
        }
        
        if (gridParent != null)
        {
            SpawnSubGrid(g1, g2,gridParent.transform);
            gridParent.ControlChilds();
            gridParent.CombineFeel();
        }
      
    }

    void SpawnSubGrid(GameObject g1,GameObject g2,Transform GridParent)
    {
        Vector3 Pos = (g1.transform.position + g2.transform.position)/2;

        GameObject NewSubGrid = Instantiate<GameObject>(SubGrid, GridParent);
        NewSubGrid.transform.position = Pos;
    }
}
