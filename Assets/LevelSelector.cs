using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelSelector : MonoBehaviour
{
    [SerializeField] LineRenderer lineRenderer;

    int LevelIndex;
    int StarValue;
    [SerializeField] TextMeshProUGUI LevelText;

    bool isLocked;
    // Start is called before the first frame update
    void Start()
    {        
        StarValue = ES3.Load("Level " + LevelIndex, -1);
        isLocked = ES3.Load("LastLevel",1) >= LevelIndex ? false : true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetData(int levelIndex, Vector2 Pos)
    {
        LevelIndex = levelIndex;
        lineRenderer.SetPosition(1, transform.InverseTransformPoint(Pos));
        LevelText.text = (LevelIndex + 1).ToString();
    }

    private void OnMouseEnter()
    {
        
    }

    private void OnMouseExit()
    {
        
    }

    private void OnMouseUp()
    {
        
    }
}
