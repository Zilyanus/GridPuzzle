using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public Vector3 playerPosition;
    public List<BoxSaveData> boxData;

    public SaveData()
    {
        playerPosition = new Vector3(0f, 0f, 0f);
        boxData = new List<BoxSaveData>();
    }
}

[System.Serializable]
public class BoxSaveData
{
    public Vector3 position;
    public int parentIndex;

    public BoxSaveData(Vector3 position, int parentIndex)
    {
        this.position = position;
        this.parentIndex = parentIndex;
    }
}