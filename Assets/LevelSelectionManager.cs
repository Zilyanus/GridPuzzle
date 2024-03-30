using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectionManager : MonoBehaviour
{
    LevelSelector[] levelSelectors;
    // Start is called before the first frame update
    void Start()
    {
        
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
    }
}
