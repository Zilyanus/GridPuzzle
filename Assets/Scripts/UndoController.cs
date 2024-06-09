using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UndoController : MonoBehaviour
{
    [SerializeField] Button UndoButton;
    [SerializeField] Button RedoButton;

    public static event Action OnUndoButtonPressed;
    public static event Action OnRedoButtonPressed;

    [SerializeField] CommandInvoker CommandInvoker;

    private void Update()
    {
        UndoButton.transform.GetChild(0).GetComponent<Image>().color = CommandInvoker._commandList.Count > 0 ? Color.white : new Color(1, 1, 1, 0.5f);
        RedoButton.transform.GetChild(0).GetComponent<Image>().color = CommandInvoker._undoCommandList.Count > 0 ? Color.white : new Color(1, 1, 1, 0.5f);
    }

    public void UndoButtonPressed()
    {
        OnUndoButtonPressed.Invoke();
    }

    public void RedoButtonPressed()
    {
        OnRedoButtonPressed.Invoke();
    }
}
