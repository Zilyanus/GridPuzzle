using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using System;
using UnityEngine.SceneManagement;
using DG.Tweening;
using ZilyanusLib.Audio;

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
    public static event Action OnLastFishAted;
    public static event Action OnFishAted;

    [SerializeField] StarSliderScript StarSlider;

    int LevelCount;
    // Start is called before the first frame update
    void Start()
    {
        LevelCount = SceneManager.GetActiveScene().buildIndex -2;
        LevelText.text = "Level " + (SceneManager.GetActiveScene().buildIndex -1);
        StartLevelText.text = "-" + LevelText.text + "-";

        MoveCountFor2Star = TotalMoveCount + (int)(TotalMoveCount * 25/100);
        MoveCountFor1Star = TotalMoveCount + (int)(TotalMoveCount * 50/100);
        MoveCountFor0Star = TotalMoveCount + (int)(TotalMoveCount * 75 / 100);

        StarSlider.GetStarValues(TotalMoveCount, MoveCountFor2Star, MoveCountFor1Star, MoveCountFor0Star);
    }

    private void OnEnable()
    {
        CollectableScript.OnSpawned += CollectableSpawned;
        EatCommand.OnCollected += CollectableCollected;

        EatCommand.OnReturned += CollectableReturned;

        PlayerMovement.OnMoved += OnPlayerMoved;
    }

    private void OnDisable()
    {
        CollectableScript.OnSpawned -= CollectableSpawned;
        EatCommand.OnCollected -= CollectableCollected;

        EatCommand.OnReturned -= CollectableReturned;

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

        if (Input.GetKeyDown(KeyCode.T))
        {
            EndPanel.EndLevel(3, LevelCount);
        }
    }

    void OnPlayerMoved(int value)
    {
        MoveCount+=value;

        if (MoveCount > MoveCountFor0Star)
        {
            EndPanel.EndLevel(0, LevelCount);
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
        Debug.Log("Collected");
        CollectedCount++;

        if (CollectableCount == CollectedCount)
        {
            OnLastFishAted.Invoke();
            DOVirtual.DelayedCall(2, () => { EndTheGame(); });
        }
        else
        {
            OnFishAted.Invoke();
        }
    }

    void CollectableReturned()
    {
        CollectedCount--;
    }

    void EndTheGame()
    {
        OnGameFinished.Invoke();
        EndPanel.EndLevel(StarSlider.GetEndValues(), LevelCount);
        Debug.Log("Game Ended");
    }
}
