using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using ZilyanusLib.Audio;

public class CollectableScript : MonoBehaviour
{
    public static event Action OnSpawned;
    public bool isCollected;

    [SerializeField] LayerMask GridMask;

    [SerializeField] Transform Effect;

    [SerializeField] SoundData soundData;

    public static event Action<ICommand> OnCommandCreated;
    private void Start()
    {
        OnSpawned?.Invoke();

        Effect.DOScale(0.85f, 0.5f).OnComplete(() => Effect.DOScale(0.8f, 0.5f)).SetLoops(-1,LoopType.Yoyo);
    }

    private void OnEnable()
    {
        ControlParent();
    }

    void ControlParent()
    {
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, Vector2.one, 0, Vector2.down, 0.1f, GridMask);

        if (hit)
        {
            GridParent gridParent = null;
            if (hit.transform.GetComponentInParent<GridParent>() != null)
            {
                gridParent = hit.transform.GetComponentInParent<GridParent>();
            }
            else
            {
                gridParent = hit.transform.GetComponent<GridScript>().TransformToGridParent();
            }

            transform.parent = gridParent.transform;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8 && !isCollected)
        {          
            Collected();
        }
    }

    public void Collected()
    {
        EatCommand eatCommand = new EatCommand(transform,this);
        OnCommandCreated.Invoke(eatCommand);
        AudioClass.PlayAudio(soundData);
    }
}
