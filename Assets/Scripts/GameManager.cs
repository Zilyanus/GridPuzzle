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
       
    }

    private void OnEnable()
    {
        CollectableScript.OnSpawned += CollectableSpawned;
        CollectableScript.OnCollected += CollectableCollected;
    }

    private void OnDisable()
    {
        CollectableScript.OnSpawned -= CollectableSpawned;
        CollectableScript.OnCollected -= CollectableCollected;
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
