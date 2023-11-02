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
        if (g1.transform.parent != null && g1.transform.parent != g2.transform.parent)
        {
            Debug.Log("a");
            Transform OldParent = g2.transform.parent;

            g2.transform.parent = g1.transform.parent;

            if (OldParent != null)
                Destroy(OldParent.gameObject);
        }
        else if (g2.transform.parent != null && g1.transform.parent != g2.transform.parent)
        {
            Debug.Log("b");
            Transform OldParent = g1.transform.parent;

            g1.transform.parent = g2.transform.parent;

            if (OldParent != null)
                Destroy(OldParent.gameObject);
        }
        else if (g1.transform.parent == null && g2.transform.parent == null) 
        {
            Debug.Log("c");
            GameObject Parent = new GameObject();

            g1.transform.parent = Parent.transform;
            g2.transform.parent = Parent.transform;
        }
    }
}
