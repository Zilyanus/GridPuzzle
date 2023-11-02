using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance;

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
        if (g1.transform.parent != null)
        {
            GameObject OldParent = g2.transform.parent.gameObject;

            g2.transform.parent = g1.transform.parent;

            if (OldParent != null)
                Destroy(OldParent);
        }
        else if (g2.transform.parent != null)
        {
            GameObject OldParent = g1.transform.parent.gameObject;

            g1.transform.parent = g2.transform.parent;

            if (OldParent != null)
                Destroy(OldParent);
        }
        else 
        {
            GameObject Parent = new GameObject();

            g1.transform.parent = Parent.transform;
            g2.transform.parent = Parent.transform;
        }
    }
}
