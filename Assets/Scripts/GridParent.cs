using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using ZilyanusLib.Audio;

public class GridParent : SurroundControl
{
    public List<ISurroundable> Grids = new List<ISurroundable>();

    public static event Action<GridParent> OnSpawned;
    public int GridParentdIndex;

    [SerializeField] int ChildCount;
    // Start is called before the first frame update
    void Start()
    {
        ControlSurround();
    }

    public void RegainIndex()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ChildCount = 0;
        for (int i = 0; i < Grids.Count; i++)
        {
            if (Grids[i] != null)
            {
                ChildCount++;
            }
        }
    }

    public override void ControlSurround()
    {
        SetSurroundAtIndex(0, Control(new Vector2(0, 0),0));
        SetSurroundAtIndex(1, Control(new Vector2(1, 0),1));
        SetSurroundAtIndex(2, Control(new Vector2(2, 0),2));
        SetSurroundAtIndex(3, Control(new Vector2(3, 0),3));
    }

    public override int Control(Vector2 vector2, int index)
    {
        SetPuzzleGridAtIndex(index, null);
        int SurroundInt = 0;
        for (int i = 0; i < Grids.Count; i++)
        {
            Grids[i].ControlSurround();
            if (Grids[i].GetSurroundAtIndex(index) == 4 && Grids[i].GetPuzzleGridAtIndex(index) != null)
            {
                //Debug.Log(i + " " + Grids[i].GetPuzzleGridAtIndex(index));
                SetPuzzleGridAtIndex(index,Grids[i].GetPuzzleGridAtIndex(index)); 
                SurroundInt = 4;
            }
            else if (Grids[i].GetSurroundAtIndex(index) == 2)
            {
                SurroundInt = 2;
            }   
            else if (Grids[i].GetSurroundAtIndex(index) == -1 && SurroundInt != 2)
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

    public void CombineFeel()
    {
        AudioClass.PlayAudio("MergeSound",1);

        for (int i = 0; i < Grids.Count; i++)
        {
            if (Grids[i].GetTransform().GetComponent<GridScript>() != null)
                Grids[i].GetTransform().DOScale(1.3f, 0.15f).SetLoops(2, LoopType.Yoyo);
        }
    }
    public override Transform ChangeParent(int index)
    {
        return null;
    }
}
