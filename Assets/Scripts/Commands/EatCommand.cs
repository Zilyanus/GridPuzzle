using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class EatCommand : ICommand
{
    Vector3 Pos;
    Transform transform;
    CollectableScript collectableScript;

    public static event Action OnCollected;
    public static event Action OnReturned;
    public EatCommand(Transform _transform, CollectableScript _collectableScript)
    {
        transform = _transform; 
        Pos = transform.position;
        collectableScript = _collectableScript;
    }

    public void Execute()
    {
        EatFish();
    }

    public void Undo()
    {
        SpawnFish();
    }

    public void Redo()
    {

    }

    void EatFish()
    {
        OnCollected.Invoke();
        collectableScript.isCollected = true;
        DOTween.Kill(transform);
        transform.DOScale(1.5f, 0.25f).OnComplete(() => transform.DOScale(0f, 0.3f).OnComplete(() => collectableScript.gameObject.SetActive(false)));
    }

    void SpawnFish()
    {
        collectableScript.transform.position = Pos;
        collectableScript.gameObject.SetActive(true);
        DOTween.Kill(transform);
        transform.DOScale(1.5f, 0.25f).OnComplete(() => transform.DOScale(1.3f, 0.3f).OnComplete(() => collectableScript.isCollected = false));
        OnReturned?.Invoke();
    }

    public float ReturnExecutionTime()
    {
        return 0f;
    }
}
