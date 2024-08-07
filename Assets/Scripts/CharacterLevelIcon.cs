using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using ZilyanusLib.Audio;

public class CharacterLevelIcon : MonoBehaviour
{
    public static UnityEvent<int> OnLevelTranslate = new UnityEvent<int>();
    private void OnEnable()
    {
        LevelSelector.OnLevelClicked.AddListener(OnLevelClicked);
        LevelSelector.OnCursorSpawn.AddListener(StartPos);
    }

    private void OnDisable()
    {
        LevelSelector.OnLevelClicked.RemoveListener(OnLevelClicked);
        LevelSelector.OnCursorSpawn.RemoveListener(StartPos);
    }

    void StartPos(Vector3 pos)
    {
        transform.position = pos;
    }

    private void OnLevelClicked(int level,Vector3 Pos)
    {
        if (Pos != transform.position)
        {
            transform.DOMove(Pos, 0.3f).OnComplete(() =>
            {
                OnLevelTranslate.Invoke(level);
            });
        }
        else
        {
            OnLevelTranslate.Invoke(level);
        }
    }
}
