using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class LevelSelectionManager : MonoBehaviour
{
    LevelSelector[] levelSelectors;
    public int TotalStar = 0;
    [SerializeField] Transform LastCamPos;
    // Start is called before the first frame update
    void Awake()
    {
        ArrangeLevels();

        for (int i = 0; i < ES3.Load("LastLevel", 0); i++)
        {
            TotalStar += ES3.Load<int>("Level " + i, -1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [Button]
    void ArrangeLevels()
    {
        levelSelectors = GetComponentsInChildren<LevelSelector>();

        for (int i = 0; i < levelSelectors.Length; i++)
        {
            levelSelectors[i].GetData(i, i + 1 < levelSelectors.Length ? levelSelectors[i + 1].transform.position : levelSelectors[i].transform.position);
        }

        LastCamPos.position = levelSelectors[levelSelectors.Length -1].transform.position + new Vector3(-2,0,0);
    }

    public Vector3 GetPos(int level)
    {
        return levelSelectors[level].transform.position;
    }
}
