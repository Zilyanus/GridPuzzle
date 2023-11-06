using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GridParent : SurroundControl
{
    [SerializeField] List<SurroundControl> Grids = new List<SurroundControl>();

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
        Surrounding[0] = Control(new Vector2(0, 0));
        Surrounding[1] = Control(new Vector2(1, 0));
        Surrounding[2] = Control(new Vector2(2, 0));
        Surrounding[3] = Control(new Vector2(3, 0));
    }

    public override int Control(Vector2 vector2)
    {
        int SurroundInt = 0;
        for (int i = 0; i < Grids.Count; i++)
        {
            Grids[i].ControlSurround();
            if (Grids[i].Surrounding[(int)vector2.x] == 2)
            {
                SurroundInt = 2;
            }
            else if (Grids[i].Surrounding[(int)vector2.x] == -1 && SurroundInt != 2)
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
            if (transform.GetChild(i).GetComponent<SurroundControl>() != null)
                Grids.Add(transform.GetChild(i).GetComponent<SurroundControl>());
        }
    }
}
