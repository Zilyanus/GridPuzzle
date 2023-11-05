using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GridParent : SurroundControl<int>
{
    [SerializeField] List<SurroundControl<Vector2>> Grids = new List<SurroundControl<Vector2>>();

    // Start is called before the first frame update
    void Start()
    {
        ControlSurround();
    }

    // Update is called once per frame
    void Update()
    {
        
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
            Grids[i].ControlSurround();
            if (Grids[i].Surrounding[index] == 2)
            {
                SurroundInt = 2;
            }
            else if (Grids[i].Surrounding[index] == -1 && SurroundInt != 2)
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
            if (transform.GetChild(i).GetComponent<SurroundControl<Vector2>>() != null)
                Grids.Add(transform.GetChild(i).GetComponent<SurroundControl<Vector2>>());
        }
    }
}
