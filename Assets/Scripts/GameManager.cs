using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using System;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    int CollectableCount;
    int CollectedCount;

    [SerializeField] TextMeshProUGUI CollectableText;
    [SerializeField] TextMeshProUGUI MoveText;
    [SerializeField] TextMeshProUGUI LevelText;

    [SerializeField] float TotalMoveCount;
    float MoveCount;

    public static event Action OnGameCompleted;
    // Start is called before the first frame update
    void Start()
    {
        LevelText.text = "Level " + SceneManager.GetActiveScene().buildIndex;
    }

    private void OnEnable()
    {
        CollectableScript.OnSpawned += CollectableSpawned;
        CollectableScript.OnCollected += CollectableCollected;

        PlayerMovement.OnMoved += OnPlayerMoved;
    }

    private void OnDisable()
    {
        CollectableScript.OnSpawned -= CollectableSpawned;
        CollectableScript.OnCollected -= CollectableCollected;

        PlayerMovement.OnMoved -= OnPlayerMoved;
    }

    // Update is called once per frame
    void Update()
    {
        CollectableText.text = CollectedCount + "/" + CollectableCount;
        MoveText.text = (TotalMoveCount - MoveCount > 0 ? TotalMoveCount - MoveCount : 0).ToString();
    }

    void OnPlayerMoved()
    {
        MoveCount++;
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
        OnGameCompleted.Invoke();
        Debug.Log("Game Ended");
    }
}
