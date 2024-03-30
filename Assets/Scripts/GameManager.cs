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

    [SerializeField] TextMeshProUGUI EndText;

    [SerializeField] float TotalMoveCount;
    [SerializeField] float MoveCountFor2Star;
    [SerializeField] float MoveCountFor1Star;
    [SerializeField] float MoveCountFor0Star;
    float MoveCount;

    [SerializeField] EndPanelScript EndPanel;

    public static UnityEvent OnGameFinished = new UnityEvent();

    [SerializeField] StarSliderScript StarSlider;
    // Start is called before the first frame update
    void Start()
    {
        LevelText.text = "Level " + SceneManager.GetActiveScene().buildIndex;
        StartLevelText.text = "-" + LevelText.text + "-";

        MoveCountFor2Star = TotalMoveCount + (int)(TotalMoveCount * 25/100);
        MoveCountFor1Star = TotalMoveCount + (int)(TotalMoveCount * 50/100);
        MoveCountFor0Star = TotalMoveCount + (int)(TotalMoveCount * 75 / 100);

        StarSlider.GetStarValues(TotalMoveCount, MoveCountFor2Star, MoveCountFor1Star, MoveCountFor0Star);
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
        MoveText.text = (MoveCountFor0Star - MoveCount > 0 ? MoveCountFor0Star - MoveCount : 0).ToString();

        StartCollectableText.text = CollectableCount.ToString();
        StartMoveText.text = TotalMoveCount.ToString();

        StarSlider.MovedCount = MoveCount;
    }

    void OnPlayerMoved()
    {
        MoveCount++;

        if (MoveCount > MoveCountFor0Star)
        {
            EndPanel.EndLevel(0);
            OnGameFinished.Invoke();
            EndText.text = "-You Lose-";
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
