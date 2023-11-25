using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewindManager : MonoBehaviour
{
    [SerializeField] PlayerInputs InputActions;
    int CurrentMoveIndex = 0;

    private void Awake()
    {
        InputActions = new PlayerInputs();
        InputActions.Player.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        if (InputActions.Player.UndoButton.ReadValue<float>() != 0)
        {
            UndoMove();
        }
        else if (InputActions.Player.RedoButton.ReadValue<float>() != 0)
        {
            RedoMove();
        }
    }

    void UndoMove()
    {
        LoadData(CurrentMoveIndex++);
    }

    void RedoMove()
    {
        LoadData(CurrentMoveIndex--);
    }

    public void SaveData()
    {
        SaveData saveData = new SaveData();
        ES3.Save<SaveData>("SaveSlot"+ CurrentMoveIndex, saveData);
    }

    public SaveData LoadData(int index)
    {
        return ES3.Load<SaveData>("SaveSlot" + index);
    }
}
