using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    int CollectableCount;
    int CollectedCount;

    // Start is called before the first frame update
    void Start()
    {
        CollectableScript.OnSpawned.AddListener(CollectableSpawned);
        CollectableScript.OnCollected.AddListener(CollectableCollected);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CollectableSpawned()
    {
        CollectableCount++;
    }

    void CollectableCollected()
    {
        CollectedCount++;

        if (CollectableCount == CollectedCount)
        {
            EndTheGame();
        }
    }

    void EndTheGame()
    {
        Debug.Log("Game Ended");
    }
}
