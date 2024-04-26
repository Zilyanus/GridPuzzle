using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] GameObject SubGrid;

    public static event Action<ICommand> CombineCommandAction;

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
        CombineCommand command = new CombineCommand(g1,g2,SubGrid);
        CombineCommandAction(command);      
    }
}
