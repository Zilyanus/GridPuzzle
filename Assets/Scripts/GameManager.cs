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
    [SerializeField] TextMeshProUGUI StartLevelText;
    [SerializeField] TextMeshProUGUI StartCollectableText;
    [SerializeField] TextMeshProUGUI StartMoveText;

    [SerializeField] float TotalMoveCount;
    float MoveCount;

    [SerializeField] EndPanelScript EndPanel;

    public static UnityEvent OnGameFinished = new UnityEvent();
    // Start is called before the first frame update
    void Start()
    {
        LevelText.text = "Level " + SceneManager.GetActiveScene().buildIndex;
        StartLevelText.text = "-" + LevelText.text + "-";
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

        StartCollectableText.text = CollectableCount.ToString();
        StartMoveText.text = TotalMoveCount.ToString();
    }

    void OnPlayerMoved()
    {
        MoveCount++;

        if (MoveCount > TotalMoveCount)
        {
            EndPanel.EndLevel(0);
            OnGameFinished.Invoke();
        }
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
        OnGameFinished.Invoke();
        EndPanel.EndLevel(3);
        Debug.Log("Game Ended");
    }
}
