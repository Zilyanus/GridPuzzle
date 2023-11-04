using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GridParent : SurroundControl<int>
{
    [SerializeField] List<GameObject> Grids = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ControlSurround();
    }

    public override void ControlSurround()
    {
        Surrounding[0] = Control(0);
        Surrounding[1] = Control(1);
        Surrounding[2] = Control(2);
        Surrounding[3] = Control(3);
    }

    public override int Control(int index)
    {
        int SurroundInt = 0;
        for (int i = 0; i < Grids.Count; i++)
        {
            if (Grids[i].GetComponent<GridScript>().Surrounding[index] == 2)
            {
                SurroundInt = 2;
            }
            else if (Grids[i].GetComponent<GridScript>().Surrounding[index] == -1 && SurroundInt != 2)
            {
                SurroundInt = -1;
            }
        }

        return SurroundInt;
    }

    public void ControlChilds()
    {
        Grids.Clear();
        
        for (int i = 0; i < transform.childCount; i++)
        {
            Grids.Add(transform.GetChild(i).gameObject);
        }
    }
}
