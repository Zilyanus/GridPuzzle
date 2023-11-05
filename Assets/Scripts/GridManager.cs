using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance;

    [SerializeField] GameObject SubGrid;
    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CombineGrids(GameObject g1,GameObject g2)
    {       
        GridParent gridParent = null;
        if (g1.transform.parent != null && g1.transform.parent != g2.transform.parent)
        {
            Transform OldParent = g2.transform.parent;

            g2.transform.parent = g1.transform.parent;

            if (OldParent != null)
                Destroy(OldParent.gameObject);

            gridParent = g1.GetComponentInParent<GridParent>();
        }
        else if (g2.transform.parent != null && g1.transform.parent != g2.transform.parent)
        {
            Transform OldParent = g1.transform.parent;

            g1.transform.parent = g2.transform.parent;

            if (OldParent != null)
                Destroy(OldParent.gameObject);

            gridParent = g2.GetComponentInParent<GridParent>();
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
        
        SpawnSubGrid(g1, g2,gridParent.transform);

        if (gridParent != null)
            gridParent.ControlChilds();

    }

    void SpawnSubGrid(GameObject g1,GameObject g2,Transform GridParent)
    {
        Debug.Log("SpawnedSubGird");
        Vector3 Pos = (g1.transform.position + g2.transform.position)/2;

        GameObject NewSubGrid = Instantiate<GameObject>(SubGrid, GridParent);
        NewSubGrid.transform.position = Pos;
    }

    public GridParent CreateParent(GameObject gameObject)
    {
        GameObject Parent = new GameObject();
        Parent.name = "GridParent";
        GridParent gridParent = Parent.AddComponent<GridParent>();

        gameObject.transform.parent = Parent.transform;
        gridParent.ControlChilds();

        return gridParent;
    }
}
